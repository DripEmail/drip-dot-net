using RestSharp;
using System.Threading;
using System.Threading.Tasks;

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

        protected virtual TResponse GetResource<TResponse>(string resourceUrl, string urlSegmentKey, string urlSegmentValue)
            where TResponse: DripResponse, new()
        {
            var req = new RestRequest(resourceUrl, Method.GET);
            req.AddUrlSegment(urlSegmentKey, urlSegmentValue);
            var resp = Client.Execute<TResponse>(req);
            resp.Data.ProcessRestResponse(resp);
            return resp.Data;
        }

        protected virtual async Task<TResponse> GetResourceAsync<TResponse>(string resourceUrl, string urlSegmentKey, string urlSegmentValue, CancellationToken cancellationToken)
            where TResponse : DripResponse, new()
        {
            var req = new RestRequest(resourceUrl, Method.GET);
            req.AddUrlSegment(urlSegmentKey, urlSegmentValue);
            var resp = await Client.ExecuteTaskAsync<TResponse>(req, cancellationToken);
            resp.Data.ProcessRestResponse(resp);
            return resp.Data;
        }

        protected virtual TResponse PostResource<TResponse>(string resourceUrl, object requestBody)
            where TResponse : DripResponse, new()
        {
            var req = new RestRequest(resourceUrl, Method.POST);
            req.AddJsonBody(requestBody);
            var resp = Client.Execute<TResponse>(req);
            resp.Data.ProcessRestResponse(resp);
            return resp.Data;
        }

        protected virtual async Task<TResponse> PostResourceAsync<TResponse>(string resourceUrl, object requestBody, CancellationToken cancellationToken)
            where TResponse : DripResponse, new()
        {
            var req = new RestRequest(resourceUrl, Method.POST);
            req.AddJsonBody(requestBody);
            var resp = await Client.ExecuteTaskAsync<TResponse>(req, cancellationToken);
            resp.Data.ProcessRestResponse(resp);
            return resp.Data;
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
