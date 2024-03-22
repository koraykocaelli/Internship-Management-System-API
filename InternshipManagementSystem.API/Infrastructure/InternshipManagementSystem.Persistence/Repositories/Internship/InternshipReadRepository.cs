using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipReadRepository : ReadRepository<Internship>, IInternshipReadRepository
    {
        public InternshipReadRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
