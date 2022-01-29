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
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity, TDbContext> 
        where TEntity : Entity
        where TDbContext : DbContext
    { 
        protected readonly TDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbset;

        public BaseRepository(TDbContext context)
        { 
            _dbContext = context;
            _dbset = _dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.LastModified = DateTime.UtcNow;
            entity.IsDeleted = false;

            var ret =_dbContext.Add(entity);

            //_dbContext.SaveChanges();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.LastModified = DateTime.UtcNow;
            entity.IsDeleted = false;

            _dbset.Add(entity); 

            return entity;
        }
        public void Update(TEntity entity)
        {
            (entity as Entity).LastModified = DateTime.UtcNow; 
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public TEntity Get(object id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            if (entity == null) return null;
            if (entity.IsDeleted == false)
                return entity;
            return null;
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).Where(t => t.IsDeleted == false);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().Where(t => t.IsDeleted == false);
        }
        

    }
}
