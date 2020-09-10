using System.Linq;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.Entity.CartModels;
using BetterCommerce.Entity.Entities;

namespace BetterCommerce.Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<IQueryable<Order>> GetOrdersOfUser(string userId);
        IDataResult<IQueryable<OrderLine>> GetOrderLinesOfOrder(int orderId);
        IResult CreateOrder(Order order, Cart cart);
        IResult UpdateOrderStatus(Order order);
        
    }
}