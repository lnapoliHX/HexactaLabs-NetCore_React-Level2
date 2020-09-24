using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        public string ProductTypeId { get; set; }

        [Required]
        public string ProductTypeDesc { get; set; }

        [Required]
        public string ProviderId { get;set; }
        
        public string ProviderName { get;set; }

        public int Stock { get; set;}
        
        private int _stock { get; set;}

    }
}
