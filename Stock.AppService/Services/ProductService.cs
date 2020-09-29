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

        public int ObtenerStock(string idProducto)
        {
            var producto = this.Repository.GetById(idProducto);
            return producto.Stock;
        }

        public void DescontarStock(string idProducto, int value)
        {
            var producto = this.Repository.GetById(idProducto);
            producto.DescontarStock(value);
            this.Repository.Update(producto);
        }

        public void SumarStock(string idProducto, int value)
        {
            var producto = this.Repository.GetById(idProducto);
            producto.SumarStock(value);
            this.Repository.Update(producto);
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> filter)
        {
            return this.Repository.List(filter);
        }
    }
}
