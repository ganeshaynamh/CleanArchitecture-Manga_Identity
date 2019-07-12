namespace Manga.Domain
{
    using System;
    using Manga.Domain.Accounts;
    using Manga.Domain.Customers;
    using Manga.Domain.ValueObjects;

    public sealed class DefaultEntitiesFactory : IEntitiesFactory
    {
        public IAccount NewAccount(Guid customerId)
        {
            var account = new Account(customerId);
            return account;
        }

        public ICustomer NewCustomer(string ssn, string UserName)
        {
            var customer = new Customer(ssn, UserName);
            return customer;
        }
    }
}