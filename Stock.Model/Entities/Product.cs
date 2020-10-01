using Stock.Model.Base;
using System.Collections.Generic;
using Stock.Model.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Model.Entities
{
    [Table("product")]
    public class Product: IEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public virtual ProductType ProductType { get; set; }

        private int _stock {get; set; }

        public int Stock
        {
            get
            {
                return this._stock;
            }
        }

        public void DiscountStock(int value)
        {
            if (this._stock - value < 0)
                throw new ModelException("No hay stock disponible para efectuar la operación.");

            this._stock -= value;
        }

        public void AddStock(int value)
        {
            this._stock += value;
        }

        public string ProviderId { get; set; }
        public Provider Provider { get; set; }
        // public List<Provider> Providers { get; set; }

    }
}
