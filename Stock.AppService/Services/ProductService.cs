using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stock.AppService.Services
{
    public class ProductService: BaseService<Product>
    {
        private readonly IRepository<ProductType> productTypeRepository;
        private readonly ProductTypeService productTypeService;

        public ProductService(IRepository<Product> repository, IRepository<ProductType> productTypeRepository, ProductTypeService productTypeService)
            : base(repository)
        {
            this.productTypeRepository = productTypeRepository;
            this.productTypeService = productTypeService;
        }

        public new Product Create(Product entity)
        {
            var validCategory = this.productTypeRepository.List(x => x.Initials.ToUpper().Equals(entity.ProductTypeDesc.ToUpper())).Count == 1;
            var categorias = this.productTypeService.GetAll();
            if (validCategory)
            {
                if(this.NombreUnico(entity.Name))
                {
                    return base.Create(entity);
                }                
                throw new System.Exception("Producto ya existente");
            }

            throw new System.Exception("Categoría inválida");
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
