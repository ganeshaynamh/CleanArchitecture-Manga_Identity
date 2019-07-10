using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Application.Boundaries.LoginUser
{
    public class Output
    {
        public string Message { get; set; }
        public Output(string message)
        {
            Message = message;
        }
    }
}
