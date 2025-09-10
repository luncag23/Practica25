using Microsoft.AspNetCore.Identity;
using Practica25.Infrastructure.Data;
using SendGrid;
using SendGrid.Helpers.Mail;

public class SendGridEmailSender : IEmailSender<ApplicationUser>
{
     private readonly IConfiguration _config;

     public SendGridEmailSender(IConfiguration config)
     {
          _config = config;
     }

     public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
     {
          await SendEmailAsync(email, "Confirmă-ți adresa de email",
              $"<p>Salut {user.UserName},</p><p>Apasă pe linkul de mai jos pentru a-ți confirma emailul:</p><p><a href='{confirmationLink}'>Confirmă Email</a></p>");
     }

     public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
     {
          await SendEmailAsync(email, "Resetare parolă",
              $"<p>Salut {user.UserName},</p><p>Poți reseta parola aici:</p><p><a href='{resetLink}'>Resetează parola</a></p>");
     }

     public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
     {
          await SendEmailAsync(email, "Cod resetare parolă",
              $"<p>Salut {user.UserName},</p><p>Codul tău este:</p><h2>{resetCode}</h2>");
     }

     private async Task SendEmailAsync(string email, string subject, string htmlMessage)
     {
          var apiKey = _config["SendGrid:ApiKey"];
          var client = new SendGridClient(apiKey);

          var from = new EmailAddress(
              _config["SendGrid:FromEmail"],
              _config["SendGrid:FromName"]
          );

          var to = new EmailAddress(email);
          var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

          await client.SendEmailAsync(msg);
     }
}
