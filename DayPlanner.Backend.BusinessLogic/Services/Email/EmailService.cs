using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using DayPlanner.Backend.Domain;

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
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = GetHTMLVerificationTemplate(user);

                email.Body = bodyBuilder.ToMessageBody();
            //    email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //    {
            //        Text = $"<h1>Hey, {user.FirstName} {user.LastName}! Here is your verification token. Copy it and paste at the website in order to activate your account.</h1>"+
            //        "<h2> Please, do not share this token with anyone.</h2>" +
            //        $"{user.VerificationToken}",
            //};

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

                };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("dianka_levchenko@outlook.com", "Lbfyf1mamapapacats");//this.FromEmail, this.FromEmailPswd
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            else throw new ApplicationException("User to send email to not found...");
        }

        private string GetHTMLVerificationTemplate(User user)
        {
             return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                <title>DayPlanner: Verify Your Email</title>
                <meta name='viewport' content='width=device-width, initial-scale=1'>
                <style>
                    
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Email Verification</h1>
                    <p>Daer {user.FirstName} {user.LastName},</p>
                    <p>Thank you for joining us in DayPlanner. In order to accomplish your registration process, click the verification button below:</p>
                    <p>
                        <a href='http://localhost:4200/verify?token={Uri.EscapeDataString(user.VerificationToken)}' class='button' type='button'>Verify</a>
                    </p>
                    <p><i>Please, ignore this email if you did not try to register at DayPlanner.</i></p>
                </div>
            </body>
            </html>";
        }

    }
}
