using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using System.Security.Principal;
namespace DayPlanner.Backend.Api.Helper.HttpContext
{
    public class UserHttpContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserHttpContextService(IHttpContextAccessor contextAccessor) 
        {
            _contextAccessor = contextAccessor;
        }
        private IIdentity? UserIdentity => _contextAccessor.HttpContext?.User.Identity;

        public int GetCurrentUserId()
        {
            var nameClaim = UserIdentity?.Name;
            if (!string.IsNullOrEmpty(nameClaim) && int.TryParse(nameClaim, out var userId))
            {
                return userId;
            }

            throw new ApplicationException("User is not authenticated!");
        }
    }
}
