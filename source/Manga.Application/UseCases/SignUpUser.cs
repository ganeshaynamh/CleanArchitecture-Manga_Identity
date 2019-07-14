using Manga.Application.Boundaries.SignUpUser;
using Manga.Application.Repositories;
using Manga.Domain;
using Manga.Domain.Accounts;
using Manga.Domain.UserModel;
using Manga.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Manga.Application.UseCases
{
    public class SignUpUser : IUseCase
    {

        private readonly IOutputHandler OutputHandler;
        private readonly IAuthenticateRepository AuthenticationRepository;
        private readonly IEntitiesFactory _EntityFactory;
        private readonly IAccountRepository _AccountRepository;

        public IConfiguration Configuration { get; }
        public SignUpUser(IOutputHandler outputHandler,
            IAuthenticateRepository authenticateRepository,
            IConfiguration configuration,
            IEntitiesFactory _entityFactory,
            IAccountRepository _accountRepository
            )
        {
            OutputHandler = outputHandler;
            AuthenticationRepository = authenticateRepository;
            Configuration = configuration;
            _EntityFactory = _entityFactory;
            _AccountRepository = _accountRepository;
        }

        public async Task Execute(Input input)
        {
            if (input == null)
            {
                OutputHandler.Error("Input is null.");
                return;
            }


            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = input.UserName,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
                SSN = input.SSN
                
            };

            var customer = _EntityFactory.NewCustomer(Guid.Parse(user.Id),input.SSN, input.UserName);
            var account = _EntityFactory.NewAccount(customer.Id);

            var Email = await AuthenticationRepository.FindByEmail(input.Email);
            var Username = await AuthenticationRepository.FindByName(input.UserName);
            var Phone = await AuthenticationRepository.FindByPhoneNumber(input.PhoneNumber);
            var SSN = await AuthenticationRepository.FindBySSN(input.SSN);

            if (Email != null)
            {
                OutputHandler.Error("Email Already Exist");
            }
            if (Username != null)
            {
                OutputHandler.Error("Username Already Exist");
            }
            if (Phone != null)
            {
                OutputHandler.Error("Phone number Already Exist");
            }
            if (SSN != null)
            {
                OutputHandler.Error("SSN Name Already Exist");
            }
            else
            {
                var result4 = await AuthenticationRepository.Createuser(user, input.password);

                ICredit credit = account.Deposit(new PositiveAmount(input.InitialAmount));
                if (credit == null)
                {
                    OutputHandler.Error("An error happened when depositing the amount.");
                    return;
                }

                customer.Register(account.Id);
                await _AccountRepository.Add(account, credit);

                Output output = new Output(customer, account, "Registraction Successful..");
                OutputHandler.Handle(output);
            }





        }
    }
}
