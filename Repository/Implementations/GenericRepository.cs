using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _db;

        public GenericRepository(DbContext db)
        {
            _db = db;
        }
        public void Add(TEntity entity)
        {
            //here set is setting the entity type
            _db.Set<TEntity>().Add(entity);
        }

        public void DeleteById(object Id)
        {
            var entity = _db.Set<TEntity>().Find(Id);
            if (null != entity)
                _db.Set<TEntity>().Remove(entity);
        }

        public TEntity Find(object Id)
        {
            return _db.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public void Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
        }
    }
}
