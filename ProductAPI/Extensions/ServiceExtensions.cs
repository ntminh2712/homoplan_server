using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeminarAPI.Data;
using SeminarAPI.Repositories.Implementation;
using SeminarAPI.Repositories.Interface;

namespace SeminarAPI.Extensions
{
    /// <summary>
    /// Configure ServiceExtensions
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configure Cors
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin() 
                    .AllowAnyMethod()                   
                    .AllowAnyHeader());                 
            });
        }

        /// <summary>
        /// Configure SqlServerContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SeminarDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));
        }

        /// <summary>
        /// Configure Repositories
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IDailyTasksService, DailyTasksService>();
            services.AddTransient<IChallengeTasksService, ChallengeTasksService>();
            services.AddTransient<ITransaction, TransactionService>();
        }
    }
}
