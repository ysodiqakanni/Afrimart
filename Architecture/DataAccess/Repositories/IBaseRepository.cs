using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IBaseRepository<T, TDbContext> where T : Entity where TDbContext : DbContext
    {
        IQueryable<T> GetAll();

        T Get(object id);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void Update(T entity);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindInclude(Expression<Func<T, bool>> predicate, List<string> includeProperties);
        T GetInclude(Expression<Func<T, bool>> predicate, List<string> includeProperties);
    }
}
