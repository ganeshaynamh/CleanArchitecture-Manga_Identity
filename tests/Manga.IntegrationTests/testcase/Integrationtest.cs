using Manga.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Manga.IntegrationTests.testcase
{
    public class Integrationtest : IDisposable
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public Integrationtest()
        {
            SetupClient();
        }

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<StartupDevelopment>());

            Client = _server.CreateClient();

        }
        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}
