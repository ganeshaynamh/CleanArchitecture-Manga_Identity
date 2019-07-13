using Manga.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manga.Domain.UserModel
{
    public interface IAntiCorruption
    {
        Customer Translate(ApplicationUser applicationUser);
    }
}
