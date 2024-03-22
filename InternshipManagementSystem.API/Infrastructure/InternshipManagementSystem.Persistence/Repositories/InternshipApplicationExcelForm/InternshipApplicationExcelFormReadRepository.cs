using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipApplicationExcelFormReadRepository : ReadRepository<InternshipApplicationExcelForm>, IInternshipApplicationExcelFormReadRepository
    {
        public InternshipApplicationExcelFormReadRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }


    }
}
