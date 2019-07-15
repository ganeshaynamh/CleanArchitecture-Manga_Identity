using Manga.Domain.UserModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Manga.UnitTests.EntitiesTests
{
    public class AspNetUserTest
    {
        [Fact]
        public void Value_NotNull()
        {
            ApplicationUser user = new ApplicationUser
            {

            };
            var context = new ValidationContext(user, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                bool flag = results.Count != 0 ? true : false;
                Assert.True(flag);
            } 
        }
    }
}
