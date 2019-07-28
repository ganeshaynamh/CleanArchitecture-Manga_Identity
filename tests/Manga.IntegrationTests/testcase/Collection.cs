using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Manga.IntegrationTests.testcase
{
    [CollectionDefinition("SystemCollection")]
    public class Collection : ICollectionFixture<Integrationtest>
    {
    }
}
