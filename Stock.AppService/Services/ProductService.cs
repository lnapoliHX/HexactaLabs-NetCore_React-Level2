using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
//using AutoMapper;


namespace Stock.AppService.Services
{
    public class ProductService : BaseService<Product>
    {

        private readonly ProductTypeService productTypeService;
        //private readonly IMapper mapper;

        public ProductService(IRepository<Product> repository, ProductTypeService productTypeService) : base(repository)
        {
            this.productTypeService = productTypeService;
            //this.mapper = mapper;
        }

        public new Product Create(Product entity)
        {
            if (this.NombreUnico<Product>(entity.Name))
            {
                var pType = this.productTypeService.Get(entity.ProductType.Id);
                entity.ProductType = pType;
                return base.Create(entity);
            }

            throw new System.Exception("The name is already in use");
        }
        private bool NombreUnico<T>(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return this.Repository.List(x => x.Name.ToUpper().Equals(name.ToUpper())).Count == 0;
        }

        public IEnumerable<Product> Search(Expression<Func<Product, bool>> filter)
        {
            return this.Repository.List(filter);
        }

        public Product UpdateProduct(Product product, string productTypeId, int modStock)
        {
            var productType = this.productTypeService.Get(productTypeId);
            Product dbProduct = base.Get(product.Id);
            product.SumarStock(dbProduct.Stock);
            product.ProductType = productType;
            if (modStock > 0)
            {
                product.SumarStock(modStock);
            }
            else
            {
                product.DescontarStock(Math.Abs(modStock));
            }
            return base.Update(product);
        }
    }
}
