using System.Linq;
using BetterCommerce.AdminUI.Models.Products;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Business.Structure.Abstract;
using BetterCommerce.Core.Identity;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Controllers
{
    public class ProductController : BaseController
    {
       
        public IActionResult ListProducts()
        {
         // var products= _businessService.Product.GetProductList();
         // if (products.Data==null)
         // {
         //     HttpContext.Response.Redirect("/ErrorPage");
         // }
         // var returnModel = products.Data!.Select(x=> new ListProductsModel(x));
         return View();
        }


        public ProductController(UserManager<ApplicationUser> userManager, IBaseDal<Order> orderBaseDal,
            IBaseDal<OrderLine> orderLineBaseDal, IBusinessService businessService) 
            : base(userManager, orderBaseDal, orderLineBaseDal, businessService)
        {
        }
    }
}