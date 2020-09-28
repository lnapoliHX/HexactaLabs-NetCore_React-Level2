using Stock.Model.Entities;
namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public string ProductTypeId { get; set; }

        public string ProductTypeDesc { get; set; }

        public int Stock { get; set; }

        private int _stock { get; set; }

        public string ProviderId { get; set; }

        public string ProviderName { get; set; }

    }
}