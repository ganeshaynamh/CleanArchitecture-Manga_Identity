using Manga.Domain.UserModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Application.Repositories
{
    public interface IAuthenticateRepository
    {
        Task<IdentityUser> FindByName(string name);
        Task<IdentityUser> FindByPhoneNumber(string phonenumber);

        Task<IdentityUser> FindByEmail(string email);

        Task<SignInResult> Logincheck1(IdentityUser applicationUser, string password);
        Task<object> Createuser(IdentityUser applicationUser, string password);
    }
}
