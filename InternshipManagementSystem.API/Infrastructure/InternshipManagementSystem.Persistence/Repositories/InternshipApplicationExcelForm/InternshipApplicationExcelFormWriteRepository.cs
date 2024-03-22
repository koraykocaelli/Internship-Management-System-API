using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;


namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipApplicationExcelFormWriteRepository : WriteRepository<InternshipApplicationExcelForm>, IInternshipApplicationExcelFormWriteRepository
    {
        public InternshipApplicationExcelFormWriteRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        { 
        }

    }
}
