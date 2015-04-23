using System;
using System.Collections.Generic;
using System.Linq;
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
            

            //There is no API for reading events so we can't verify the contents from here. Look in your Dashboard.
        }
    }
}
