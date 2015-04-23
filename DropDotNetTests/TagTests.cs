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
    public class TagTests : IClassFixture<DripClientFixture>, IClassFixture<SubscriberFactoryFixture>
    {
        DripClientFixture dripClientFixture;
        SubscriberFactoryFixture subscriberFactoryFixture;

        public TagTests(DripClientFixture dripClientFixture, SubscriberFactoryFixture subscriberFactoryFixture)
        {
            this.dripClientFixture = dripClientFixture;
            this.subscriberFactoryFixture = subscriberFactoryFixture;
        }

        [Fact]
        public void CanApplyAndRemoveTags()
        {
            var originalSubscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();
            dripClientFixture.Client.CreateOrUpdateSubscriber(originalSubscriber);

            var newTag = Guid.NewGuid().ToString("n");

            var result = dripClientFixture.Client.ApplyTagToSubscriber(originalSubscriber.Email, newTag);
            DripAssert.Success(result, HttpStatusCode.Created);

            var oldTag = originalSubscriber.Tags[0];
            result = dripClientFixture.Client.RemoveTagFromSubscriber(originalSubscriber.Email, oldTag);
            DripAssert.Success(result, HttpStatusCode.NoContent);

            var subscriberResult = dripClientFixture.Client.GetSubscriber(originalSubscriber.Email);
            DripAssert.Success(subscriberResult);

            var newSubscriber = subscriberResult.Subscribers.First();
            Assert.True(newSubscriber.Tags.Contains(newTag));
            Assert.False(newSubscriber.Tags.Contains(oldTag));
        }

        [Fact]
        public void ApplyingTagCreatesSubscriber()
        {
            var email = subscriberFactoryFixture.GetRandomEmailAddress();
            var tag = Guid.NewGuid().ToString();
            var result = dripClientFixture.Client.ApplyTagToSubscriber(email, tag);
            DripAssert.Success(result, HttpStatusCode.Created);

            var subscriberResult = dripClientFixture.Client.GetSubscriber(email);
            DripAssert.Success(subscriberResult);

            var newSubscriber = subscriberResult.Subscribers.First();
            Assert.True(newSubscriber.Tags.Contains(tag));
        }

        [Fact]
        public void CanRemoveNonExistantTag()
        {
            var originalSubscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();
            dripClientFixture.Client.CreateOrUpdateSubscriber(originalSubscriber);

            var tag = Guid.NewGuid().ToString();
            var result = dripClientFixture.Client.RemoveTagFromSubscriber(originalSubscriber.Email, tag);
            DripAssert.Success(result, HttpStatusCode.NoContent);
        }




        [Fact]
        public async Task CanApplyAndRemoveTagsAsync()
        {
            var originalSubscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();
            await dripClientFixture.Client.CreateOrUpdateSubscriberAsync(originalSubscriber);

            var newTag = Guid.NewGuid().ToString("n");

            var result = await dripClientFixture.Client.ApplyTagToSubscriberAsync(originalSubscriber.Email, newTag);
            DripAssert.Success(result, HttpStatusCode.Created);

            var oldTag = originalSubscriber.Tags[0];
            result = await dripClientFixture.Client.RemoveTagFromSubscriberAsync(originalSubscriber.Email, oldTag);
            DripAssert.Success(result, HttpStatusCode.NoContent);

            var subscriberResult = await dripClientFixture.Client.GetSubscriberAsync(originalSubscriber.Email);
            DripAssert.Success(subscriberResult);

            var newSubscriber = subscriberResult.Subscribers.First();
            Assert.True(newSubscriber.Tags.Contains(newTag));
            Assert.False(newSubscriber.Tags.Contains(oldTag));
        }

        [Fact]
        public async Task ApplyingTagCreatesSubscriberAsync()
        {
            var email = subscriberFactoryFixture.GetRandomEmailAddress();
            var tag = Guid.NewGuid().ToString();
            var result = await dripClientFixture.Client.ApplyTagToSubscriberAsync(email, tag);
            DripAssert.Success(result, HttpStatusCode.Created);

            var subscriberResult = await dripClientFixture.Client.GetSubscriberAsync(email);
            DripAssert.Success(subscriberResult);

            var newSubscriber = subscriberResult.Subscribers.First();
            Assert.True(newSubscriber.Tags.Contains(tag));
        }

        [Fact]
        public async Task CanRemoveNonExistantTagAsync()
        {
            var originalSubscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();
            await dripClientFixture.Client.CreateOrUpdateSubscriberAsync(originalSubscriber);

            var tag = Guid.NewGuid().ToString();
            var result = await dripClientFixture.Client.RemoveTagFromSubscriberAsync(originalSubscriber.Email, tag);
            DripAssert.Success(result, HttpStatusCode.NoContent);
        }
    }
}
