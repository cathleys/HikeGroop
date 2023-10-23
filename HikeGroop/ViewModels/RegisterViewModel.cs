using HikeGroop.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HikeGroop.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; } 
        [Required(ErrorMessage="Email address is required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

		[Required]
		[Display(Name = "Hiker Type")]
		public HikerType HikerType { get; set; }
        
        [Required]
		public string City { get; set; }
        [Required]
        [DataType(DataType.Password)]
       
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage =" Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
