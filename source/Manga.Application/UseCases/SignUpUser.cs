using Manga.Application.Boundaries.SignUpUser;
using Manga.Application.Repositories;
using Manga.Domain.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Application.UseCases
{
    public class SignUpUser : IUseCase
    {

        private readonly IOutputHandler OutputHandler;
        private readonly IAuthenticateRepository AuthenticationRepository;

        public IConfiguration Configuration { get; }
        public SignUpUser(IOutputHandler outputHandler, IAuthenticateRepository authenticateRepository,IConfiguration configuration)
        {
            OutputHandler = outputHandler;
            AuthenticationRepository = authenticateRepository;
            Configuration = configuration;

        }

        public async Task Execute(Input input)
        {
          

            var user = new IdentityUser
            {
                UserName = input.UserName,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber
            };

            var Email = await AuthenticationRepository.FindByEmail(input.Email);
            var Username = await AuthenticationRepository.FindByName(input.UserName);
            var Phone = await AuthenticationRepository.FindByPhoneNumber(input.PhoneNumber);

            if (Email != null)
            {
                OutputHandler.Error("Email Already Exist");
            }
            if (Username != null)
            {
                OutputHandler.Error("Username Already Exist");
            }
            if (Phone != null)
            {
                OutputHandler.Error("Phone number Already Exist");
            }
            else
            {
                var result4 = await AuthenticationRepository.Createuser(user, input.password);
                OutputHandler.Handle(new Output("Registraction Successful.."));
            }





        }
    }
}
