using Manga.Domain.UserModel;
using NUnit.Framework;
using System.Linq;


namespace Manga.AcceptanceTests.Entities_Tests
{
    [TestFixture]
    public class VerifyInputFiled
    {
        [Test]
        public void verifyclassattributes()
        {
            InputFieldTest cpv = new InputFieldTest();
            var Appuser = new ApplicationUser
            {
                UserName = "jagdishparmar",
                Email = "jd@gmail.com",
                PhoneNumber = "8866825150"

            };
            var errorcount = cpv.myValidation(Appuser).Count();
            Assert.AreEqual(0, errorcount);


        }
    }
}
