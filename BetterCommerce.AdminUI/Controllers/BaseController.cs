
using BetterCommerce.Business.Abstract;
using BetterCommerce.Business.Structure.Abstract;
using BetterCommerce.Core.Identity;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Controllers
{
    public class BaseController: Controller
    {
        public readonly IBusinessService _BusinessService;
        public readonly UserManager<ApplicationUser> _UserManager;
        public readonly IBaseDal<Order> _OrderBaseDal;
        public readonly IBaseDal<OrderLine> _OrderLineBaseDal;

        public BaseController(UserManager<ApplicationUser> userManager, IBaseDal<Order> orderBaseDal,
            IBaseDal<OrderLine> orderLineBaseDal, IBusinessService businessService)
        {
            _UserManager = userManager;
            _OrderBaseDal = orderBaseDal;
            _OrderLineBaseDal = orderLineBaseDal;
            _BusinessService = businessService;
        }
    }
}