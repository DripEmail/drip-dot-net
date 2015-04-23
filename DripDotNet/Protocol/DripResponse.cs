using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Drip
{
    /// <summary>
    /// Represents the response received from the Drip REST API. Subclasses of DripResponse
    /// contain strongly typed collections of the various resources.
    /// 
    /// This is returned from every method on the DripClient that makes a REST call. 
    /// 
    /// This library does not throw exceptions for application level errors returned by the
    /// REST API. Check the Errors collection for details on response errors.
    /// </summary>
    public class DripResponse
    {
        /// <summary>
        /// Pagination and query metadata included on resopnses containing lists of items.
        /// </summary>
        public DripMeta Meta { get; set; }

        /// <summary>
        /// Links to resources that are relevant to this response.
        /// </summary>
        public Dictionary<string, string> Links { get; set; }

        /// <summary>
        /// The Http status code returned with the response.
        /// This library does not throw exceptions for application level errors returned by the REST API.
        /// Check the Errors collection for details on response errors.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The collection of application level errors returned from the REST API.
        /// </summary>
        public List<DripError> Errors { get; set; }

        /// <summary>
        /// The underlying response that was received.
        /// You shouldn't need to use this, but it might be helpful for debugging.
        /// </summary>
        public IRestResponse OriginalResponse { get; set; }

        /// <summary>
        /// The underlying request that was sent
        /// You shouldn't need to use this, but it might be helpful for debugging.
        /// </summary>
        public IRestRequest OriginalRequest { get; set; }

        /// <summary>
        /// A quick way to check if this DripResponse contains errors.
        /// This library does not throw exceptions for application level errors returned by the REST API.
        /// Check the Errors collection for details on response errors.
        /// </summary>
        /// <returns>True if there are any errors in this DripResponse, otherwise false.</returns>
        public bool HasErrors()
        {
            return Errors != null && Errors.Count > 0;
        }

        public bool HasSuccessStatusCode()
        {
            return (int)StatusCode < 400 && !HasErrors();
        }

        internal protected virtual void ProcessRestResponse(IRestRequest restRequest, IRestResponse restResponse)
        {
            OriginalRequest = restRequest;
            OriginalResponse = restResponse;

            if (restResponse == null) return;

            StatusCode = restResponse.StatusCode;
        }
    }
}
