using RestSharp;

namespace DripDotNet
{
    public partial class DripClient
    {
        public const string BaseUrl = "https://api.getdrip.com/v2/";
        public string ApiKey { get; set; }
        public string AccessToken { get; set; }
        public string AccountId { get; set; }

        private RestClient client;
        protected RestClient Client
        {
            get
            {
                if (client == null)
                    client = CreateRestClient();
                return client;
            }
        }

        public DripClient()
        {
        }

        public DripClient(string accountId = null, string apiKey = null, string accessToken = null)
        {
            ApiKey = apiKey;
            AccountId = accountId;
            AccessToken = accessToken;
        }

        protected virtual RestClient CreateRestClient()
        {
            var client = new RestClient(BaseUrl);
            client.AddDefaultHeader("Content-Type", "application/vnd.api+json");
            client.AddDefaultHeader("Accept", "*/*");
            client.UserAgent = "Drip DotNet v#" + typeof(DripClient).Assembly.GetName().Version.ToString();
            client.AddDefaultUrlSegment("accountId", AccountId);

            if (string.IsNullOrEmpty(AccessToken))
                client.Authenticator = new HttpBasicAuthenticator(ApiKey, string.Empty);
            else
                client.AddDefaultHeader("Authorization", "Bearer #" + AccessToken);

            return client;
        }
    }
}
