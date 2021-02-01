using System.ComponentModel;

namespace VendorService.Domain.Enums
{
    public enum EProfiles
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Employee")]
        Employee = 2,
        [Description("Customer")]
        Customer = 3
    }
}
