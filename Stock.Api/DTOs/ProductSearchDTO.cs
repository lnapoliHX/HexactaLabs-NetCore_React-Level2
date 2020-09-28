namespace Stock.Api.DTOs
{
    public class ProductSearchDTO
    {
        public string ProviderName { get; set; }

        public string Name { get; set; }

        public ActionDto Condition { get; set; }
    }
}