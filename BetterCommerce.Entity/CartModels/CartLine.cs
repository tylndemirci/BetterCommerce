using BetterCommerce.Entity.Entities;

namespace BetterCommerce.Entity.CartModels
{
    public class CartLine
    {
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}