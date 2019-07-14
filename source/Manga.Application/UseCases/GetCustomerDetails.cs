namespace Manga.Application.UseCases
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using Manga.Application.Boundaries.GetCustomerDetails;
    using Manga.Application.Repositories;
    using Manga.Domain.Accounts;
    using Manga.Domain.Customers;
    using Manga.Application.Common;

    public sealed class GetCustomerDetails : IUseCase
    {
        private readonly IOutputHandler _outputHandler;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthenticateRepository authenticateRepository;

        public GetCustomerDetails(
            IOutputHandler outputHandler,
            ICustomerRepository customerRepository,
            IAccountRepository accountRepository,
            IAuthenticateRepository authenticateRepository)
        {
            _outputHandler = outputHandler;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            this.authenticateRepository = authenticateRepository;
        }

        public async Task Execute(Input input)
        {
            Guid CustomerId;
            var result = CommonAccess.commonAccessCustomer(input.CustomerId, authenticateRepository).Result.ToString();
            if (!Guid.TryParse(result, out CustomerId))
            {
                _outputHandler.Error(result);
                return;
            }
            ICustomer customer = await _customerRepository.Get(CustomerId);

            if (customer == null)
            {
                _outputHandler.Error($"The customer {CustomerId} does not exists or is not processed yet.");
                return;
            }

            List<Boundaries.GetCustomerDetails.Account> accounts = new List<Boundaries.GetCustomerDetails.Account>();

            foreach (Guid accountId in customer.Accounts)
            {
                IAccount account = await _accountRepository.Get(accountId);

                if (account != null)
                {
                    Boundaries.GetCustomerDetails.Account accountOutput = new Boundaries.GetCustomerDetails.Account(account);
                    accounts.Add(accountOutput);
                }
            }

            Output output = new Output(customer, accounts);
            _outputHandler.Handle(output);
        }
    }
}