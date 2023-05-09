using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DayPlanner.Backend.BusinessLogic
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBoardMemberRepository, BoardMemberRepository>();
            
            return services;
        }
    }
}
