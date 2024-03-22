using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class StudentReadRepository : ReadRepository<Student>, IStudentReadRepository
    {
        public StudentReadRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
