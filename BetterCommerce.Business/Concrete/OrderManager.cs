using System;
using System.Collections.Generic;
using System.Linq;
using BetterCommerce.Business.Abstract;
using BetterCommerce.Core.Extensions;
using BetterCommerce.Core.Utilities.Results;
using BetterCommerce.DataAccess.Abstract;
using BetterCommerce.Entity.CartModels;
using BetterCommerce.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace BetterCommerce.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IBaseDal<Order> _orderRepo;
        private readonly IBaseDal<OrderLine> _orderLineRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderManager(IBaseDal<Order> orderRepo, IBaseDal<OrderLine> orderLineRepo, IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _orderLineRepo = orderLineRepo;
            _unitOfWork = unitOfWork;
        }


        public IDataResult<IQueryable<Order>> GetOrdersOfUser(string userId)
        {
            if (userId.IsNullS()) return new ErrorDataResult<IQueryable<Order>>("User not found.");
            var orders = _orderRepo.GetBy(x => x.UserId == userId)?
                .Include(x => x.OrderLines).Where(x => x.IsDeleted == false);
            if (orders == null) return new ErrorDataResult<IQueryable<Order>>("There is no order.");
            return new SuccessDataResult<IQueryable<Order>>(orders);
        }

        public IDataResult<IQueryable<OrderLine>> GetOrderLineOfOrder(int orderId)
        {
            var orderLine = _orderLineRepo.GetBy(x => x.OrderId == orderId);
            if (orderLine == null) return new ErrorDataResult<IQueryable<OrderLine>>("There is no order line.");
            return new SuccessDataResult<IQueryable<OrderLine>>(orderLine);
        }

        public IResult CreateOrder(Order order, Cart cart)
        {
            if (order == null) return new ErrorResult("Order is empty.");
            if (order.AddressId == 0) return new ErrorResult("Address is empty.");
            var newOrder = new Order();
            newOrder.Total = order.Total;
            newOrder.AddressId = order.AddressId;
            newOrder.OrderDate = DateTime.Now;
            newOrder.OrderNumber = "B" + (new Random()).Next(1111111, 9999999).ToString();
            newOrder.UserId = order.UserId;
            newOrder.OrderLineId = order.OrderLineId;
            newOrder.CreatedAt = DateTime.Now;
            newOrder.OrderLines = new List<OrderLine>();
            foreach (var product in cart.CartLines)
            {
                var orderLine = new OrderLine();
                orderLine.Quantity = product.Quantity;
                orderLine.Price = product.Product.FinalPrice;
                orderLine.ProductId = product.Product.Id;
                newOrder.OrderLines.Add(orderLine);
                _orderLineRepo.Create(orderLine);
            }

            _orderRepo.Create(newOrder);
            var result = _unitOfWork.SaveChanges();
            return result > 0 
                ? (IResult) new SuccessResult("Order successfully created.") 
                : new ErrorResult("Order not created.");
        }

        public IResult UpdateOrderStatus(Order order)
        {
            var updatingOrder = _orderRepo.GetBy(x => x.Id == order.Id)?.FirstOrDefault();
            if (updatingOrder==null) return new ErrorResult("Order not found.");
            // updatingOrder.Status = order.Status;
            _orderRepo.Update(updatingOrder);
            var result = _unitOfWork.SaveChanges();
            return result > 0 
                ? (IResult) new SuccessResult("Order successfully updated.") 
                : new ErrorResult("Order not updated.");
        }
    }
}