namespace DayPlanner.Backend.BusinessLogic.Models
{
    public class HashModel
    {
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
    }
}
