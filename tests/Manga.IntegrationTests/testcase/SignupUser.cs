using Manga.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Manga.IntegrationTests.testcase
{
    public class SignupUser : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; private set; }

        public SignupUser()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<StartupDevelopment>());

            Client = server.CreateClient();
        }
        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }


    }
}
