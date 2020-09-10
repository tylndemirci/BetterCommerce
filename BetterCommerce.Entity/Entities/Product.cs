using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterCommerce.Entity.Entities
{
    public class Product : BaseEntity
    {
        public int Count { get; set; }
        public int Star { get; set; }
        public int SoldCount { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }
        [Column(TypeName = "decimal(18,2)")] public double BasePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")] public double DiscountPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")] public double FinalPrice { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsStock { get; set; }

        public bool IsInSale { get; set; }

        public bool IsFeatured { get; set; }
        public bool IsPassive { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}