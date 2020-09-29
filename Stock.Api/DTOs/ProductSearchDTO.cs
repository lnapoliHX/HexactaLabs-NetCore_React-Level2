namespace Stock.Api.DTOs
{
    public class ProductSearchDTO
    {
        public string ProductTypeDesc { get; set; }

        public string Name { get; set; }

        public ActionDto Condition { get; set; }
    }
} 