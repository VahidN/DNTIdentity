using ASPNETCoreIdentitySample.Common.GuardToolkit;
using ASPNETCoreIdentitySample.Common.WebToolkit;
using ASPNETCoreIdentitySample.Services.Contracts.Identity;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using System.IO;
using System.Threading.Tasks;
using System;
using ASPNETCoreIdentitySample.ViewModels.Identity.Settings;

namespace ASPNETCoreIdentitySample.Services.Identity
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2551
    /// And http://www.dotnettips.info/post/2564
    /// </summary>
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly IOptionsSnapshot<SiteSettings> _smtpConfig;
        private readonly IViewRendererService _viewRendererService;

        public AuthMessageSender(
            IOptionsSnapshot<SiteSettings> smtpConfig,
            IViewRendererService viewRendererService)
        {
            _smtpConfig = smtpConfig;
            _smtpConfig.CheckArgumentIsNull(nameof(_smtpConfig));

            _viewRendererService = viewRendererService;
            _viewRendererService.CheckArgumentIsNull(nameof(_viewRendererService));
        }

        public async Task SendEmailAsync<T>(string email, string subject, string viewNameOrPath, T model)
        {
            var message = await _viewRendererService.RenderViewToStringAsync(viewNameOrPath, model).ConfigureAwait(false);
            await SendEmailAsync(email, subject, message).ConfigureAwait(false);
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            var smtpConfigValue = _smtpConfig.Value.Smtp;
            smtpConfigValue.CheckArgumentIsNull(nameof(smtpConfigValue));

            emailMessage.From.Add(new MailboxAddress(smtpConfigValue.FromName, smtpConfigValue.FromAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };


            if (smtpConfigValue.UsePickupFolder)
            {
                using (var stream = new FileStream(
                    Path.Combine(smtpConfigValue.PickupFolder, $"email-{Guid.NewGuid().ToString("N")}.eml"),
                    FileMode.CreateNew))
                {
                    emailMessage.WriteTo(stream);
                }
            }
            else
            {
                using (var client = new SmtpClient())
                {
                    if (!string.IsNullOrWhiteSpace(smtpConfigValue.LocalDomain))
                    {
                        client.LocalDomain = smtpConfigValue.LocalDomain;
                    }
                    await client.ConnectAsync(smtpConfigValue.Server, smtpConfigValue.Port, SecureSocketOptions.None).ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(smtpConfigValue.Username) &&
                        !string.IsNullOrWhiteSpace(smtpConfigValue.Password))
                    {
                        await client.AuthenticateAsync(smtpConfigValue.Username, smtpConfigValue.Password).ConfigureAwait(false);
                    }
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}