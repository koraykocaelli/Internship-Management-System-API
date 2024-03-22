using InternshipManagementSystem.Domain.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        T GetFirst(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(Guid id ,bool tracking = true);

        Task<bool> AnyAsync(Expression<Func<T, bool>> method ,bool tracking = true); 




    }
}
