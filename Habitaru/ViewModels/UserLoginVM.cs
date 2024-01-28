using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Habitaru.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string Username { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
