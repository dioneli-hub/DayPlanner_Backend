

namespace DayPlanner.Backend.ApiModels.BoardMember
{
    public class BoardMemberModel
    {
        public int BoardId { get; set; }
        public int MemberId { get; set; }
        public BoardModel Board { get; set; }
        public UserModel Member { get; set; }
    }
}
