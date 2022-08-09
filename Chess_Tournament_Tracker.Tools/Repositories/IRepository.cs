using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.Tools.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Any(Func<TEntity, bool> predicate);
        bool Delete(TEntity entity);
        IEnumerable<TEntity> FindMany(Func<TEntity, bool>? predicate = null);
        TEntity FindOne(Func<TEntity, bool> predicate);
        TEntity? FindOne(params object[] ids);
        TEntity Insert(TEntity entity);
        bool Update(TEntity entity);
    }
}
