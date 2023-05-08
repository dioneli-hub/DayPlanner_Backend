using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.DataAccess.Entities
{
    public class BoardMember
    {
            public int BoardId { get; set; }
            public int MemberId { get; set; }
            public Board Board { get; set; }
            public User Member { get; set; }
    }
}
