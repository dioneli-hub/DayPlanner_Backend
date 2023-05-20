using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Repositories;
using Microsoft.Extensions.DependencyInjection;
using DayPlanner.Backend.BusinessLogic.Services;
using DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember;

namespace DayPlanner.Backend.BusinessLogic
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IBoardMemberRepository, BoardMemberRepository>();

            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBoardProvider, BoardProvider>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<ITaskItemProvider, TaskItemProvider>();
            services.AddScoped<ITaskItemService, TaskItemService>();
            services.AddScoped<IBoardMemberProvider, BoardMemberProvider>();
            services.AddScoped<IBoardMemberService, BoardMemberService>();

            return services;
        }
    }
}
