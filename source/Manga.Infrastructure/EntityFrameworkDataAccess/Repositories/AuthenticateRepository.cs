using Manga.Application.Repositories;
using Manga.Domain.UserModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Infrastructure.EntityFrameworkDataAccess.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private UserManager<IdentityUser> UserManager;
        private SignInManager<IdentityUser> SignInManager;

        public AuthenticateRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public async Task<object> Createuser(IdentityUser applicationUser, string password)
        {
            return await UserManager.CreateAsync(applicationUser, password);
        }

        public async Task<IdentityUser> FindByEmail(string email)
        {
            return UserManager.Users.SingleOrDefault(r => r.Email == email);
        }

        public async Task<IdentityUser> FindByName(string name)
        {
            return UserManager.Users.SingleOrDefault(r => r.UserName == name);
        }

        public async Task<IdentityUser> FindByPhoneNumber(string phonenumber)
        {
            return UserManager.Users.SingleOrDefault(r => r.PhoneNumber == phonenumber);
        }

        public async Task<SignInResult> Logincheck1(IdentityUser applicationUser, string password)
        {
            return await SignInManager.PasswordSignInAsync(applicationUser, password, false, false);
        }
    }
}
