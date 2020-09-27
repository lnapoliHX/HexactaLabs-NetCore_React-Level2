using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        public string Id { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string ProductTypeId { get; set; }
        public string ProductTypeDesc { get; set; }
        public string ProviderId { get; set; }
        public int _stock { get; set; }
        public int Stock { get; set; }
    }
}