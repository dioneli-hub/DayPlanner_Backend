namespace DayPlanner.Backend.ApiModels.Recurrence
{
    public class RecurringPatternModel
    {
        public int TaskId { get; set; }
        public string RecurringType { get; set; }
        public int OccurencesNumber { get; set; }
    }
}
