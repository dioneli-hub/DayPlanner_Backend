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

        public EmailService(DataContext context)
        {
            _context = context;
        }


        public async Task SendVerificationEmail(int userId)
        {
            try
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

                    using var smtp = new SmtpClient();
                    smtp.Connect("smtp.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync("dianka_levchenko@outlook.com", "Lbfyf1mamapapacats");//this.FromEmail, this.FromEmailPswd
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                else throw new ApplicationException("User to send email to not found...");
            } catch
            {
                throw new ApplicationException("Some error has occured while sending the verification email");
            }
           
        }

        public async Task SendResetPasswordEmail(int userId)
        {
            try
            {
                var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
                if (user != null)
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("dianka_levchenko@outlook.com"));//this.FromEmail
                    email.To.Add(MailboxAddress.Parse(user.Email));// user.Email //"round.world@bk.ru"
                    email.Subject = "DayPlanner Reset Password";

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = GetHTMLResetPasswordTemplate(user);

                    email.Body = bodyBuilder.ToMessageBody();
                    //email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    //{
                    //    Text = $"<h1>Hey, {user.FirstName} {user.LastName}! Here is your reset password token. Copy it and paste at the website in order to set a new password.</h1>" +
                    //    "<h2> Please, do not share this token with anyone.</h2>" +
                    //    $"{user.ResetPasswordToken}",

                    //};

                    using var smtp = new SmtpClient();
                    smtp.Connect("smtp.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync("dianka_levchenko@outlook.com", "Lbfyf1mamapapacats");//this.FromEmail, this.FromEmailPswd
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                else throw new ApplicationException("User to send email to not found...");

            } catch
            {
                throw new ApplicationException("Some error has occured while sending the reset password email");
            }
           
        }

        public async Task SendInviteToBoardEmail(int inviterId, string invitedPersonEmail, int boardId)
        {
            try
            {
                var inviter = await _context.Users.Where(x => x.Id == inviterId).FirstOrDefaultAsync();
                var board = await _context.Boards.Where(x => x.Id == boardId).FirstOrDefaultAsync();

                if (inviter == null)
                {
                    throw new ApplicationException("The inviting person seems not to be registered at DayPlanner...");
                }

                var invitation = await _context.BoardMembershipInvitations.Where(x => (x.InviterId == inviterId) && (x.InvitedPersonEmail == invitedPersonEmail) && (x.BoardId == boardId)).FirstOrDefaultAsync();

                if (invitation == null)
                {
                    throw new ApplicationException("Such invitation not found in the DayPlanner's database...");
                }

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("dianka_levchenko@outlook.com"));//this.FromEmail
                email.To.Add(MailboxAddress.Parse(invitedPersonEmail));// user.Email //"round.world@bk.ru"
                email.Subject = "DayPlanner Board Join Invitation";

                var bodyBuilder = new BodyBuilder();

                bodyBuilder.HtmlBody = $@"
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
                    <h1>Board Join Invitation</h1>
                    <p>Dear User,</p>
                    <p>{inviter.Email} is inviting you to join board {board.Name} at DayPlanner. In order to accomplish your join process, please, register (if you are still not with us) and click the Join button below:</p>
                    <p>
                        <a href='http://localhost:4200/join-board?token={Uri.EscapeDataString(invitation.InvitationToken)}&decline=false' class='button' type='button'>Join</a>
                    </p>
                    <hr/>
                    <p>Or decline the invitation: </p>
                    <p>
                        <a href='http://localhost:4200/join-board?token={Uri.EscapeDataString(invitation.InvitationToken)}&decline=true' class='button' type='button'>Decline</a>
                    </p>
                    <p><i>Please, ignore this email if you did not intend to register at DayPlanner.</i></p>
                </div>
            </body>
            </html>";

                email.Body = bodyBuilder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("dianka_levchenko@outlook.com", "Lbfyf1mamapapacats");//this.FromEmail, this.FromEmailPswd
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch
            {
                throw new ApplicationException("Some error has occured while sending the invitation email...");
            }
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
                    <p>Dear {user.FirstName} {user.LastName},</p>
                    <p>Thank you for joining us at DayPlanner. In order to accomplish your registration process, click the verification button below:</p>
                    <p>
                        <a href='http://localhost:4200/verify?token={Uri.EscapeDataString(user.VerificationToken)}' class='button' type='button'>Verify</a>
                    </p>
                    <p><i>Please, ignore this email if you did not try to register at DayPlanner.</i></p>
                </div>
            </body>
            </html>";
        }

        private string GetHTMLResetPasswordTemplate(User user)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                <title>DayPlanner: Reset Password</title>
                <meta name='viewport' content='width=device-width, initial-scale=1'>
                <style>
                    
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Reset Password</h1>
                    <p>Dear {user.FirstName} {user.LastName},</p>
                    <p>You are trying to reset password at DayPlanner. In order to accomplish the process, please, click the Reset Password button below:</p>
                    <p>
                        <a href='http://localhost:4200/reset-password?token={Uri.EscapeDataString(user.ResetPasswordToken)}' class='button' type='button'>Reset Password</a>
                    </p>
                    <p>Or just copy the token and paste it at the website:</p>
                    <p><b>{user.ResetPasswordToken}</b></p>
                    <p><i>Please, ignore this email if you did not try to reset your password at DayPlanner.</i></p>
                </div>
            </body>
            </html>";
        }

        private string GetCssStyles()
        {
            return $@"
                      
                body, p, h1, h2, h3, h4, h5, h6 {{ margin: 0; padding: 0;}}
                body {{font-family: Arial, sans-serif; line-height: 1.6; background-color: #f5f5f5;}}
                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; background-color: #ffffff;border: 1px solid #e0e0e0;border-radius: 5px; box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);}}
                .header {{text-align: center;padding-bottom: 20px;}} 
                .logo {{max - width: 100px;}}
                .content {{padding: 20px;background-color: #f9f9f9;border-radius: 5px;}}
                .button {{display: inline-block;background-color: #007bff;color: #ffffff; padding: 10px 20px;text-decoration: none;border-radius: 5px;}}
                .footer {{text-align: center;margin-top: 20px;color: #999999;}}

            ";
        }

 
    }
}
