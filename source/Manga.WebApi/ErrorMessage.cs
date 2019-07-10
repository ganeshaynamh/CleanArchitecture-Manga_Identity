using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manga.WebApi
{
    public class ErrorMessage 
    {
        public ErrorMessage(string Message)
        {
            this.Message = Message;
        }

        public string Message { get; private set; }
    }
}
