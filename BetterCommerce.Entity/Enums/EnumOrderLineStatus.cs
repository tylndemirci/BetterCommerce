using System.ComponentModel;

namespace BetterCommerce.Entity.Enums
{
    public enum EnumOrderLineStatus
    {
        [Description("Product was not delivered")] NotDelivered,
        [Description("Product was delivered successfully")] SuccessfullyDelivered,
        [Description("Product was refunded")] Refunded,
    }
}