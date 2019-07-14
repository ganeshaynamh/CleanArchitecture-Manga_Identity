using Manga.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Manga.Application.Common
{
    public static class CommonAccess
    {
        public static async Task<object> commonAccessAccount(string AccountId,IAuthenticateRepository authenticateRepository,IAccountRepository accountRepository)
        {
            Guid guid;
            bool isEmail = Regex.IsMatch(AccountId, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            bool ismobile = Regex.IsMatch(AccountId, @"\+?[0-9]{10}");
            bool IsGuid = Guid.TryParse(AccountId, out guid);

            if (isEmail)

            {
                var emailExist = await authenticateRepository.FindByEmail(AccountId);
                if (emailExist != null)
                {
                    return await accountRepository.GetAccountId(Guid.Parse(emailExist.Id));
                }
                else
                {
                    return "Email ID Not Found";
                }
            }
            else if (ismobile)
            {
                var PhoneExist = await authenticateRepository.FindByPhoneNumber(AccountId);
                if (PhoneExist != null)
                {
                    return await accountRepository.GetAccountId(Guid.Parse(PhoneExist.Id));
                }
                else
                {
                    return "Phone number Not Found";
                }

            }
            else if (IsGuid)
            {
                return Guid.Parse(AccountId);
            }
            else if (!isEmail && !ismobile  && !IsGuid)
            {
                var UserName = await authenticateRepository.FindByName(AccountId);
                if (UserName != null)
                {
                    return await accountRepository.GetAccountId(Guid.Parse(UserName.Id));
                }
                else
                {
                    return "Phone number Not Found";
                }
            }
            else
            {
                return "UnExpected Error";
            }

            
        }
        public static async Task<object> commonAccessCustomer(string CustomerId, IAuthenticateRepository authenticateRepository)
        {
            Guid guid;
            bool isEmail = Regex.IsMatch(CustomerId, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            bool ismobile = Regex.IsMatch(CustomerId, @"\+?[0-9]{10}");
            bool IsGuid = Guid.TryParse(CustomerId, out guid);

            if (isEmail)

            {
                var emailExist = await authenticateRepository.FindByEmail(CustomerId);
                if (emailExist != null)
                {
                    return Guid.Parse(emailExist.Id);
                }
                else
                {
                    return "Email ID Not Found";
                }
            }
            else if (ismobile)
            {
                var PhoneExist = await authenticateRepository.FindByPhoneNumber(CustomerId);
                if (PhoneExist != null)
                {
                    return Guid.Parse(PhoneExist.Id);
                }
                else
                {
                    return "Phone number Not Found";
                }

            }
            else if (IsGuid)
            {
                return Guid.Parse(CustomerId);
            }
            else if (!isEmail && !ismobile && !IsGuid)
            {
                var UserName = await authenticateRepository.FindByName(CustomerId);
                if (UserName != null)
                {
                    return Guid.Parse(UserName.Id);
                }
                else
                {
                    return "Phone number Not Found";
                }
            }
            else
            {
                return "UnExpected Error";
            }


        }

    }
}
