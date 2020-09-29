using Stock.Model.Base;
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

        public int stock;

        public int Stock
        {
            get
            {
                return this.stock;
            }
        }

        public void DescontarStock(int value)
        {
            if (this.stock - value < 0)
                throw new ModelException("No hay stock disponible para efectuar la operación.");

            this.stock -= value;
        }

        public void SumarStock(int value)
        {
            this.stock += value;
        }

        public string ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
