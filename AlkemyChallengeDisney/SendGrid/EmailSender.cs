using SendGrid;
using SendGrid.Helpers.Mail;

namespace AlkemyChallengeDisney.SendGrid
{
    public static class EmailSender
    {
        public static async Task SendEmailAsync(string email, string name)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("moraguesrafael@gmail.com", "Rafael Moragues");
            var subject = "Registro de cuenta";
            var to = new EmailAddress(email, name);
            var plainTextContent = "Su cuenta se registro correctamente. Muchas gracias";
            var htmlContent = "<strong>Su cuenta se registro correctamente. Muchas gracias</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
