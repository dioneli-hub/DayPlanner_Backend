using DayPlanner.Backend.Api.Interfaces;
using DayPlanner.Backend.DataAccess;

namespace DayPlanner.Backend.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

    }
}
