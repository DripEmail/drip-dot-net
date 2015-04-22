using Drip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropDotNetTests
{
    public class DripClientFixture: IDisposable
    {
        public string ApiKey { get; private set; }
        public string AccountId { get; private set; }
        public DripClient Client { get; private set; }

        public DripClientFixture()
        {
            ApiKey = Environment.GetEnvironmentVariable("DRIP_API_KEY");
            AccountId = Environment.GetEnvironmentVariable("DRIP_ACCOUNT_ID");

            if (string.IsNullOrEmpty(ApiKey))
                throw new InvalidOperationException("DRIP_API_KEY environment variable must be set.");

            if (string.IsNullOrEmpty(AccountId))
                throw new InvalidOperationException("DRIP_ACCOUNT_ID environment variable must be set.");

            Client = new DripClient(ApiKey, AccountId);
        }

        public void Dispose()
        {
        }
    }
}
