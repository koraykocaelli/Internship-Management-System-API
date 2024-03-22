using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class AdvisorWriteRepository : WriteRepository<Advisor>, IAdvisorWriteRepository
    {
        public AdvisorWriteRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }

}
