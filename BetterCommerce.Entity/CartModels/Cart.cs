using System.Collections.Generic;
using System.Linq;

namespace BetterCommerce.Entity.CartModels
{
    public class Cart
    {
        public Cart()
        {
            CartLines = new List<CartLine>();
        }
        public List<CartLine> CartLines { get; set; }
        public string UserId { get; set; }

        public double TotalPrice()
        {
            return CartLines.Sum(x => x.Product.FinalPrice * x.Quantity);
        }

        public int TotalProductQuantity()
        {
            return CartLines.Sum(x => x.Quantity);
        }
    }
}