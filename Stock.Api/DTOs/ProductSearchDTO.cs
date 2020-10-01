namespace Stock.Api.DTOs
{
    public class ProductSearchDTO
    { 
        public string Name { get; set; }

        public string ProductTypeName { get; set; }

        public string ProviderName { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public decimal Stock {get; set; }

        public ActionDto Condition { get; set; } = ActionDto.AND;
    }
}