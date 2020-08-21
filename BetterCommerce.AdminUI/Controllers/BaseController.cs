using BetterCommerce.Business.Structure.Abstract;
using BetterCommerce.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BetterCommerce.AdminUI.Controllers
{
    public class BaseController: Controller
    {
        public readonly IBusinessService _businessService;
        public readonly IUnitOfWork _UnitOfWork;

        public BaseController(IBusinessService businessService, IUnitOfWork unitOfWork)
        {
            _businessService = businessService;
            _UnitOfWork = unitOfWork;
        }
    }
}