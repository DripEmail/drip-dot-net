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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Drip
{
    public partial class DripClient
    {
        protected const string ApplyTagToSubscriberResource = "/{accountId}/tags";
        protected const string TagsRequestBodyKey = "tags";
        protected const string RemoveTagFromSubscriberResource = "/{accountId}/subscribers/{subscriberId}/tags/{tag}";
        protected const string TagUrlSegmentKey = "tag";

        /// <summary>
        /// Apply a tag to a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#apply_tag
        /// </summary>
        /// <param name="email">The subscriber's email address.</param>
        /// <param name="tag">The tag to apply. E.g. "Customer"</param>
        /// <returns>On success, a DripResponse with StatusCode of Created.</returns>
        public DripResponse ApplyTagToSubscriber(string email, string tag)
        {
            //This doesn't use a PostResource overload because specifying a return type causes an error
            var req = CreatePostRequest(ApplyTagToSubscriberResource, TagsRequestBodyKey, new DripTag[] { new DripTag { Email = email, Tag = tag } });
            var resp = Client.Execute(req);
            return DripResponse.FromRequestResponse(req, resp);
        }

        /// <summary>
        /// Apply a tag to a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#apply_tag
        /// </summary>
        /// <param name="email">The subscriber's email address.</param>
        /// <param name="tag">The tag to apply. E.g. "Customer"</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed successfully, will contain a StatusCode of NoContent.</returns>
        public async Task<DripResponse> ApplyTagToSubscriberAsync(string email, string tag, CancellationToken cancellationToken = default(CancellationToken))
        {
            //This doesn't use a PostResource overload because specifying a return type causes an error
            var req = CreatePostRequest(ApplyTagToSubscriberResource, TagsRequestBodyKey, new DripTag[] { new DripTag { Email = email, Tag = tag } });
            var resp = await Client.ExecuteAsync(req, cancellationToken);
            return DripResponse.FromRequestResponse(req, resp);
        }

        /// <summary>
        /// Remove a tag from a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#remove_tag
        /// </summary>
        /// <param name="email">The subscriber's email address.</param>
        /// <param name="tag">The tag to remove. E.g. "Customer"</param>
        /// <returns>On success, a DripResponse with a StatusCode of NoContent.</returns>
        public DripResponse RemoveTagFromSubscriber(string email, string tag)
        {
            return Execute<DripResponse>(CreateRemoveTagFromSubscriberRequest(email, tag));
        }

        /// <summary>
        /// Remove a tag from a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#remove_tag
        /// </summary>
        /// <param name="email">The subscriber's email address.</param>
        /// <param name="tag">The tag to remove. E.g. "Customer"</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed successfully, will contain a DripResponse with a StatusCode of NoContent.</returns>
        public Task<DripResponse> RemoveTagFromSubscriberAsync(string email, string tag, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ExecuteAsync<DripResponse>(CreateRemoveTagFromSubscriberRequest(email, tag), cancellationToken);
        }

        protected virtual RestRequest CreateRemoveTagFromSubscriberRequest(string email, string tag)
        {
            var req = CreateRequest(Method.Delete, RemoveTagFromSubscriberResource, null, null, SubscriberIdUrlSegmentKey, email);
            if (tag != null)
                req.AddUrlSegment(TagUrlSegmentKey, tag);
            return req;
        }
    }
}
