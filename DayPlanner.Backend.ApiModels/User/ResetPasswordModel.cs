namespace DayPlanner.Backend.ApiModels.User
{
    public class ResetPasswordModel
    {
        public string ResetPasswordToken { get; set; }
        public string NewPassword { get; set; }
    }
}
