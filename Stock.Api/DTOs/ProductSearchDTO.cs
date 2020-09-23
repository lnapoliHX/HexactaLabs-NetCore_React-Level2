using Stock.Model.Entities;

namespace Stock.Api.DTOs
{
    public class ProductSearchDTO
    {
        public string Name { get; set; }

        public ProductType ProductType { get; set;}

        public ActionDto Condition { get; set; } 
    }
}