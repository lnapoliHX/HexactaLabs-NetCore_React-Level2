namespace Stock.Api.DTOs
{
    public class ProductSearchDTO
    {
        public string Name { get; set; }
        public string productTypeDesc { get; set; }
        public ActionDto Condition { get; set; }
    }
}