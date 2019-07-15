using Manga.Domain.Customers;
using Manga.Domain.UserModel;
using Manga.Infrastructure.InMemoryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Infrastructure.InMemoryGateway.Repositories
{
    public class AuthenticationRepository
    {
        private readonly MangaContext mangaContext;

        public AuthenticationRepository(MangaContext mangaContext)
        {
            this.mangaContext = mangaContext;
        }
        public async Task<object> Createuser(ApplicationUser applicationUser, string password)
        {
            applicationUser.PasswordHash = password;
            mangaContext.ApplicationUsers.Add(applicationUser);
            await Task.CompletedTask;
            return applicationUser;
        }

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            return mangaContext.ApplicationUsers.Where(e => e.Email == email).SingleOrDefault();
        }

        public async Task<ApplicationUser> FindByName(string name)
        {
            return mangaContext.ApplicationUsers.Where(e => e.UserName == name).SingleOrDefault();
        }

        public async Task<ApplicationUser> FindByPhoneNumber(string phonenumber)
        {
            return mangaContext.ApplicationUsers.Where(e => e.PhoneNumber == phonenumber).SingleOrDefault();
        }

        public async Task<ApplicationUser> FindBySSN(string ssn)
        {
            return mangaContext.ApplicationUsers.Where(e => e.SSN == ssn).SingleOrDefault();
        }

        public async Task<ICustomer> GetCustomer(Guid id)
        {
            ApplicationUser user = mangaContext.ApplicationUsers.Where(e => e.Id == id.ToString()).SingleOrDefault();
      

            var customer = new Customer(Guid.Parse(user.Id), user.SSN, user.UserName);

            var accounts = mangaContext.Accounts
                .Where(e => e.CustomerId == id)
                .Select(e => e.Id)
                .ToList();

            customer.LoadAccounts(accounts);

            return customer;
        }

        //public async Task<SignInResult> Logincheck1(ApplicationUser applicationUser, string password)
        //{
        //    return await SignInManager.PasswordSignInAsync(applicationUser, password, false, false);
        //}
    }
}
