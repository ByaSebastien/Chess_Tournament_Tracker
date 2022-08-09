using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.Tools.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> GetAll();
        public TEntity? GetById(params object[] ids);
        public TEntity Insert(TEntity entity);
        public bool Update(TEntity entity);
        public bool Delete(TEntity entity);
    }
}
