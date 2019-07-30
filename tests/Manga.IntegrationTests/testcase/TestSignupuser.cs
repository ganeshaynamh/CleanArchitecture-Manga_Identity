using FluentAssertions;
using Manga.Domain.UserModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Manga.IntegrationTests.testcase
{
    public class TestSignupuser 
    {
       
        [Fact]
        public async Task Get_all_User()
        {
            using (var client = new SignupUser().Client)
            {
                var response = await client.GetAsync("/api/SignUp");

                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_post()
        {
            using (var client = new SignupUser().Client)
            {
                var response = await client.PostAsync("/api/SignUp", new StringContent(JsonConvert.SerializeObject(new SignUpModel()
                {
                    UserName = "jagdish",
                    Email = "jagdish@gmail.com",
                    PhoneNumber = "8866825150",
                    password = "jagdish@1998"
                    //SSN = "123456789-1234",
                    //InitialAmount = "1200"
                }),Encoding.UTF8));
                //application.json

                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
      
    }
}
