namespace Manga.Domain.Customers
{
    using System.Collections.Generic;
    using System;
    using Manga.Domain.ValueObjects;

    public class Customer : ICustomer
    { 
        public Guid Id { get; set; }
        public string UserName { get; protected set; }
        public string SSN { get; protected set; }
        public IReadOnlyCollection<Guid> Accounts
        {
            get
            {
                IReadOnlyCollection<Guid> readOnly = _accounts.GetAccountIds();
                return readOnly;
            }
        }

        private AccountCollection _accounts = new AccountCollection();

        public void Register(Guid accountId)
        {
            _accounts.Add(accountId);
        }

        private Customer() { }

        public Customer(string ssn, string Username)
        {
            Id = Guid.NewGuid();
            SSN = ssn;
            UserName = Username;
        }

        public void LoadAccounts(ICollection<Guid> accountIds)
        {
            _accounts = new AccountCollection();
            foreach(var account in accountIds)
                _accounts.Add(account);
        }
    }
}