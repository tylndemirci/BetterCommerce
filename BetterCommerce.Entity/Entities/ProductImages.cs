﻿namespace BetterCommerce.Entity.Entities
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
    }
}    