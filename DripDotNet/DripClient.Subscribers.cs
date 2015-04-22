using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DripDotNet
{
    public partial class DripClient
    {
        /// <summary>
        /// Fetch a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#fetch_subscriber
        /// </summary>
        /// <param name="idOrEmail">Required. The String id or email address of the subscriber.</param>
        /// <returns>A DripSubscribersResponse.</returns>
        public DripSubscribersResponse GetSubscriber(string idOrEmail)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create or update a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#create_or_update_subscriber
        /// </summary>
        /// <param name="subscriber">The DripSubscriber to create or update.</param>
        /// <returns>A DripSubscribersResponse</returns>
        public DripSubscribersResponse CreateOrUpdateSubscriber(ModifyDripSubscriber subscriber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create or update a subscriber.
        /// See: https://www.getdrip.com/docs/rest-api#create_or_update_subscriber
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed, will contain a DripSubscribersResponse.</returns>
        public Task<DripSubscribersResponse> CreateOrUpdateSubscriberAsync(ModifyDripSubscriber subscriber, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
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
        public DripResponse CreateOrUpdateSubscribers(IEnumerable<ModifyDripSubscriber> subscribers)
        {
            throw new NotImplementedException();
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
        public Task<DripResponse> CreateOrUpdateSubscribersAsync(IEnumerable<ModifyDripSubscriber> subscribers, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }

}
