using InternshipManagementSystem.Application.Repositories;
using InternshipManagementSystem.Domain.Entities;
using InternshipManagementSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InternshipManagementSystem.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly InternshipManagementSystemDbContext _dbContext;

        public WriteRepository(InternshipManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<T> Table => _dbContext.Set<T>();


        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddAsyncRange(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(T entities)
        {
            EntityEntry<T> entityEntry = Table.Remove(entities);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            T model = await Table.FirstOrDefaultAsync(e => e.ID == id);
            return Remove(model);
        }


        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }
        //remove range async
        public async Task<bool> RemoveRangeAsync(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public async Task<int> SaveAsync()
          => await _dbContext.SaveChangesAsync();

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
