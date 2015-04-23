using Drip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DropDotNetTests
{
    public class SubscriberTests : IClassFixture<DripClientFixture>, IClassFixture<SubscriberFactoryFixture>
    {
        DripClientFixture dripClientFixture;
        SubscriberFactoryFixture subscriberFactoryFixture;

        public SubscriberTests(DripClientFixture dripClientFixture, SubscriberFactoryFixture subscriberFactoryFixture)
        {
            this.dripClientFixture = dripClientFixture;
            this.subscriberFactoryFixture = subscriberFactoryFixture;
        }

        [Fact]
        public void CanCreateUpdateAndRetrieveSubscriber()
        {
            var expected = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();

            var result = dripClientFixture.Client.CreateOrUpdateSubscriber(expected);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());

            result = dripClientFixture.Client.GetSubscriber(expected.Email);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());
            Assert.Equal(1, result.Subscribers.Count);

            var actual = result.Subscribers.First();
            DripAssert.Equal(expected.CustomFields, actual.CustomFields);
            DripAssert.ContainsSameItems(expected.Tags, actual.Tags);

            var oldEmail = expected.Email;
            expected.NewEmail = subscriberFactoryFixture.GetRandomEmailAddress();
            result = dripClientFixture.Client.CreateOrUpdateSubscriber(expected);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());

            result = dripClientFixture.Client.GetSubscriber(expected.NewEmail);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());
            Assert.Equal(1, result.Subscribers.Count);

            actual = result.Subscribers.First();
            Assert.Equal(expected.NewEmail, actual.Email);
            DripAssert.Equal(expected.CustomFields, actual.CustomFields);
            DripAssert.ContainsSameItems(expected.Tags, actual.Tags);

        }

        [Fact]
        public async Task CanCreateUpdateAndRetrieveSubscriberAsync()
        {
            var expected = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();

            var result = await dripClientFixture.Client.CreateOrUpdateSubscriberAsync(expected);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());

            result = await dripClientFixture.Client.GetSubscriberAsync(expected.Email);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());
            Assert.Equal(1, result.Subscribers.Count);

            var actual = result.Subscribers.First();
            Assert.Equal(expected.Email, actual.Email);
            DripAssert.Equal(expected.CustomFields, actual.CustomFields);
            DripAssert.ContainsSameItems(expected.Tags, actual.Tags);

            var oldEmail = expected.Email;
            expected.NewEmail = subscriberFactoryFixture.GetRandomEmailAddress();
            result = await dripClientFixture.Client.CreateOrUpdateSubscriberAsync(expected);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());

            result = await dripClientFixture.Client.GetSubscriberAsync(expected.NewEmail);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());
            Assert.Equal(1, result.Subscribers.Count);

            actual = result.Subscribers.First();
            Assert.Equal(expected.NewEmail, actual.Email);
            DripAssert.Equal(expected.CustomFields, actual.CustomFields);
            DripAssert.ContainsSameItems(expected.Tags, actual.Tags);
        }

        [Fact]
        public void NonExistantSubcriberReturnsError()
        {
            var randomSubscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();

            var result = dripClientFixture.Client.GetSubscriber(randomSubscriber.Email);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.False(result.HasSuccessStatusCode());
            Assert.True(result.HasErrors());
        }

        [Fact]
        public async Task NonExistantSubcriberReturnsErrorAsync()
        {
            var randomSubscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();

            var result = await dripClientFixture.Client.GetSubscriberAsync(randomSubscriber.Email);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.False(result.HasSuccessStatusCode());
            Assert.True(result.HasErrors());
        }

        [Fact]
        public void CanBulkInsertSubscribers()
        {
            var actual = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscribers(23);
            var result = dripClientFixture.Client.CreateOrUpdateSubscribers(actual);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());
        }

        [Fact]
        public async Task CanBulkInsertSubscribersAsync()
        {
            var actual = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscribers(29);
            var result = await dripClientFixture.Client.CreateOrUpdateSubscribersAsync(actual);
            //TODO: Fix server and/or RestSharp
            //This operation succeeds, but it looks like the server returns a content-type of json with a non-empty, but non-JSON result.
            //This causes RestSharp to try to parse the body, which isn't empty, as JSON, which throws an error.
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());
        }
    }
}
