using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetterCommerce.Entity.Entities
{
    public class Order: BaseEntity
    {
        [Required] public string OrderNumber { get; set; }
        [Required] public int AddressId { get; set; }
        [Required] public double Total  { get; set; }
        [Required] public DateTime OrderDate { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
        public List<int> OrderLineId { get; set; }
        
    }
}    