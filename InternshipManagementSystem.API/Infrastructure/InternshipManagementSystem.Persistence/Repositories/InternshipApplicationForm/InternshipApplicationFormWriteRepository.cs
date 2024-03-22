using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class InternshipApplicationFormWriteRepository: WriteRepository<InternshipApplicationForm>, IInternshipApplicationFormWriteRepository
    {
        public InternshipApplicationFormWriteRepository(InternshipManagementSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
   
}
