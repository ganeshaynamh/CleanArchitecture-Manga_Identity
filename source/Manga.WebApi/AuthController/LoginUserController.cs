using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manga.Application.Boundaries.LoginUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manga.WebApi.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        private readonly IUseCase useCase;
        private readonly Presenter presenter;

        public LoginUserController(IUseCase useCase, Presenter presenter)
        {
            this.useCase = useCase;
            this.presenter = presenter;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            await useCase.Execute(new Input(loginModel.UserName, loginModel.Password));
            return presenter.ViewModel;
        }
    }
}