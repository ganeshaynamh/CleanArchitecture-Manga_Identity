using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manga.Application.Boundaries.SignUpUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manga.WebApi.SignUpController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IUseCase UseCase;
        private readonly Presenter Presenter;

        public SignUpController(IUseCase useCase, Presenter presenter)
        {
            UseCase = useCase;
            Presenter = presenter;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<Object> Registerapplicationuser([FromBody]SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                await UseCase.Execute(new Input(model.UserName, model.Email, model.password, model.PhoneNumber,model.SSN));
                return Presenter.ViewModel;
            }
            else
            {
                return BadRequest(ModelState);

            }
        }
    }
}