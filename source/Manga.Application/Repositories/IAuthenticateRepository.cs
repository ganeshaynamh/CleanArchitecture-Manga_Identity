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
        Task<ApplicationUser> FindByName(string name);
        Task<ApplicationUser> FindByPhoneNumber(string phonenumber);

        Task<ApplicationUser> FindByEmail(string email);

        Task<ApplicationUser> FindBySSN(string ssn);

        Task<SignInResult> Logincheck1(ApplicationUser applicationUser, string password);
        Task<object> Createuser(ApplicationUser applicationUser, string password);
    }
}
