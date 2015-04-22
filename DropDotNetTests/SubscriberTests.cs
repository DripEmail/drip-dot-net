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

        [Fact]
        public void CanCreateUpdateAndRetrieveSubscriber()
        {
            var expected = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();

            var result = dripClientFixture.Client.CreateOrUpdateSubscriber(expected);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());

            result = dripClientFixture.Client.GetSubscriber(expected.Email);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());
            Assert.Equal(1, result.Subscribers.Count);

            var actual = result.Subscribers.First();
            Assert.Equal(expected.CustomFields, actual.CustomFields);
            Assert.Equal(expected.Tags, actual.Tags);
            Assert.Equal(expected.PotentialLead, actual.PotentialLead);

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
            Assert.Equal(expected.CustomFields, actual.CustomFields);
            Assert.Equal(expected.Tags, actual.Tags);
            Assert.Equal(expected.PotentialLead, actual.PotentialLead);

        }

        [Fact]
        public async Task CanCreateUpdateAndRetrieveSubscriberAsync()
        {
            var expected = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();

            var result = await dripClientFixture.Client.CreateOrUpdateSubscriberAsync(expected);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());

            result = await dripClientFixture.Client.GetSubscriberAsync(expected.Email);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());
            Assert.Equal(1, result.Subscribers.Count);

            var actual = result.Subscribers.First();
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.CustomFields, actual.CustomFields);
            Assert.Equal(expected.Tags, actual.Tags);
            Assert.Equal(expected.PotentialLead, actual.PotentialLead);

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
            Assert.Equal(expected.CustomFields, actual.CustomFields);
            Assert.Equal(expected.Tags, actual.Tags);
            Assert.Equal(expected.PotentialLead, actual.PotentialLead);
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
    }
}
