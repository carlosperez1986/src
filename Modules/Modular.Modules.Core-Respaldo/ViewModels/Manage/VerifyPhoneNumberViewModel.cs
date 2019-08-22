using System.ComponentModel.DataAnnotations;

namespace Modular.Modules.Core.ViewModels.Account.Manage
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
