namespace Stock.Api.DTOs
{
    public class ProductSearchDTO
    {
        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public string ProductTypeDesc { get; set; }

        public ActionDto Condition { get; set; } 
    }
}