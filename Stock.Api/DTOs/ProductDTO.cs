using Stock.Model.Base;
using Stock.Model.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Api.DTOs
{
    public class ProductDTO: IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public string ProductTypeId { get; set; }
        
        public string ProductTypeDesc { get; set; }

        private int _stock;

        public int Stock
        {
            get
            {
                return this._stock;
            }
        }

        public string ProviderId { get; set; }
        public ProviderDTO Provider { get; set; }
    }
}
