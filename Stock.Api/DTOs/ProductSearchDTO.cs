namespace Stock.Api.DTOs
{
    public class ProductSearchDTO
    {


        public string Name { get; set; }

     //   public string ProductType { get; set; }

        public string ProductTypeDesc { get; set; }

        
        public ActionDto Condition { get; set; } 
    }
}