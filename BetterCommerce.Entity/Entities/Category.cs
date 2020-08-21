﻿using System.Collections.Generic;

namespace BetterCommerce.Entity.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<int> ProductId { get; set; }
    }
}