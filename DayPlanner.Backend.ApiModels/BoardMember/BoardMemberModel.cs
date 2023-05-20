
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.ApiModels.BoardMember
{
    public class BoardMemberModel
    {
        public int BoardId { get; set; }
        public int MemberId { get; set; }
        public Board Board { get; set; }
        public User Member { get; set; }
    }
}
