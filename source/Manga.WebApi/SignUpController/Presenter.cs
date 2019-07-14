using Manga.Application.Boundaries.SignUpUser;
using Manga.WebApi.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manga.WebApi.SignUpController
{
    public class Presenter : IOutputHandler
    {
        public IActionResult ViewModel { get; private set; }
        public void Error(string message)
        {
            ViewModel = new ObjectResult(new ErrorMessage(message));
        }

        public void Handle(Output output)
        {
            List<TransactionModel> transactions = new List<TransactionModel>();

            foreach (var item in output.Account.Transactions)
            {
                var transaction = new TransactionModel(
                    item.Amount,
                    item.Description,
                    item.TransactionDate);

                transactions.Add(transaction);
            }

            AccountDetailsModel account = new AccountDetailsModel(
                output.Account.AccountId,
                output.Account.CurrentBalance,
                transactions);

            List<AccountDetailsModel> accounts = new List<AccountDetailsModel>();
            accounts.Add(account);

            SignUpOutput model = new SignUpOutput(
                output.Customer.Id,
                output.Customer.SSN,
                output.Customer.UserName,
                output.Message,
                accounts
            );

            ViewModel = new CreatedAtRouteResult("GetCustomer",
                new
                {
                    customerId = model.CustomerId
                },
                model);

           // ViewModel = new ObjectResult(output);
        }
    }
}
