namespace Stock.Api.DTOs
{
    public class ModifyStockDataDTO
    {
        public string Id { get; set; }

        public int Value { get; set; }

        public bool Increase { get; set; }
    }
}
