using BlazorFormsTest.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BlazorFormsTest.Service
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient sendGridClient;

        public EmailService(ISendGridClient sendGrid)
        {
            sendGridClient = sendGrid ?? throw new ArgumentNullException(nameof(sendGrid));
        }

        public async Task<bool> SendEmailAsync(Contact contact)
        {
            SendGridMessage message = new SendGridMessage();
            EmailAddress emailAddress = new EmailAddress(contact.Email, contact.Name);
            List<EmailAddress> recipients = new List<EmailAddress> { new EmailAddress("email@yes.com", "Winter")};

            message.SetSubject("Testing email");
            message.SetFrom(emailAddress);
            message.AddTos(recipients);
            message.PlainTextContent = contact.Message;

            Response response = await sendGridClient.SendEmailAsync(message);
            if (Convert.ToInt32(response.StatusCode) >= 400)
            {
                return false;
            }

            return true;
        }
    }
}
