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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Drip
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
            return PostBatchResource<DripEvent[]>(TrackEventsResource, EventsRequestBodyKey, dripEvents.ToArray());
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
            return PostBatchResourceAsync<DripEvent[]>(TrackEventsResource, EventsRequestBodyKey, dripEvents.ToArray(), cancellationToken);
        }
    }
}
