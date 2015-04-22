using System.Collections.Generic;
using System.Net;

namespace DripDotNet
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
        /// A quick way to check if this DripResponse contains errors.
        /// This library does not throw exceptions for application level errors returned by the REST API.
        /// Check the Errors collection for details on response errors.
        /// </summary>
        /// <returns>True if there are any errors in this DripResponse, otherwise false.</returns>
        public bool HasErrors()
        {
            return Errors != null && Errors.Count > 0;
        }
    }

    /// <summary>
    /// A response that contains DripSubscriber items.
    /// </summary>
    public class DripSubscribersResponse : DripResponse
    {
        /// <summary>
        /// The Subscribers that were returned by the API endpoint.
        /// </summary>
        public List<DripSubscriber> Subscribers { get; set; }
    }

}
