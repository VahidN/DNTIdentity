using System.IO;
using ASPNETCoreIdentitySample.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;
using Microsoft.Extensions.Options;
using System;
using DNTCommon.Web.Core;

namespace ASPNETCoreIdentitySample.Services.Identity
{
    public class UsersPhotoService : IUsersPhotoService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IOptionsSnapshot<SiteSettings> _siteSettings;

        public UsersPhotoService(
            IHttpContextAccessor contextAccessor,
            IWebHostEnvironment hostingEnvironment,
            IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _siteSettings = siteSettings ?? throw new ArgumentNullException(nameof(siteSettings));
        }

        public string GetUsersAvatarsFolderPath()
        {
            var usersAvatarsFolder = _siteSettings.Value.UsersAvatarsFolder;
            var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, usersAvatarsFolder);
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }
            return uploadsRootFolder;
        }

        public void SetUserDefaultPhoto(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.PhotoFileName))
            {
                return;
            }

            var avatarPath = Path.Combine(GetUsersAvatarsFolderPath(), user.PhotoFileName ?? string.Empty);
            if (!File.Exists(avatarPath))
            {
                user.PhotoFileName = _siteSettings.Value.UserDefaultPhoto;
            }
        }

        public string GetUserDefaultPhoto(string photoFileName)
        {
            if (string.IsNullOrWhiteSpace(photoFileName))
            {
                return _siteSettings.Value.UserDefaultPhoto;
            }

            var avatarPath = Path.Combine(GetUsersAvatarsFolderPath(), photoFileName ?? string.Empty);
            return !File.Exists(avatarPath) ? _siteSettings.Value.UserDefaultPhoto : photoFileName;
        }

        public string GetUserPhotoUrl(string photoFileName)
        {
            photoFileName = GetUserDefaultPhoto(photoFileName);
            return $"~/{_siteSettings.Value.UsersAvatarsFolder}/{photoFileName}";
        }

        public string GetCurrentUserPhotoUrl()
        {
            var photoFileName = _contextAccessor.HttpContext.User.Identity.GetUserClaimValue(ApplicationClaimsPrincipalFactory.PhotoFileName);
            return GetUserPhotoUrl(photoFileName);
        }
    }
}