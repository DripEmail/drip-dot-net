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

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DripDotNetTests
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
            DripAssert.Success(result, HttpStatusCode.OK);

            result = dripClientFixture.Client.GetSubscriber(expected.Email);
            DripAssert.Success(result, HttpStatusCode.OK);
            Assert.Single(result.Subscribers);

            var actual = result.Subscribers.First();
            DripAssert.Equal(expected.CustomFields, actual.CustomFields);
            DripAssert.ContainsSameItems(expected.Tags, actual.Tags);

            var oldEmail = expected.Email;
            expected.NewEmail = subscriberFactoryFixture.GetRandomEmailAddress();
            result = dripClientFixture.Client.CreateOrUpdateSubscriber(expected);
            Assert.True(result.HasSuccessStatusCode());
            Assert.False(result.HasErrors());

            result = dripClientFixture.Client.GetSubscriber(expected.NewEmail);
            DripAssert.Success(result, HttpStatusCode.OK);
            Assert.Single(result.Subscribers);

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
            DripAssert.Success(result, HttpStatusCode.OK);

            result = await dripClientFixture.Client.GetSubscriberAsync(expected.Email);
            DripAssert.Success(result, HttpStatusCode.OK);
            Assert.Single(result.Subscribers);

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
            DripAssert.Success(result, HttpStatusCode.OK);
            Assert.Single(result.Subscribers);

            actual = result.Subscribers.First();
            Assert.Equal(expected.NewEmail, actual.Email);
            DripAssert.Equal(expected.CustomFields, actual.CustomFields);
            DripAssert.ContainsSameItems(expected.Tags, actual.Tags);
        }

        [Fact]
        public void NonExistentSubcriberReturnsError()
        {
            var randomSubscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();

            var result = dripClientFixture.Client.GetSubscriber(randomSubscriber.Email);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.False(result.HasSuccessStatusCode());
            Assert.True(result.HasErrors());
        }

        [Fact]
        public async Task NonExistentSubscriberReturnsErrorAsync()
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
            DripAssert.Success(result, HttpStatusCode.Created);
        }

        [Fact]
        public async Task CanBulkInsertSubscribersAsync()
        {
            var actual = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscribers(29);
            var result = await dripClientFixture.Client.CreateOrUpdateSubscribersAsync(actual);
            //TODO: Fix server and/or RestSharp
            //This operation succeeds, but it looks like the server returns a content-type of json with a non-empty, but non-JSON result.
            //This causes RestSharp to try to parse the body, which isn't empty, as JSON, which throws an error.
            DripAssert.Success(result, HttpStatusCode.Created);
        }
    }
}
