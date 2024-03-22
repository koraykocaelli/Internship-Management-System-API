using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Application.Token;
using InternshipManagementSystem.Domain.Entities.Identity;
using InternshipManagementSystem.Infrastructure.Services.Token;
using InternshipManagementSystem.Persistence.Contexts;
using InternshipManagementSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipManagementSystem.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceService(this
            IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<InternshipManagementSystemDbContext>();



            services.AddDbContext<InternshipManagementSystemDbContext>(options => options.UseNpgsql(Configuration.ConnectionString), ServiceLifetime.Singleton);
            services.AddSingleton<ITokenHandler, TokenHandler>();


            services.AddScoped<IAdvisorReadRepository, AdvisorReadRepository>();
            services.AddScoped<IAdvisorWriteRepository, AdvisorWriteRepository>();
            services.AddScoped<IInternshipReadRepository, InternshipReadRepository>();
            services.AddScoped<IInternshipWriteRepository, InternshipWriteRepository>();
            services.AddScoped<IStudentReadRepository, StudentReadRepository>();
            services.AddScoped<IStudentWriteRepository, StudentWriteRepository>();
            services.AddScoped<IInternshipDocumentReadRepository, InternshipDocumentReadRepository>();
            services.AddScoped<IInternshipDocumentWriteRepository, InternshipDocumentWriteRepository>();
            services.AddScoped<IInternshipApplicationFormWriteRepository, InternshipApplicationFormWriteRepository>();
            services.AddScoped<IInternshipApplicationFormReadRepository, InternshipApplicationFormReadRepository>();
            services.AddScoped<IInternshipBookReadRepository, InternshipBookReadRepository>();
            services.AddScoped<IInternshipBookWriteRepository, InternshipBookWriteRepository>();
            services.AddScoped<IInternshipApplicationExcelFormWriteRepository, InternshipApplicationExcelFormWriteRepository>();
            services.AddScoped<IInternshipApplicationExcelFormReadRepository, InternshipApplicationExcelFormReadRepository>();

        }
    }
}
