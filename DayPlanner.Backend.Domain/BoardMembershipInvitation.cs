
namespace DayPlanner.Backend.Domain
{
    public class BoardMembershipInvitation
    {
        public int Id { get; set; }
        public int InviterId { get; set; }
        public string InvitedPersonEmail { get; set; }
        public int BoardId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string InvitationToken { get; set; }
        public DateTimeOffset? IsAcceptedAt { get; set; }
        public bool IsDeclined { get; set; } = false;
    }
}
