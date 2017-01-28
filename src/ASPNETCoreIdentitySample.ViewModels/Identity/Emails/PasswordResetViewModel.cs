namespace ASPNETCoreIdentitySample.ViewModels.Identity.Emails
{
    public class PasswordResetViewModel : EmailsBase
    {
        public int UserId { set; get; }
        public string Token { set; get; }
    }
}