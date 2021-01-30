using System;
using VendorService.Domain.Enums;

namespace VendorService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public EProfiles Profile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
