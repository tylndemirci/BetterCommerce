using BetterCommerce.Business.Structure.Abstract;
using BetterCommerce.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;


namespace BetterCommerce.AdminUI.Controllers
{
    public class HomeController : BaseController
    {
      

        public IActionResult Index()
        {
            return View();
        }


        public HomeController(IBusinessService businessService, IUnitOfWork unitOfWork) : base(businessService, unitOfWork)
        {
        }
    }
}