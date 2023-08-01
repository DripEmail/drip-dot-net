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

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Drip
{
    public partial class DripClient
    {
        protected const string FetchSubscriberResource = "{accountId}/subscribers/{subscriberId}";
        protected const string CreateOrUpdateSubscriberResource = "{accountId}/subscribers";
        protected const string CreateOrUpdateSubscribersBatchResource = "{accountId}/subscribers/batches";
        protected const string SubscriberIdUrlSegmentKey = "subscriberId";

        /// <summary>
        /// Fetch a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#fetch_subscriber
        /// </summary>
        /// <param name="idOrEmail">Required. The String id or email address of the subscriber.</param>
        /// <returns>A DripSubscribersResponse.</returns>
        public DripSubscribersResponse GetSubscriber(string idOrEmail)
        {
            return GetResource<DripSubscribersResponse>(FetchSubscriberResource, SubscriberIdUrlSegmentKey, idOrEmail);
        }

        /// <summary>
        /// Fetch a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#fetch_subscriber
        /// </summary>
        /// <param name="idOrEmail">Required. The String id or email address of the subscriber.</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed, will contain a DripSubscribersResponse.</returns>
        public Task<DripSubscribersResponse> GetSubscriberAsync(string idOrEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetResourceAsync<DripSubscribersResponse>(FetchSubscriberResource, SubscriberIdUrlSegmentKey, idOrEmail, cancellationToken);
        }

        /// <summary>
        /// Create or update a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#create_or_update_subscriber
        /// </summary>
        /// <param name="subscriber">The DripSubscriber to create or update.</param>
        /// <returns>A DripSubscribersResponse</returns>
        public DripSubscribersResponse CreateOrUpdateSubscriber(ModifyDripSubscriberRequest subscriber)
        {
            return PostResource<DripSubscribersResponse>(CreateOrUpdateSubscriberResource, SubscribersRequestBodyKey, new ModifyDripSubscriberRequest[] { subscriber });
        }

        /// <summary>
        /// Create or update a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#create_or_update_subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed, will contain a DripSubscribersResponse.</returns>
        public Task<DripSubscribersResponse> CreateOrUpdateSubscriberAsync(ModifyDripSubscriberRequest subscriber, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostResourceAsync<DripSubscribersResponse>(CreateOrUpdateSubscriberResource, SubscribersRequestBodyKey, new ModifyDripSubscriberRequest[] { subscriber }, cancellationToken);
        }

        /// <summary>
        /// We recommend using this API endpoint when you need to create or update
        /// a collection of subscribers at once. Note: Since our batch APIs process
        /// requests in the background, there may be a delay between the time you
        /// submit your request and the time your data appears in user interface.
        /// See: https://www.getdrip.com/docs/rest-api#subscriber_batches
        /// </summary>
        /// <param name="subscribers">An enumerable list of ModifyDripSubscribers to create or update.</param>
        /// <returns>A DripResponse.</returns>
        public DripResponse CreateOrUpdateSubscribers(IEnumerable<ModifyDripSubscriberRequest> subscribers)
        {
            return PostBatchResource<ModifyDripSubscriberRequest[]>(CreateOrUpdateSubscribersBatchResource, SubscribersRequestBodyKey, subscribers.ToArray());
        }

        /// <summary>
        /// We recommend using this API endpoint when you need to create or update
        /// a collection of subscribers at once. Note: Since our batch APIs process
        /// requests in the background, there may be a delay between the time you
        /// submit your request and the time your data appears in user interface.
        /// See: https://www.getdrip.com/docs/rest-api#subscriber_batches
        /// </summary>
        /// <param name="subscribers">An enumerable list of ModifyDripSubscribers to create or update.</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed, will contain a DripResponse.</returns>
        public Task<DripResponse> CreateOrUpdateSubscribersAsync(IEnumerable<ModifyDripSubscriberRequest> subscribers, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostBatchResourceAsync<ModifyDripSubscriberRequest[]>(CreateOrUpdateSubscribersBatchResource, SubscribersRequestBodyKey, subscribers.ToArray(), cancellationToken);
        }
    }
}
