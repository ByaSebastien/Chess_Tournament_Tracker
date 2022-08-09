using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.Tools.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> Entities;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<TEntity>();
        }
        public TEntity? FindOne(params object[] ids)
        {
            return Entities.Find(ids);
        }
        public TEntity FindOne(Func<TEntity, bool> predicate)
        {
            return Entities.SingleOrDefault(predicate) ?? throw new ArgumentNullException("Not Found");
        }
        public IEnumerable<TEntity> FindMany(Func<TEntity, bool>? predicate = null)
        {
            if (predicate == null)
                return Entities;
            return Entities.Where(predicate);
        }

        public bool Any(Func<TEntity, bool> predicate)
        {
            return Entities.Any(predicate);
        }


        public bool Delete(TEntity entity)
        {
            Entities.Remove(entity);
            return _dbContext.SaveChanges() == 1;
        }


        public TEntity Insert(TEntity entity)
        {
            TEntity newEntity = Entities.Add(entity).Entity;
            _dbContext.SaveChanges();
            return newEntity;
        }

        public bool Update(TEntity entity)
        {
            Entities.Update(entity);
            return _dbContext.SaveChanges() == 1;
        }
    }
}
