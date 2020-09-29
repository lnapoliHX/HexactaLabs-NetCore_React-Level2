using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using System.Collections.Generic;
using Stock.Repository.LiteDb.Interface;
using System;
using Stock.Model.Exceptions;

namespace Stock.AppService.Services
{
    public class ProductService : BaseService<Product>
    {
        public ProductService(IRepository<Product> repository) : base(repository)
        {    

        }

        public new Product Create(Product entity)
        {
            var validationMessage = this.validate(entity);
            if (string.IsNullOrWhiteSpace(validationMessage))
            {
                return base.Create(entity);
            }
            throw new ModelException(validationMessage);
        }

        private string validate(Product entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                return("The name is required");

            if (this.Repository.List(x => x.Name.ToUpper().Equals(entity.Name.ToUpper()) && x.Id != entity.Id).Count != 0)
                return("The name is already in use");

            if (entity.CostPrice <= 0)
                return("CostPrice must be greater than 0");

            if (entity.SalePrice <= 0)
                return("SalePrice must be greater than 0");

            if (entity.SalePrice < entity.CostPrice)
                return("SalePrice must be higher than CostPrice");

            if (entity.ProductType == null)
                return("ProductType is required");

            return null;
        }

        public new Product Update(Product entity)
        {
            var validationMessage = this.validate(entity);
            if (string.IsNullOrWhiteSpace(validationMessage))
            {
                return base.Update(entity);
            }
            throw new ModelException(validationMessage);
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
} 