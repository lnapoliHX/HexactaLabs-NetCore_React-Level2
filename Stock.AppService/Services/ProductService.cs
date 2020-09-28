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

        public new Product Create (Product entity)
        {
            if (IsUniqueName(entity.Name)){
                return base.Create(entity);
            }
            throw new System.ArgumentException("The name is already in use");
        }

        public new Product Update (Product entity){
            if (IsUniqueNameEdit(entity.Id, entity.Name)){
                return base.Update(entity);
            }
            throw new System.ArgumentException("The name is already in use");
        }
        public bool IsUniqueName (string name)
        {
            if(string.IsNullOrWhiteSpace(name)){
                return false;
            }
            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        public bool IsUniqueNameEdit (string id, string name){
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            return this.Repository.List(x=> x.Name.ToUpper().Equals(name.ToUpper()) && (x.Id != id)).Count == 0;
        }
        public IEnumerable<Product> Search(Expression<Func<Product,bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}