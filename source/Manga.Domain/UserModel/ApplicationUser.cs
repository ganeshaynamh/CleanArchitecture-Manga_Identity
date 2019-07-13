using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Manga.Domain.UserModel
{
    public class ApplicationUser : IdentityUser
    {
        public string SSN { get; set; }
    }
}
