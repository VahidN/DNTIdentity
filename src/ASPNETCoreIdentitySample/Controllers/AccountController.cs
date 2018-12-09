using System.Security.Claims;
using System.Threading.Tasks;
using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.DataLayer.Context;
using ASPNETCoreIdentitySample.Entities.Identity;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.Services.Token;
using ASPNETCoreIdentitySample.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ASPNETCoreIdentitySample.Controllers
{
  [Route("api/[controller]")]
  //  [EnableCors("CorsPolicy")]
  public class AccountController : Controller
  {
    private readonly IApplicationUserManager _usersService;
    private readonly ITokenStoreService _tokenStoreService;
    private readonly IUnitOfWork _uow;
    private readonly IApplicationSignInManager _signInManager;
    private readonly ITokenFactoryService _tokenFactoryService;


    public AccountController(
            IApplicationUserManager usersService,
            ITokenStoreService tokenStoreService,
            ITokenFactoryService tokenFactoryService,
            IApplicationSignInManager signInManager,
            IUnitOfWork uow

          )
    {
      _usersService = usersService;
      _usersService.CheckArgumentIsNull(nameof(usersService));

      _tokenStoreService = tokenStoreService;
      _tokenStoreService.CheckArgumentIsNull(nameof(tokenStoreService));

      _uow = uow;
      _uow.CheckArgumentIsNull(nameof(_uow));

      _signInManager = signInManager;
      _signInManager.CheckArgumentIsNull(nameof(_signInManager));


      _tokenFactoryService = tokenFactoryService;
      _tokenFactoryService.CheckArgumentIsNull(nameof(tokenFactoryService));
    }



    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody]  LoginViewModel loginUser)
    {
      if (loginUser == null)
      {
        return BadRequest("user is not set.");
      }

      var user = await _usersService.FindByNameAsync(loginUser.Username);

      var result1 = await _signInManager.PasswordSignInAsync(
        loginUser.Username,
        loginUser.Password,
        false,
        lockoutOnFailure: true);

      if (user == null || !user.IsActive)
      {
        return Unauthorized();
      }

      var result = await _tokenFactoryService.CreateJwtTokensAsync(user);
      await _tokenStoreService.AddUserTokenAsync(user, result.RefreshTokenSerial, result.AccessToken, null);
      await _uow.SaveChangesAsync();

    //  _antiforgery.RegenerateAntiForgeryCookies(result.Claims);

      return Ok(new { access_token = result.AccessToken, refresh_token = result.RefreshToken });
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken([FromBody]JToken jsonBody)
    {
      var refreshTokenValue = jsonBody.Value<string>("refreshToken");
      if (string.IsNullOrWhiteSpace(refreshTokenValue))
      {
        return BadRequest("refreshToken is not set.");
      }

      var token = await _tokenStoreService.FindTokenAsync(refreshTokenValue);
      if (token == null)
      {
        return Unauthorized();
      }

      var result = await _tokenFactoryService.CreateJwtTokensAsync(token.User);
      await _tokenStoreService.AddUserTokenAsync(token.User, result.RefreshTokenSerial, result.AccessToken, _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue));
      await _uow.SaveChangesAsync();

    //  _antiforgery.RegenerateAntiForgeryCookies(result.Claims);

      return Ok(new { access_token = result.AccessToken, refresh_token = result.RefreshToken });
    }

    [AllowAnonymous]
    [HttpGet("[action]")]
    public async Task<bool> Logout(string refreshToken)
    {
      var claimsIdentity = this.User.Identity as ClaimsIdentity;
      var userIdValue = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

      // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
      // Delete the user's tokens from the database (revoke its bearer token)
      await _tokenStoreService.RevokeUserBearerTokensAsync(userIdValue, refreshToken);
      await _uow.SaveChangesAsync();

    //  _antiforgery.DeleteAntiForgeryCookies();

      return true;
    }

    [HttpGet("[action]"), HttpPost("[action]")]
    public bool IsAuthenticated()
    {
      return User.Identity.IsAuthenticated;
    }

    [HttpGet("[action]"), HttpPost("[action]")]
    public IActionResult GetUserInfo()
    {
      var claimsIdentity = User.Identity as ClaimsIdentity;
      return Json(new { Username = claimsIdentity.Name });
    }


  }
}