using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        string FromEmail { get; set; } = "round.world@bk.ru";
        string FromEmailPswd { get; set; } = "thisiscreatedforus";

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
                    Text = $"<h1>{user.VerificationToken}</h1>",
                    //Text = "<h1>Hi</h1>",
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
