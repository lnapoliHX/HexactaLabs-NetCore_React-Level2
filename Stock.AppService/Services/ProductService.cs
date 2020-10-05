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

        public new Product Get(string id)
        {
            Product aux = this.Repository.GetById(id);
           // ProductTypeService ptService = new ProductTypeService(); <-- El vinculo esta hecho en el controller!
           // ProductType ptAux = ptService.Get(aux.ProductType.Id);
           // aux.ProductType = ptAux;
            return aux;
        }

        public new Product Create(Product entity)
        {
            if (entity.CostPrice > 0 && entity.SalePrice>0)
            {
                return base.Create(entity);
            }

            throw new System.Exception("El precio de venta y el costo no pueden ser cero");
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