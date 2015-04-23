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
