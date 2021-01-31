

using System.ComponentModel;

namespace VendorService.Domain.Enums
{
    public enum EMessages
    {
        [Description("Usuário cadastrado com sucesso.")]
        SuccessUserCreate = 1,
        [Description("Usuário excluído com sucesso.")]
        SuccessUserDelete = 2,
        [Description("Usuário atualizado com sucesso.")]
        SuccessUserEdit = 3,
        [Description("Produto cadastrado com sucesso.")]
        SuccessProductCreate = 4,
        [Description("Produto excluído com sucesso.")]
        SuccessProductDelete = 5,
        [Description("Produto atualizado com sucesso.")]
        SuccessProductEdit = 6,
        [Description("Pedido cadastrado com sucesso.")]
        SuccessOrderCreate = 7,
        [Description("Pedido excluído com sucesso.")]
        SuccessOrderDelete = 8,
        [Description("Pedido atualizado com sucesso.")]
        SuccessOrderEdit = 9,
        [Description("Erro ao realizar a operação.")]
        Error = 10,
        [Description("Operação realizada com sucesso.")]
        Success = 11,




    }
}
