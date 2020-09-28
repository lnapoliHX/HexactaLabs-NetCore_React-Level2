using Stock.AppService.Base;
using Stock.Model.Entities;
using Stock.Repository.LiteDb.Interface;


namespace Stock.AppService.Services
{
    public class ProductService : BaseService<Product>
    {
        private readonly ProviderService providerservice;
        private readonly ProductTypeService productTypeService;


        public ProductService(IRepository<Product> repository, ProviderService providerservice, ProductTypeService productTypeService) : base(repository)
        {
            this.providerservice = providerservice;
            this.productTypeService = productTypeService;
        }

        public new Product Create(Product entity)
        {


            Provider provider = this.providerservice.Get(entity.ProviderId);
            ProductType productType = this.productTypeService.Get(entity.ProductTypeId);
            entity.Provider = provider;
            entity.ProductType = productType;

            return base.Create(entity);


        }

        public Product UptdateP(Product entity, int stock)
        {
            var productType = this.productTypeService.Get(entity.ProductTypeId);
            entity.ProductType = productType;
            if(stock > 0)
            {
                entity.SumarStock(stock);

            }
            else { entity.DescontarStock(stock); }


            return base.Update(entity);


        }



    }
}
