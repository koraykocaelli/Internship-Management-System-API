using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipWriteRepository : WriteRepository<Internship>, IInternshipWriteRepository
    {
        public InternshipWriteRepository(InternshipManagementSystemDbContext context) : base(context)
        {
        }
    }
}
