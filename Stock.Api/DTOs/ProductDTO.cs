using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }
        public int Stock { set; get; }
        private int _stock { set; get; }

        public string ProviderId { get; set; }

        public string ProductTypeId { get; set; }
        public string ProductTypeDesc { set; get; }

    }
}
