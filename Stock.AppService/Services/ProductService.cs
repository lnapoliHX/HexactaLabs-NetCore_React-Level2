using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            if (this.NombreUnico(entity.Id, entity.Name))
            {
                return base.Create(entity);
            }

            throw new System.Exception("The name is already in use");
        }

        private bool NombreUnico(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper()) &&
                                             !x.Id.Equals(id)).Count == 0;
        }

        public new Product Update(Product entity)
        {
            if (this.NombreUnico(entity.Id, entity.Name))
            {
                return base.Update(entity);
            }

            throw new System.Exception("The name is already in use");
        }

        public IEnumerable<Product> GetProductsByProductTypeId(string id) 
        {
            return this.Repository.List(x => x.ProductType.Id.Equals(id));
        }

        public IEnumerable<Product> GetProductsByProviderId(string id) 
        {
            return this.Repository.List(x => x.ProviderId.Equals(id));
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}