using Manga.WebApi.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manga.WebApi.SignUpController
{
    public class SignUpOutput
    {
        public Guid CustomerId { get; }
        public string SSN { get; }
        public string UserName { get; }
        public string Message { get; set; }
        public List<AccountDetailsModel> Accounts { get; set; }
        public SignUpOutput(Guid customerId,
            string ssn,
            string name,string message,
            List<AccountDetailsModel> accounts)
        {
            CustomerId = customerId;
            SSN = ssn;
            UserName = name;
            Accounts = accounts;
            Message = message;
        }
    }
}
