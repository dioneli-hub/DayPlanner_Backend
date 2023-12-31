﻿namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IEmailService
    {
       Task SendVerificationEmail(int userId);
       Task SendResetPasswordEmail(int userId);
        Task SendInviteToBoardEmail(int inviterId, string invitedPersonEmail, int boardId);
    }
}
