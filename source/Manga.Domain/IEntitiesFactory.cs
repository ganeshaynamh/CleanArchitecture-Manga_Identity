namespace Manga.Domain
{
    using System;
    using Manga.Domain.Accounts;
    using Manga.Domain.Customers;
    using Manga.Domain.ValueObjects;

    public interface IEntitiesFactory
    {
        ICustomer NewCustomer(string ssn, string UserName);
        IAccount NewAccount(Guid customerId);
    }
}