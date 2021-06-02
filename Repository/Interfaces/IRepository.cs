using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity Find(object Id);
        void DeleteById(object Id);
    }
}
