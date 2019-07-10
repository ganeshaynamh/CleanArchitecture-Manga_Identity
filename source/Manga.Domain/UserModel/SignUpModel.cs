using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Domain.UserModel
{
    public class SignUpModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string password { get; set; }

        
        public string PhoneNumber { get; set; }
    }
}
