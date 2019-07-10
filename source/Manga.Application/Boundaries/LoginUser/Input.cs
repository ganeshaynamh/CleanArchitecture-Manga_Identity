using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Application.Boundaries.LoginUser
{
    public class Input
    {
        

        public string  UserName { get; set; }
        public string Password { get; set; }

        public Input(String username,String password)
        {
            UserName = username;
            Password = password;
        }
    }
}
