using Manga.Application.Repositories;
using Manga.Application.Boundaries.LoginUser;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Manga.Domain.UserModel;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Manga.Application.UseCases
{
    public class LoginUser :IUseCase
    {
        private readonly IOutputHandler OutputHandler;
        private readonly IAuthenticateRepository AuthenticationRepository;

        public IConfiguration Configuration { get; }
        public LoginUser(IOutputHandler outputHandler, IAuthenticateRepository authenticateRepository, IConfiguration configuration)
        {
            OutputHandler = outputHandler;
            AuthenticationRepository = authenticateRepository;
            Configuration = configuration;

        }

        public async Task Execute(Input input)
        {
            var r1 = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
            var r2 = new Regex(@"^([0-9]{10})$");
            if (!string.IsNullOrEmpty(input.UserName) && r1.IsMatch(input.UserName))
            {
                //var result1 = userManager.Users.SingleOrDefault(r => r.Email == model.UserName);

                //return await logincheck(result1, model.password);

                var result1 = await AuthenticationRepository.FindByEmail(input.UserName);
                if (result1 == null)
                {
                    OutputHandler.Error("Email not Found");
                }
                else
                {
                    var output = await logincheck(result1, input.Password);
                    OutputHandler.Handle(new Output(output.ToString()));

                }

            }
            else if (!string.IsNullOrEmpty(input.UserName) && r2.IsMatch(input.UserName))
            {
                //var result2 = userManager.Users.SingleOrDefault(r => r.PhoneNumber == model.UserName);

                //return await logincheck(result2, model.password);

                var result2 = await AuthenticationRepository.FindByPhoneNumber(input.UserName);
                if (result2 == null)
                {
                    OutputHandler.Error("PhoneNumber Not Found");
                }
                else
                {
                    var output1 = await logincheck(result2, input.Password);
                    OutputHandler.Handle(new Output(output1.ToString()));
                }

                

            }
            else
            {
                var result3 = await AuthenticationRepository.FindByName(input.UserName);
                if (result3 == null)
                {
                    OutputHandler.Error("UserName not Found");
                }
                else
                {
                    var output2 = await logincheck(result3, input.Password);
                    OutputHandler.Handle(new Output(output2.ToString()));
                }
                
            }


        }

        private async Task<object> logincheck(IdentityUser applicationUser, string password)
        {
            if (applicationUser == null)
            {
                OutputHandler.Error("Invaild username");
                return null;
            }
            var appuser = AuthenticationRepository.FindByName(applicationUser.UserName);
            //var appuser = userManager.FindByNameAsync(applicationUser.UserName);
            if (appuser != null)
            {

                // var result = await SignInManager.PasswordSignInAsync(applicationUser, password, false, false);
                var result = (SignInResult)await AuthenticationRepository.Logincheck1(applicationUser, password);
                if (result.IsNotAllowed)
                {
                    OutputHandler.Error("User is allowed or not");
                    return null;
                }
                if (result.IsLockedOut)
                {
                    OutputHandler.Error("Account Locked");
                    return null;
                }
                if (result.RequiresTwoFactor)
                {
                    OutputHandler.Error("Login required two step auth");
                    return null;
                }
                if (result.Succeeded)
                {
                    var token = await AuthenticationToken();
                    //  return Ok("Login Success");
                    return token;
                }
                else
                {
                    OutputHandler.Error("Invaild Password..");
                    return null;
                }
            }
            else
            {
                OutputHandler.Error("Invaild username");
                return null;
            }
        

        }
        private async Task<Object> AuthenticationToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["JwtKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
