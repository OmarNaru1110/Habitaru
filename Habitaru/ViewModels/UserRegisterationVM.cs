using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Habitaru.ViewModels
{
    public class UserRegisterationVM
    {
        [Required]
        public string Username { get; set; }
        [DataType(DataType.Password),Required]
        public string Password { get; set; }
        [DataType(DataType.Password),Required,Compare("Password")]
        public string ConfirmPassword{ get; set; }
    }
}
