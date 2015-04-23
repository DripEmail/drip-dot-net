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
    public class CampaignTests: IClassFixture<DripClientFixture>, IClassFixture<SubscriberFactoryFixture>
    {
        //TODO: put this in a configuration file
        const string TestCampaignId = "3057996"; //This is a test campaign in my test account

        DripClientFixture dripClientFixture;
        SubscriberFactoryFixture subscriberFactoryFixture;

        public CampaignTests(DripClientFixture dripClientFixture, SubscriberFactoryFixture subscriberFactoryFixture)
        {
            this.dripClientFixture = dripClientFixture;
            this.subscriberFactoryFixture = subscriberFactoryFixture;
        }

        [Fact]
        public void CanSubscribeExistingSubscriberToCampaignAndThenUnsubscribe()
        {
            var subscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();
            var subscriberResult = dripClientFixture.Client.CreateOrUpdateSubscriber(subscriber);
            DripAssert.Success(subscriberResult);

            var campaignRequest = new ModifyDripCampaignSubscriberRequest { Email = subscriber.Email };
            var result = dripClientFixture.Client.SubscribeToCampaign(TestCampaignId, campaignRequest);
            DripAssert.Success(result, HttpStatusCode.Created);

            subscriberResult = dripClientFixture.Client.UnsubscribeFromCampaign(subscriber.Email, TestCampaignId);
            //TODO: this is a mismatch with the docs. Docs say it will be "200 OK"
            DripAssert.Success(result, HttpStatusCode.Created);
        }

        [Fact]
        public void CanSubscribeNewSubscriberToCampaignAndThenUnsubscribeFromAll()
        {
            var newSubscriber = subscriberFactoryFixture.GetRandomEmailAddress();
            var campaignRequest = new ModifyDripCampaignSubscriberRequest { Email = newSubscriber };
            var result = dripClientFixture.Client.SubscribeToCampaign(TestCampaignId, campaignRequest);
            DripAssert.Success(result, HttpStatusCode.Created);

            var subscriberResult = dripClientFixture.Client.GetSubscriber(newSubscriber);
            DripAssert.Success(subscriberResult);

            subscriberResult = dripClientFixture.Client.UnsubscribeFromCampaign(newSubscriber);
            //TODO: this is a mismatch with the docs. Docs say it will be "200 OK"
            DripAssert.Success(result, HttpStatusCode.Created);
        }

        [Fact]
        public async Task CanSubscribeExistingSubscriberToCampaignAndThenUnsubscribeAsync()
        {
            var subscriber = subscriberFactoryFixture.CreateComplexUniqueModifyDripSubscriber();
            var subscriberResult = await dripClientFixture.Client.CreateOrUpdateSubscriberAsync(subscriber);
            DripAssert.Success(subscriberResult);

            var campaignRequest = new ModifyDripCampaignSubscriberRequest { Email = subscriber.Email };
            var result = await dripClientFixture.Client.SubscribeToCampaignAsync(TestCampaignId, campaignRequest);
            DripAssert.Success(result, HttpStatusCode.Created);

            subscriberResult = await dripClientFixture.Client.UnsubscribeFromCampaignAsync(subscriber.Email, TestCampaignId);
            //TODO: this is a mismatch with the docs. Docs say it will be "200 OK"
            DripAssert.Success(result, HttpStatusCode.Created);
        }

        [Fact]
        public async Task CanSubscribeNewSubscriberToCampaignAndThenUnsubscribeFromAllAsync()
        {
            var newSubscriber = subscriberFactoryFixture.GetRandomEmailAddress();
            var campaignRequest = new ModifyDripCampaignSubscriberRequest { Email = newSubscriber };
            var result = await dripClientFixture.Client.SubscribeToCampaignAsync(TestCampaignId, campaignRequest);
            DripAssert.Success(result, HttpStatusCode.Created);

            var subscriberResult = await dripClientFixture.Client.GetSubscriberAsync(newSubscriber);
            DripAssert.Success(subscriberResult);

            subscriberResult = await dripClientFixture.Client.UnsubscribeFromCampaignAsync(newSubscriber);
            //TODO: this is a mismatch with the docs. Docs say it will be "200 OK"
            DripAssert.Success(result, HttpStatusCode.Created);
        }
    }
}
