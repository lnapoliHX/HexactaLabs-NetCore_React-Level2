using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }
        
        [Required]
        public decimal? SalePrice { get; set; }

        [Required]
        public string ProductTypeId { get; set; }
        public string ProductTypeDesc { get; set; }
        
        [Required]
        public int Stock { get; set; }       

        public string ProviderId { get; set; }
        public string NameProvider { get; set; }
    }
}
