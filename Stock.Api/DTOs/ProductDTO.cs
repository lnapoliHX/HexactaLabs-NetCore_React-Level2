using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stock.Model.Entities;


namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        [Required]

        public string Name { get; set; }


        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }


        public int Stock {get;set;}

        public string ProductTypeId {get;set;}

        public string ProductTypeDesc {get;set;} 


       public string ProviderId {get;set;}

        public string ProviderDesc {get;set;} 

    }
}