using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BetterCommerce.Entity.Enums;

namespace BetterCommerce.Entity.Entities
{
    public class Order: BaseEntity
    {
       
        [Required] public double Total  { get; set; }
        [Required] public int AddressId { get; set; }
        [Required] public string OrderNumber { get; set; }
        public string UserId { get; set; }
        [Required] public DateTime OrderDate { get; set; }
        public EnumOrderStatus OrderStatus { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
        public List<int> OrderLineId { get; set; }
        
    }
}    