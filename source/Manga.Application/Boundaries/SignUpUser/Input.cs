using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Application.Boundaries.SignUpUser
{
    public class Input
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string PhoneNumber { get; set; }
        public Input(string username, string email, string password, string phonenumber)
        {
            this.UserName = username;
            Email = email;
            this.password = password;
            PhoneNumber = phonenumber;
        }
    }
}
