namespace Manga.Infrastructure.EntityFrameworkDataAccess
{
    using System.Threading.Tasks;
    using System;
    using Manga.Application.Repositories;
    using Manga.Domain.Customers;
    using System.Linq;
    using Manga.Domain.UserModel;

    public sealed class CustomerRepository : ICustomerRepository, IAntiCorruption
    {
        private readonly MangaContext _context;

        public CustomerRepository(MangaContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(ICustomer customer)
        {
           await _context.Users.AddAsync((Customer)customer);
            await _context.SaveChangesAsync();
        }

        public async Task<ICustomer> Get(Guid id)
        {
            ApplicationUser user = await _context.ApplicationUsers
                .FindAsync(id.ToString());

            var customer = Translate(user);

            var accounts = _context.Accounts
                .Where(e => e.CustomerId == id)
                .Select(e => e.Id)
                .ToList();

            customer.LoadAccounts(accounts);

            return customer;
        }

        public async Task Update(ICustomer customer)
        {
            _context.Users.Update((Customer)customer);
            await _context.SaveChangesAsync();
        }
        public Customer Translate(ApplicationUser user)
        {
            return new Customer(user.SSN,user.UserName);
        }
    }
}