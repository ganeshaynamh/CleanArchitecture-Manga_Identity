using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Manga.Domain.UserModel
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Id { get; set; }

        [ForeignKey("Id")]
        public ApplicationUser User { get; set; }
        public string SSN { get; set; }
    }
}
