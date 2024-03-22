using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;


namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipApplicationFormReadRepository : ReadRepository<InternshipApplicationForm>, IInternshipApplicationFormReadRepository
    {
        public InternshipApplicationFormReadRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
