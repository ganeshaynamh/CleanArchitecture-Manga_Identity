using Manga.Application.Boundaries.LoginUser;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manga.WebApi.AuthController
{
    public class Presenter : IOutputHandler
    {
        public IActionResult ViewModel { get; private set; }
        public void Error(string message)
        {
            ViewModel = new ObjectResult(new ErrorMessage(message));
        }

        public void Handle(Output output)
        {
            ViewModel = new ObjectResult(output);
        }
    }
}
