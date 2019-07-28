namespace Manga.UnitTests.UseCasesTests
{
    using Xunit;
    using Manga.Application.UseCases;
    using Manga.Infrastructure.InMemoryGateway;
    using Manga.Infrastructure.InMemoryDataAccess.Repositories;
    using Manga.Infrastructure.InMemoryDataAccess;
    using System.Linq;
    using Manga.Domain.ValueObjects;
    using System.Threading.Tasks;
    using Application.Boundaries.Deposit;
    using Manga.Application.Repositories;

    public sealed class DepositTests
    {
        private readonly IAuthenticateRepository authenticateRepository;

        public DepositTests(IAuthenticateRepository authenticateRepository)
        {
            this.authenticateRepository = authenticateRepository;
            //authenticateRepository = new IAuthenticateRepository();
        }

        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(0)]
        [InlineData(-100)]
        public async Task Deposit_ChangesBalance_WhenAccountExists(double amount)
        {
            var presenter = new Presenter();
            var context = new MangaContext();
            var accountRepository = new AccountRepository(context);
            
            var sut = new Deposit(
                presenter,
                accountRepository,
                authenticateRepository
            );

            await sut.Execute(
                new Input(context.DefaultAccountId.ToString(), new PositiveAmount(amount)));


            var output = presenter.Deposits.First();
            Assert.Equal(amount, output.Transaction.Amount);
        }
    }
}
