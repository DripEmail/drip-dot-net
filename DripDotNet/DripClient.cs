/*
 The MIT License (MIT)

 Copyright (c) 2015 - 2017 Avenue 81 Inc. d/b/a Leadpages, All Rights Reserved

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
*/

using Drip.Protocol;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Drip
{
    public partial class DripClient
    {
        protected const string BatchRequestBodyKey = "batches";

        public const string BaseUrl = "https://api.getdrip.com/v2/";
        private string ApiKey { get; set; }
        public string AccessToken { get; set; }
        private string AccountId { get; set; }

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

        public DripClient(string apiKey = null, string accountId = null, string accessToken = null)
        {
            ApiKey = apiKey;
            AccountId = accountId;
            AccessToken = accessToken;
        }

        protected virtual TResponse GetResource<TResponse>(string resourceUrl, string urlSegmentKey, string urlSegmentValue)
            where TResponse : DripResponse, new()
        {
            var req = new RestRequest(resourceUrl, Method.Get);
            req.AddUrlSegment(urlSegmentKey, urlSegmentValue);
            return Execute<TResponse>(req);
        }

        protected virtual Task<TResponse> GetResourceAsync<TResponse>(string resourceUrl, string urlSegmentKey, string urlSegmentValue, CancellationToken cancellationToken)
            where TResponse : DripResponse, new()
        {
            var req = new RestRequest(resourceUrl, Method.Get);
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

        protected virtual DripResponse PostBatchResource<TData>(string resourceUrl, string key, TData data)
        {
            var body = new Dictionary<string, TData>[] { new Dictionary<string, TData> { { key, data } } };
            var req = CreatePostRequest(resourceUrl, BatchRequestBodyKey, body);
            var resp = Client.Execute(req);
            return DripResponse.FromRequestResponse(req, resp);
        }

        protected async virtual Task<DripResponse> PostBatchResourceAsync<TData>(string resourceUrl, string key, TData data, CancellationToken cancellationToken)
        {
            var body = new Dictionary<string, TData>[] { new Dictionary<string, TData> { { key, data } } };
            var req = CreatePostRequest(resourceUrl, BatchRequestBodyKey, body);
            var resp = await Client.ExecuteAsync(req, cancellationToken);
            return DripResponse.FromRequestResponse(req, resp);
        }

        protected virtual RestRequest CreatePostRequest(string resourceUrl, string requestBodyKey = null, object requestBody = null, string urlSegmentKey = null, string urlSegmentValue = null)
        {
            return CreateRequest(Method.Post, resourceUrl, requestBodyKey, requestBody, urlSegmentKey, urlSegmentValue);
        }

        protected virtual RestRequest CreateRequest(Method method, string resourceUrl, string requestBodyKey = null, object requestBody = null, string urlSegmentKey = null, string urlSegmentValue = null)
        {
            var req = new RestRequest(resourceUrl, method);

            if (requestBodyKey != null && requestBody != null)
                req.AddJsonBody(new Dictionary<string, object> { { requestBodyKey, requestBody } });
            if (urlSegmentKey != null && urlSegmentValue != null)
                req.AddUrlSegment(urlSegmentKey, urlSegmentValue);
            return req;
        }

        protected virtual TResponse Execute<TResponse>(RestRequest request)
            where TResponse : DripResponse, new()
        {
            var resp = Client.Execute<TResponse>(request);
            return DripResponse.FromRequestResponse<TResponse>(request, resp);
        }

        protected virtual async Task<TResponse> ExecuteAsync<TResponse>(RestRequest request, CancellationToken cancellationToken)
            where TResponse : DripResponse, new()
        {
            var resp = await Client.ExecuteAsync<TResponse>(request, cancellationToken);
            return DripResponse.FromRequestResponse<TResponse>(request, resp);
        }

        protected virtual RestClient CreateRestClient()
        {
            var options = new RestClientOptions();
            options.UserAgent = "Drip DotNet v#" + typeof(DripClient).Assembly.GetName().Version.ToString();
            options.BaseUrl = new System.Uri(BaseUrl);

            // TODO: Fix this once we unblock the customer
            var addHeaderThing = false;

            if (string.IsNullOrEmpty(AccessToken))
                options.Authenticator = new HttpBasicAuthenticator(ApiKey, string.Empty);
            else
                addHeaderThing = true;

            JsonSerializerSettings defaultSettings = new JsonSerializerSettings
            {
                ContractResolver = new LcaseUnderscoreMappingResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            var client = new RestClient(
                options,
                configureSerialization: s => s.UseNewtonsoftJson(defaultSettings)
            );
            client.AddDefaultHeader("Content-Type", "application/vnd.api+json");
            client.AddDefaultUrlSegment("accountId", AccountId);

            if (addHeaderThing)
                client.AddDefaultHeader("Authorization", "Bearer #" + AccessToken);

            return client;
        }
    }
}
