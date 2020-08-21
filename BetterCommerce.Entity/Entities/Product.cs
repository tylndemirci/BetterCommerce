using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterCommerce.Entity.Entities
{
    public class Product : BaseEntity
    {
        public int Count { get; set; }
        public int Star { get; set; }

        public int SoldCount { get; set; }
        [Column(TypeName = "decimal(18,2)")] public double BasePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")] public double DiscountPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")] public double FinalPrice { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsStock { get; set; }

        public bool IsInSale { get; set; }

        public bool IsFeatured { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<ProductImages> Images { get; set; }
        public List<int> ImageId { get; set; }
        public ICollection<ProductDetails> ProductDetails { get; set; }
        public List<int> ProductDetailsId { get; set; }

        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }
    }
}