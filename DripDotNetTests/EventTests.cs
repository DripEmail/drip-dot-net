/*
 The MIT License (MIT)
 
 Copyright (c) 2015 Drip
 
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
using Drip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DripDotNetTests
{
    public class EventTests : IClassFixture<DripClientFixture>, IClassFixture<SubscriberFactoryFixture>
    {
        DripClientFixture dripClientFixture;
        SubscriberFactoryFixture subscriberFactoryFixture;

        public EventTests(DripClientFixture dripClientFixture, SubscriberFactoryFixture subscriberFactoryFixture)
        {
            this.dripClientFixture = dripClientFixture;
            this.subscriberFactoryFixture = subscriberFactoryFixture;
        }

        [Fact]
        public void CanTrackEvent()
        {
            var subscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();
            var subscriberResult = dripClientFixture.Client.CreateOrUpdateSubscriber(subscriber);
            DripAssert.Success(subscriberResult);

            //simple event
            var result = dripClientFixture.Client.TrackEvent(new DripEvent { Action="hello", Email=subscriber.Email });
            DripAssert.Success(result, HttpStatusCode.NoContent);

            //more complex event
            result = dripClientFixture.Client.TrackEvent(
                new DripEvent { 
                    Action = "hello2", 
                    Email = subscriber.Email, 
                    PotentialLead = true,
                    Properties = new Dictionary<string,string>{{ "value", "100" }}
                });
            DripAssert.Success(result, HttpStatusCode.NoContent);

            //There is no API for reading events so we can't verify the contents from here. Look in your Dashboard.
        }

        [Fact]
        public void CanTrackEventForNewUser()
        {
            //event for brand new subscriber
            var newSubscriber = subscriberFactoryFixture.GetRandomEmailAddress();
            var result = dripClientFixture.Client.TrackEvent(new DripEvent { Action = "hello", Email = newSubscriber });
            DripAssert.Success(result, HttpStatusCode.NoContent);

            var subscriberResult = dripClientFixture.Client.GetSubscriber(newSubscriber);
            DripAssert.Success(subscriberResult);
        }

        [Fact]
        public async Task CanTrackEventAsync()
        {
            var subscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();
            var subscriberResult = await dripClientFixture.Client.CreateOrUpdateSubscriberAsync(subscriber);
            DripAssert.Success(subscriberResult);

            //simple event
            var result = await dripClientFixture.Client.TrackEventAsync(new DripEvent { Action = "hello", Email = subscriber.Email });
            DripAssert.Success(result, HttpStatusCode.NoContent);

            //more complex event
            result = await dripClientFixture.Client.TrackEventAsync(
                new DripEvent
                {
                    Action = "hello2",
                    Email = subscriber.Email,
                    PotentialLead = true,
                    Properties = new Dictionary<string, string> { { "value", "100" } }
                });
            DripAssert.Success(result, HttpStatusCode.NoContent);

            //There is no API for reading events so we can't verify the contents from here. Look in your Dashboard.
        }

        [Fact]
        public async Task CanTrackEventForNewUserAsync()
        {
            //event for brand new subscriber
            var newSubscriber = subscriberFactoryFixture.GetRandomEmailAddress();
            var result = await dripClientFixture.Client.TrackEventAsync(new DripEvent { Action = "hello", Email = newSubscriber });
            DripAssert.Success(result, HttpStatusCode.NoContent);

            var subscriberResult = await dripClientFixture.Client.GetSubscriberAsync(newSubscriber);
            DripAssert.Success(subscriberResult);
        }

        [Fact]
        public void CanBulkInsertEvents()
        {
            //one new user
            var events = CreateComplexDripEvents(324, subscriberFactoryFixture.GetRandomEmailAddress());
            var result = dripClientFixture.Client.TrackEvents(events);
            DripAssert.Success(result, HttpStatusCode.Created);

            //all new users
            events = CreateComplexDripEvents(19);
            result = dripClientFixture.Client.TrackEvents(events);
            DripAssert.Success(result, HttpStatusCode.Created);
        }

        [Fact]
        public async Task CanBulkInsertEventsAsync()
        {
            //one new user
            var events = CreateComplexDripEvents(324, subscriberFactoryFixture.GetRandomEmailAddress());
            var result = await dripClientFixture.Client.TrackEventsAsync(events);
            DripAssert.Success(result, HttpStatusCode.Created);

            //all new users
            events = CreateComplexDripEvents(19);
            result = await dripClientFixture.Client.TrackEventsAsync(events);
            DripAssert.Success(result, HttpStatusCode.Created);
        }

        private DripEvent[] CreateComplexDripEvents(int count, string email = null)
        {
            var thePast = DateTime.UtcNow - TimeSpan.FromDays(1);
            var result = new DripEvent[count];
            for (var i = 0; i < result.Length; i++)
                result[i] = new DripEvent
                {
                    Action = Guid.NewGuid().ToString("n"),
                    Email = email ?? subscriberFactoryFixture.GetRandomEmailAddress(),
                    OccurredAt = thePast,
                    Properties = new Dictionary<string, string> { { "myproperty", Guid.NewGuid().ToString("n") } }
                };
            return result;
        }
    }
}
