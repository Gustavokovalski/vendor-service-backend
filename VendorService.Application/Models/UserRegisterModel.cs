using System;
using VendorService.Application.Models;

namespace VendorService.Application.Mappers
{
    public class UserRegisterModel
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ProfileId { get; set; }
        public EnumModel? Profile { get; set; }
        public string Token { get; set; }
    }
}
