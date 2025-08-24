namespace ASPNETCoreIdentitySample.ViewModels.Identity;
public sealed class ExternalLoginConfirmationViewModel
{
    [Required(ErrorMessage = "ایمیل لازم است.")]
    [EmailAddress(ErrorMessage = "ایمیل نامعتبر است.")]
    [Display(Name = "ایمیل")]
    public required string Email { get; set; }

    [Display(Name = "نام")] public string FirstName { get; set; }
    [Display(Name = "نام خانوادگی")] public string LastName { get; set; }

    public string ReturnUrl { get; set; }
    public string ProviderDisplayName { get; set; }
}
