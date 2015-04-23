using Drip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DripDotNetTests
{
    public class SubscriberFactoryFixture: IDisposable
    {
        public SubscriberFactoryFixture() { }

        public string GetRandomEmailAddress()
        {
            return Guid.NewGuid().ToString("n") + "@getdriptest.com";
        }

        public ModifyDripSubscriberRequest CreateComplexUniqueModifyDripSubscriber()
        {
            var result = new ModifyDripSubscriberRequest
            {
                Email = GetRandomEmailAddress(),
                CustomFields = new Dictionary<string, string>
                { 
                    {"id", Guid.NewGuid().ToString("n")},
                    {"Name", "Foo Example"},
                    {"SomeDate", DateTime.UtcNow.ToString("o")},
                    {"AnIntSorta_kinda", "123"},
                },
                PotentialLead = true,
                Tags = new List<string> { Guid.NewGuid().ToString("n"), "test" }
            };

            return result;
        }

        public ModifyDripSubscriberRequest[] CreateComplexUniqueModifyDripSubscribers(int count)
        {
            var result = new ModifyDripSubscriberRequest[count];
            for (var i = 0; i < result.Length; i++)
                result[i] = CreateComplexUniqueModifyDripSubscriber();
            return result;
        }

        public void Dispose()
        {
        }
    }
}
