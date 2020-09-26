using Stock.Model.Entities;
using Stock.Model.Exceptions;
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
        
        [Required]
        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        //public virtual ProductType ProductType { get; set; }

        public int Stock{ get; set; }
        public string ProviderId { get; set; }
        //public ProviderDTO Provider { get; set; }

        public string ProductTypeId { get; set; }
        public string ProductTypeDesc { get; set; }


    }
}
