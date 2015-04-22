using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace Drip
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
            where TResponse : DripResponse, new()
        {
            var req = new RestRequest(resourceUrl, Method.GET);
            req.AddUrlSegment(urlSegmentKey, urlSegmentValue);
            return Execute<TResponse>(req);
        }

        protected virtual Task<TResponse> GetResourceAsync<TResponse>(string resourceUrl, string urlSegmentKey, string urlSegmentValue, CancellationToken cancellationToken)
            where TResponse : DripResponse, new()
        {
            var req = new RestRequest(resourceUrl, Method.GET);
            req.AddUrlSegment(urlSegmentKey, urlSegmentValue);
            return ExecuteAsync<TResponse>(req, cancellationToken);
        }

        protected virtual TResponse PostResource<TResponse>(string resourceUrl, string requestBodyKey, object requestBody, string urlSegmentKey = null, string urlSegmentValue = null)
            where TResponse : DripResponse, new()
        {
            var req = CreatePostRequest(resourceUrl, requestBodyKey, requestBody, urlSegmentKey, urlSegmentValue);
            return Execute<TResponse>(req);
        }

        protected virtual Task<TResponse> PostResourceAsync<TResponse>(string resourceUrl, string requestBodyKey, object requestBody, CancellationToken cancellationToken, string urlSegmentKey = null, string urlSegmentValue = null)
            where TResponse : DripResponse, new()
        {
            var req = CreatePostRequest(resourceUrl, requestBodyKey, requestBody, urlSegmentKey, urlSegmentValue);
            return ExecuteAsync<TResponse>(req, cancellationToken);
        }

        protected virtual IRestRequest CreatePostRequest(string resourceUrl, string requestBodyKey = null, object requestBody = null, string urlSegmentKey = null, string urlSegmentValue = null)
        {
            var req = new RestRequest(resourceUrl, Method.POST);
            if (requestBodyKey != null && requestBody != null)
                req.AddJsonBody(new { requestBodyKey = requestBody });
            if (urlSegmentKey != null && urlSegmentValue != null)
                req.AddUrlSegment(urlSegmentKey, urlSegmentValue);
            return req;
        }

        protected virtual TResponse Execute<TResponse>(IRestRequest request)
            where TResponse : DripResponse, new()
        {
            var resp = Client.Execute<TResponse>(request);
            resp.Data.ProcessRestResponse(resp);
            return resp.Data;
        }

        protected virtual async Task<TResponse> ExecuteAsync<TResponse>(IRestRequest request, CancellationToken cancellationToken)
            where TResponse : DripResponse, new()
        {
            var resp = await Client.ExecuteTaskAsync<TResponse>(request, cancellationToken);
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
