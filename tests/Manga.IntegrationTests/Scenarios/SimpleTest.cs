using Manga.IntegrationTests.testcase;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Manga.IntegrationTests.Scenarios
{
    [Collection("SystemCollection")]
    public class SimpleTest
    {
        private readonly Integrationtest integrationTests;
        public SimpleTest()
        {
           // integrationTests = integrationTest;
            integrationTests = new Integrationtest();

        }
        [Fact]
        public async Task SimpleReturnOkResponse()
        {
            var response = await integrationTests.Client.GetAsync("/SimpleTest");
            //response.EnsureSuccessStatusCode();
            //response.StatusCode.Should().Be(HttpStatusCode.OK);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

    }
}
