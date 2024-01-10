using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public interface IEmailService
    {
        Task SendConfirmationEmailForRegistration(UserEmailOptions userEmailOptions);
        Task SendForgotPasswordEmail(UserEmailOptions userEmailOptions);
        Task SendPaymentConfirmationEmail(UserEmailOptions userEmailOptions);
        Task SendOngoingPaymentConfirmationEmail(UserEmailOptions userEmailOptions);
        Task SendCancelMembershipEmail(UserEmailOptions userEmailOptions);
        Task SendMembershipHasBeenCancelledEmail(UserEmailOptions userEmailOptions);
    }
}
