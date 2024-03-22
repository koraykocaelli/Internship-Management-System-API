using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;


namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipBookWriteRepository : WriteRepository<InternshipBook>, IInternshipBookWriteRepository
    {
        public InternshipBookWriteRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
    
}
