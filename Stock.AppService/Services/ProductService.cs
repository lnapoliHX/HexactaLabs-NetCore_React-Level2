using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    public class ProductService: BaseService<Product>
    {                
        public ProductService(IRepository<Product> repository)
            : base(repository)
        {
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}