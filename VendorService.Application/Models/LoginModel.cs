using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace VendorService.Application.Mappers
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
