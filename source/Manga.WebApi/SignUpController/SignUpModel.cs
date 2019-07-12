using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manga.WebApi.SignUpController
{
    public class SignUpModel
    {
        [Required]
        public string UserName { get; set; }

        [Required, RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not vaild..")]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }

       

        [Required, RegularExpression(@"^([0-9]{10})$", ErrorMessage = "please enter a valid Phone Number..")]
        public string PhoneNumber { get; set; }
        [Required]
        public string SSN { get; set; }
    }
}
