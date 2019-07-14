namespace Manga.UnitTests.UseCasesTests
{
    using Manga.Domain;
    using Manga.Domain.ValueObjects;
    using Manga.Infrastructure.InMemoryDataAccess;
    using Manga.Infrastructure.InMemoryDataAccess.Repositories;
    using Manga.Infrastructure.InMemoryGateway;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class RegisterTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            var register = new Register(null, null, null, null);
            Assert.ThrowsAsync<Exception>(async() => await register.Execute(null));
        }

        [Theory]
        [InlineData(300)]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(3300)]
        public async Task Register_WritesOutput_InputIsValid(double amount)
        {
            var ssn = "8608178888";
            var name = "Ivan Paulovich";

            var entityFactory = new DefaultEntitiesFactory();
            var presenter = new Presenter();
            var context = new MangaContext();
            var customerRepository = new CustomerRepository(context);
            var accountRepository = new AccountRepository(context);

            var sut = new Register(
                entityFactory,
                presenter,
                customerRepository,
                accountRepository
            );

            await sut.Execute(new Input(
                ssn,
                name,
                new PositiveAmount(amount)));
            
            var actual = presenter.Registers.First();
            Assert.NotNull(actual);
            Assert.Equal(ssn.ToString(), actual.Customer.SSN);
            Assert.Equal(name.ToString(), actual.Customer.UserName);
            Assert.Equal(amount, actual.Account.CurrentBalance);
        }
    }
}
