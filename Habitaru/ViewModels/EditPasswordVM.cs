using System.ComponentModel.DataAnnotations;

namespace Habitaru.ViewModels
{
    public class EditPasswordVM
    {
        [Required,DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password),Compare(otherProperty: "NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
