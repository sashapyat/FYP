using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System.Text;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public class EmailService : IEmailService
    {
        //
        // code used from: https://mailtrap.io/blog/asp-net-core-send-email/#Sending-an-Email-in-ASPNET-Core-Asynchronously
        // Youtube tutorial part 1: https://www.youtube.com/watch?v=UY0AAnOhep4&list=PLaFzfwmPR7_LTXu0Vz9Zz_Y0OMMC7ArHZ&index=104
        // Youtube tutorial part 2: https://www.youtube.com/watch?v=EDxp5Nl1xGQ&list=PLaFzfwmPR7_LTXu0Vz9Zz_Y0OMMC7ArHZ&index=105
        // Youtube tutorial part 3: https://www.youtube.com/watch?v=_rbVbTO4a5o&list=PLaFzfwmPR7_LTXu0Vz9Zz_Y0OMMC7ArHZ&index=106
        //

        private const string templatePath = @"EmailTemplates/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;

        public async Task SendConfirmationEmailForRegistration(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Please Confirm your Email", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ConfirmationEmail_Registration"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendForgotPasswordEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Reset your Password", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendPaymentConfirmationEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Payment Confirmation", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ConfirmationEmail_Payment"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }
        
        public async Task SendOngoingPaymentConfirmationEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Membership Renewal", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ConfirmationEmail_OngoingPayment"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendCancelMembershipEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Cancel Membership", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("CancelMembership"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }
        
        public async Task SendMembershipHasBeenCancelledEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Membership Ended", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("MembershipHasBeenCancelled"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }


        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }
    }
}
