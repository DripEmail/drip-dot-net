using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DripDotNet
{
    public partial class DripClient
    {
        /// <summary>
        /// Track an event.
        /// See: https://www.getdrip.com/docs/rest-api#record_event
        /// </summary>
        /// <param name="dripEvent">The event to track.</param>
        /// <returns>On success, a DripResponse with StatusCode of Created.</returns>
        public DripResponse TrackEvent(DripEvent dripEvent)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Track a collection of events all at once.
        /// See: https://www.getdrip.com/docs/rest-api#event_batches
        /// </summary>
        /// <param name="dripEvents">An enumerable collection of between 1 and 1000 DripEvents.</param>
        /// <returns>On success, a DripResponse with StatusCode of Created.</returns>
        public DripResponse TrackEvents(IEnumerable<DripEvent> dripEvents)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
