using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        //add sender email info as well as api address of the endpoint to verify to appsettings json
       
        private readonly DataContext _context;

        public EmailService(DataContext context) {
            _context = context;
        }
        

        public async Task SendVerificationEmail(int userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("dianka_levchenko@outlook.com"));//this.FromEmail
                email.To.Add(MailboxAddress.Parse(user.Email));// user.Email //"round.world@bk.ru"
                email.Subject = "DayPlanner Email Verification";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $"<h1>Hey, {user.FirstName} {user.LastName}! Here is your verification token. Copy it and paste at the website in order to activate your account.</h1>"+
                    "<h2> Please, do not share this token with anyone.</h2>" +
                    $"{user.VerificationToken}",
                    //Text = "<h1>Hi</h1>",   https://localhost:7231/api/User/verify?verificationToken={user.VerificationToken}
                    //Text = $"<form method=\"POST\" action=\"https://localhost:7231/api/User/verify?verificationToken={user.VerificationToken}\">" +
                    //"<input type=\"submit\" value=\"Verify\">" +
                    //$"</form>",

                    //Text = "<a href='https://localhost:7231/api/User/verify?verificationToken=" + user.VerificationToken + "'>Click</a>"
            };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("dianka_levchenko@outlook.com", "Lbfyf1mamapapacats");//this.FromEmail, this.FromEmailPswd
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            else throw new ApplicationException("User to send email to not found...");
        }

        public async Task SendResetPasswordEmail(int userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("dianka_levchenko@outlook.com"));//this.FromEmail
                email.To.Add(MailboxAddress.Parse(user.Email));// user.Email //"round.world@bk.ru"
                email.Subject = "DayPlanner Reset Password";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $"<h1>Hey, {user.FirstName} {user.LastName}! Here is your reset password token. Copy it and paste at the website in order to set a new password.</h1>" +
                    "<h2> Please, do not share this token with anyone.</h2>" +
                    $"{user.ResetPasswordToken}",
                    //Text = "<h1>Hi</h1>",   https://localhost:7231/api/User/verify?verificationToken={user.VerificationToken}
                    //Text = $"<form method=\"POST\" action=\"https://localhost:7231/api/User/verify?verificationToken={user.VerificationToken}\">" +
                    //"<input type=\"submit\" value=\"Verify\">" +
                    //$"</form>",

                    //Text = "<a href='https://localhost:7231/api/User/verify?verificationToken=" + user.VerificationToken + "'>Click</a>"
                };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("dianka_levchenko@outlook.com", "Lbfyf1mamapapacats");//this.FromEmail, this.FromEmailPswd
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            else throw new ApplicationException("User to send email to not found...");
        }

    }
}
