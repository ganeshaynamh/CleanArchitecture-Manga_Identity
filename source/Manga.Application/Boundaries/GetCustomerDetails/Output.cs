namespace Manga.Application.Boundaries.GetCustomerDetails
{
    using System.Collections.Generic;
    using System;
    using Manga.Domain.Customers;

    public sealed class Output
    {
        public Guid CustomerId { get; }
        public string SSN { get; }
        public string UserName { get; }
        public IReadOnlyList<Account> Accounts { get; }

        public Output(
            ICustomer customer,
            List<Account> accounts)
        {
            Customer customerEntity = (Customer) customer;
            CustomerId = customerEntity.Id;
            SSN = customerEntity.SSN.ToString();
            UserName = customerEntity.UserName.ToString();
            Accounts = accounts;
        }
    }
}