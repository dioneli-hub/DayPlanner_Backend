namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface IValidationService
    {
        bool ValidatePassword(string password);
        bool ValidateEmail(string email);
    }
}
