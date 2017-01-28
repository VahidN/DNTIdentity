namespace ASPNETCoreIdentitySample.ViewModels.Identity.Emails
{
    public class TwoFactorSendCodeViewModel : EmailsBase
    {
        public string Token { set; get; }
    }
}