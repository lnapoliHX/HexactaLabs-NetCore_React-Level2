using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;

namespace Stock.AppService.Services
{
    public class ProductService : BaseService<Product>
    {
        public ProductService(IRepository<Product> repository) : base(repository)
        {    
              
        }

        public new Product Create(Product entity)
        {
            if (this.NombreUnico(entity.Name) 
                && (entity.CostPrice > 0) 
                && (entity.SalePrice> 0))
            {
                return base.Create(entity);
            }

            throw new System.Exception("Los datos ingresados son incorrectos.");
        }
       


        private bool NombreUnico(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        public IEnumerable<Product> Search(Expression<Func<Product,bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}