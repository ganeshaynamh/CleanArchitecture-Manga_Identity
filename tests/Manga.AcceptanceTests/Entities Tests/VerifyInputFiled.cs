using Manga.Domain.UserModel;
using System.Linq;
using Xunit;

namespace Manga.AcceptanceTests.Entities_Tests
{
   
    public class VerifyInputFiled
    {
        [Fact]
        public void verifyclassattributes()
        {
            InputFieldTest cpv = new InputFieldTest();
            var Appuser = new ApplicationUser
            {
                UserName = "jagdishparmar",
                Email = "jd@gmail.com",
                PhoneNumber = "8866825150",
                SSN ="12345678-9999"

            };
            var errorcount = cpv.myValidation(Appuser).Count();
            Assert.Equal(0, errorcount);


        }
    }
}
