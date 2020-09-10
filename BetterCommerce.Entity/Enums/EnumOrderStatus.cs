using System.ComponentModel;

namespace BetterCommerce.Entity.Enums
{
    public enum EnumOrderStatus
    {
        [Description("Order is waiting for approval")] WaitingForApproval,
        [Description("Order is approved")] OrderApproved,
        [Description("Order is on the way to your address")] OrderDispatched,
        [Description("Delivery day")] OutForDelivery,
        [Description("Order is completed")] OrderCompleted
    }
}