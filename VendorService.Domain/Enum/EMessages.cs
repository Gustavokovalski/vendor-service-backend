

using System.ComponentModel;

namespace VendorService.Domain.Enums
{
    public enum EMessages
    {
        [Description("Success.")]
        Success = 1,
        [Description("Product not found.")]
        ProductNotFound = 1,
        [Description("Order not found.")]
        OrderNotFound = 2,


    }
}
