using DAL;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        //DbContext _db;

        public DatabaseContext context 
        {
            get 
            {
                return _db as DatabaseContext;
            }
        }
        public ProductRepository(DbContext db): base(db)
        {
            //_db = db;
        }
        public IEnumerable<ProductModel> GetProduct()
        {
            var data = (from prd in context.Products
                        join cat in context.Categories on prd.CategoryId equals cat.CategoryId
                        select new ProductModel
                        {
                            ProductId = prd.ProductId,
                            Description = prd.Description,
                            Name = prd.Name,
                            Category = cat.Name,
                            UnitPrice = prd.UnitPrice
                        }).ToList();

            return data;
        }
    }
}
