namespace DayPlanner.Backend.Api.Managers
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTimeOffset ExpiredAt { get; set; }
    }
}
