namespace Manga.Application.UseCases
{
    using System;
    using System.Threading.Tasks;
    using Manga.Application.Boundaries.Deposit;
    using Manga.Application.Common;
    using Manga.Application.Repositories;
    using Manga.Domain.Accounts;

    public sealed class Deposit : IUseCase
    {
        private readonly IOutputHandler _outputHandler;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthenticateRepository authenticateRepository;

        public Deposit(
            IOutputHandler outputHandler,
            IAccountRepository accountRepository,
            IAuthenticateRepository authenticateRepository)
        {
            _outputHandler = outputHandler;
            _accountRepository = accountRepository;
            this.authenticateRepository = authenticateRepository;
        }

        public async Task Execute(Input input)
        {
            Guid AccountId;
            var result = CommonAccess.commonAccessAccount(input.AccountId, authenticateRepository, _accountRepository).Result.ToString();
            if (!Guid.TryParse(result, out AccountId))
            {
                _outputHandler.Error(result);
                return;
            }
            IAccount account = await _accountRepository.Get(AccountId);
            if (account == null)
            {
                _outputHandler.Error($"The account {AccountId} does not exists or is already closed.");
                return;
            }

            ICredit credit = account.Deposit(input.Amount);

            await _accountRepository.Update(account, credit);

            Output output = new Output(
                credit,
                account.GetCurrentBalance());

            _outputHandler.Handle(output);
        }
    }
}