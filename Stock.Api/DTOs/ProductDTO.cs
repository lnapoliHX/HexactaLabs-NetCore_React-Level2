using System.ComponentModel.DataAnnotations;
using Stock.Model.Entities;


namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string CostPrice { get; set; }

        public string SalePrice { get; set; }

        public int ProductTypeId { get; set;}

        public string ProductTypeDesc { get; set; }

        public int stock {get; set;}

    }
}