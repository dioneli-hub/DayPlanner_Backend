
namespace DayPlanner.Backend.Domain
{
    public class BoardMember
    {
            public int BoardId { get; set; }
            public int MemberId { get; set; }
            public Board Board { get; set; }
            public User Member { get; set; }

    }
}
