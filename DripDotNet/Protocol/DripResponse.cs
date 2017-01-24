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

        internal protected static DripResponse FromRequestResponse(IRestRequest restRequest, IRestResponse restResponse)
        {
            var result = new DripResponse();

            result.OriginalRequest = restRequest;
            result.OriginalResponse = restResponse;
            result.StatusCode = restResponse.StatusCode;

            return result;
        }

        internal protected static TResponse FromRequestResponse<TResponse>(IRestRequest restRequest, IRestResponse<TResponse> restResponse)
            where TResponse : DripResponse, new()
        {
            var result = restResponse.Data ?? new TResponse();

            result.OriginalRequest = restRequest;
            result.OriginalResponse = restResponse;
            result.StatusCode = restResponse.StatusCode;

            return result;
        }
    }
}
