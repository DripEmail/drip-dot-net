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
