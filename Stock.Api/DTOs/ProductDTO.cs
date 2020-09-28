using System.ComponentModel.DataAnnotations;


namespace Stock.Api.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }


        public decimal CostPrice { get; set; }


        public decimal SalePrice { get; set; }


        public string ProductTypeDesc { get; set; }


        public string ProductTypeId { get; set; }


        private int _stock { get; set; }


        public string Provider { get; set; }

    }
}