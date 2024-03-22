           using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly InternshipManagementSystemDbContext _dbContext;    

        public ReadRepository(InternshipManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DbSet<T> Table => _dbContext.Set<T>();

        public Task<bool> AnyAsync(Expression<Func<T, bool>> method , bool tracking =true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.AnyAsync(method);  
        }

        public IQueryable<T> GetAll(bool tracking = true)
        {
           var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        {
               var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();            
            return await query.FirstOrDefaultAsync(e => e.ID ==id);
        }

    

        public T GetFirst(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query.FirstOrDefault(method); 
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.SingleOrDefaultAsync(method);
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method , bool tracking = true)
        {
         var query = Table.AsQueryable();
            if (!tracking)                                                              
                query = query.AsNoTracking();
            return query.Where(method);
        }

      

    
   }
}
