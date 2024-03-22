using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Persistence.Contexts;
using InternshipManagementSystem.Domain.Entities;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipDocumentWriteRepository : WriteRepository<InternshipDocument>, IInternshipDocumentWriteRepository
    {
        public InternshipDocumentWriteRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
