using System.Linq;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.AdminUI.Models.Products
{
    public class ListProductsModel
    {
        public ListProductsModel(Product product)
        {
            Star = product.Star;
            FinalPrice = product.FinalPrice;
            Name = product.Name;
            Count = product.Count;
            IsInSale = product.IsInSale;
            ImageUrl = product.Images.FirstOrDefault()?.ImageUrl;
        }
        public int Star { get; set; }
        public double FinalPrice { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string CategoryName { get; set; }
        public int Count { get; set; }
        public bool IsInSale { get; set; }
        public string ImageUrl { get; set; }

    }
}