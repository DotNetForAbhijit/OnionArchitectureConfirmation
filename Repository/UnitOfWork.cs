using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        DatabaseContext _db;
        public UnitOfWork(DatabaseContext db)
        {
            _db = db;
        }

        private IProductRepository _ProductRepo;

        public IProductRepository ProductRepo
        {
            get
            {
                if (_ProductRepo == null)
                    _ProductRepo = new ProductRepository(_db);

                return _ProductRepo;
            }
        }

        public IRepository<Category> _CategoryRepo;

        public IRepository<Category> CategoryRepo
        {
            get
            {
                if (_CategoryRepo == null)
                    _CategoryRepo = new GenericRepository<Category>(_db);

                return _CategoryRepo;
            }
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
