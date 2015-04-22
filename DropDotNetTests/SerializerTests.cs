using Drip;
using System;
using System.Collections.Generic;
using Xunit;

namespace DropDotNetTests
{
    public class SerializerTests
    {
        [Fact]
        public void LcaseUnderscoreMappingResolverSmoke()
        {
            var resolver = new LcaseUnderscoreMappingResolver();
            var expected = "test_test_test";
            var input = "TestTestTest";

            var actual = resolver.GetResolvedPropertyName(input);
            Assert.Equal(expected, actual);

            actual = resolver.GetResolvedPropertyName(expected);
            Assert.Equal(expected, actual);

            input = "ABC123";
            expected = "abc123";
            actual = resolver.GetResolvedPropertyName(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RestSharpLcaseUnderscoreSerializerSmoke()
        {
            var serializer = new RestSharpLcaseUnderscoreSerializer();

            var sourceObj = new 
            {
                Test = "abc",
                TestingPartDeux = 2,
                NestedList = new List<int>(),
                ItsNow = new DateTime?(DateTime.UtcNow),
                ThisIsNull = (string)null
            };

            var actualString = serializer.Serialize(sourceObj);
            var expectedString = string.Format("{{\"test\":\"abc\",\"testing_part_deux\":2,\"nested_list\":[],\"its_now\":\"{0:o}\"}}", sourceObj.ItsNow);

            Assert.Equal(expectedString, actualString);
        }
    }
}

