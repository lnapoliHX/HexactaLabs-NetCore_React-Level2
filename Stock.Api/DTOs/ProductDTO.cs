using System.ComponentModel.DataAnnotations;

namespace Stock.Api.DTOs
{
    public class ProductDTO
    {                       
        public string Id { get; set; }

        public string Name { get; set; }

        public string ProductTypeId { get; set; }

        public string ProductTypeDesc { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public virtual ProductTypeDTO ProductType { get; set; }

        private int _stock;

        public int Stock
        {
            get
            {
                return this._stock;
            }
        }

        public void DescontarStock(int value)
        {
            if (this._stock - value < 0)
                // throw new ("No hay stock disponible para efectuar la operación.");

            this._stock -= value;
        }

        public void SumarStock(int value)
        {
            this._stock += value;
        }

        public string ProviderId { get; set; }
        public ProviderDTO Provider { get; set; }    
    }
}
