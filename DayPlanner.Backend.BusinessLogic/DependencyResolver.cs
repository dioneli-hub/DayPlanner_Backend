using DayPlanner.Backend.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DayPlanner.Backend.BusinessLogic.Services;
using DayPlanner.Backend.BusinessLogic.Services.Security;
using DayPlanner.Backend.BusinessLogic.Services.Auth;
using DayPlanner.Backend.BusinessLogic.Services.Validation;
using DayPlanner.Backend.BusinessLogic.Services.Recurrence;

namespace DayPlanner.Backend.BusinessLogic
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBoardProvider, BoardProvider>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<ITaskItemProvider, TaskItemProvider>();
            services.AddScoped<ITaskItemService, TaskItemService>();
            services.AddScoped<IBoardMemberProvider, BoardMemberProvider>();
            services.AddScoped<IBoardMemberService, BoardMemberService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IPasswordVerifier, PasswordVerifier>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationProvider, NotificationProvider>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IRecurrenceService, RecurrenceService>();
            services.AddScoped<IRecurrenceProvider, RecurrenceProvider>();


            return services;
        }
    }
}
