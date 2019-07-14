namespace Manga.Application.Boundaries.SignUpUser
{
    using System.Collections.Generic;
    using System;
    using Manga.Domain.Customers;

    public sealed class Customer
    {
        public Guid Id { get; }
        public string SSN { get; }
        public string UserName { get; }
        public IReadOnlyList<Account> Accounts { get; }

        public Customer(
            ICustomer customer,
            List<Account> accounts)
        {
            var customerEntity = (Domain.Customers.Customer) customer;
            Id = customerEntity.Id;
            SSN = customerEntity.SSN.ToString();
            UserName = customerEntity.UserName.ToString();
            Accounts = accounts;
        }
    }
}