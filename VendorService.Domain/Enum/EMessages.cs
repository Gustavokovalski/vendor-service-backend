

using System.ComponentModel;

namespace VendorService.Domain.Enums
{
    public enum EMessages
    {
        [Description("Usuário cadastrado com sucesso.")]
        Success = 1,
        [Description("Product not found.")]
        ProductNotFound = 2,
        [Description("Order not found.")]
        OrderNotFound = 3,


    }
}
