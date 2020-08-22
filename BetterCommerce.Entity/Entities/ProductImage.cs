using System;

namespace BetterCommerce.Entity.Entities
{
    public class ProductImage:BaseEntity
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}    