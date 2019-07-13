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
        private UserManager<ApplicationUser> UserManager;
        private SignInManager<ApplicationUser> SignInManager;

        public AuthenticateRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public async Task<object> Createuser(ApplicationUser applicationUser, string password)
        {
            return await UserManager.CreateAsync(applicationUser, password);
        }

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            return UserManager.Users.SingleOrDefault(r => r.Email == email);
        }

        public async Task<ApplicationUser> FindByName(string name)
        {
            return UserManager.Users.SingleOrDefault(r => r.UserName == name);
        }

        public async Task<ApplicationUser> FindByPhoneNumber(string phonenumber)
        {
            return UserManager.Users.SingleOrDefault(r => r.PhoneNumber == phonenumber);
        }

        public async Task<ApplicationUser> FindBySSN(string ssn)
        {
            return UserManager.Users.SingleOrDefault(r => r.SSN == ssn);
        }

        public async Task<SignInResult> Logincheck1(ApplicationUser applicationUser, string password)
        {
            return await SignInManager.PasswordSignInAsync(applicationUser, password, false, false);
        }
    }
}
