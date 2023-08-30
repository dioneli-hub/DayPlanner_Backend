using DayPlanner.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.ApiModels.Recurrence
{
    public class RecurringPatternModel
    {
        public int TaskId { get; set; }
        public string RecurringType { get; set; }
        public int OccurencesNumber { get; set; }
    }
}
