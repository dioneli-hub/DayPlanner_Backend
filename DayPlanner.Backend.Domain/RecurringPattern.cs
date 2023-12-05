using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.Domain
{
    public class RecurringPattern
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public RecurringType RecurringType { get; set; }
        public int OccurencesNumber { get; set; }
    }

    public enum RecurringType
    {
        DAILY,
        WEEKLY,
        MONTHLY
    }
}
