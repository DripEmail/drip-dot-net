using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DripDotNet
{
    public partial class DripClient
    {
        protected const string TrackEventResource = "/{accountId}/events";
        protected const string TrackEventsResource = "/{accountId}/events/batches";
        protected const string EventsRequestBodyKey = "events";

        /// <summary>
        /// Track an event.
        /// See: https://www.getdrip.com/docs/rest-api#record_event
        /// </summary>
        /// <param name="dripEvent">The event to track.</param>
        /// <returns>On success, a DripResponse with StatusCode of Created.</returns>
        public DripResponse TrackEvent(DripEvent dripEvent)
        {
            return PostResource<DripResponse>(TrackEventResource, EventsRequestBodyKey, new DripEvent[]{ dripEvent });
        }

        /// <summary>
        /// Track an event.
        /// See: https://www.getdrip.com/docs/rest-api#record_event
        /// </summary>
        /// <param name="dripEvent">The event to track.</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed successfully, will contain a StatusCode of Created.</returns>
        public Task<DripResponse> TrackEventAsync(DripEvent dripEvent, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostResourceAsync<DripResponse>(TrackEventResource, EventsRequestBodyKey, new DripEvent[] { dripEvent }, cancellationToken);
        }

        /// <summary>
        /// Track a collection of events all at once.
        /// See: https://www.getdrip.com/docs/rest-api#event_batches
        /// </summary>
        /// <param name="dripEvents">An enumerable collection of between 1 and 1000 DripEvents.</param>
        /// <returns>On success, a DripResponse with StatusCode of Created.</returns>
        public DripResponse TrackEvents(IEnumerable<DripEvent> dripEvents)
        {
            return PostResource<DripResponse>(TrackEventsResource, EventsRequestBodyKey, dripEvents.ToArray());
        }

        /// <summary>
        /// Track a collection of events all at once.
        /// See: https://www.getdrip.com/docs/rest-api#event_batches
        /// </summary>
        /// <param name="dripEvents">An enumerable collection of between 1 and 1000 DripEvents.</param>
        /// <param name="cancellationToken">The CancellationToken to be used to cancel the request.</param>
        /// <returns>A Task that, when completed successfully, will contain a StatusCode of Created.</returns>
        public Task<DripResponse> TrackEventsAsync(IEnumerable<DripEvent> dripEvents, CancellationToken cancellationToken = default(CancellationToken))
        {
            return PostResourceAsync<DripResponse>(TrackEventsResource, EventsRequestBodyKey, dripEvents.ToArray(), cancellationToken);
        }
    }
}
