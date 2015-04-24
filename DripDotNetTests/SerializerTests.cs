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
using Drip.Protocol;
using System;
using System.Collections.Generic;
using Xunit;

namespace DripDotNetTests
{
    public class SerializerTests
    {
        [Fact]
        public void LcaseUnderscoreMappingResolverBasicallyWorks()
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
        public void RestSharpLcaseUnderscoreSerializerBasicallyWorks()
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

