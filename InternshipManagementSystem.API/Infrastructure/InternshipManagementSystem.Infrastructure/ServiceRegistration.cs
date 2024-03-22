using InternshipManagementSystem.Application.Services;
using InternshipManagementSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipManagementSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastuctureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}
