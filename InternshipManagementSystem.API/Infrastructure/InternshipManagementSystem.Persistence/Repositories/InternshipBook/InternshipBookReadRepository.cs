using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;


namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipBookReadRepository : ReadRepository<InternshipBook>, IInternshipBookReadRepository
    {
        public InternshipBookReadRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }   
   
}
