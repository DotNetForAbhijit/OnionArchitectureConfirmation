using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepo { get; }

        IRepository<Category> CategoryRepo { get; }

        void SaveChanges();
    }
}
