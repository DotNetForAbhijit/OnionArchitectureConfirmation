﻿using DAL;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository: IRepository<Product>
    {
        IEnumerable<ProductModel> GetProduct();
    }
}
