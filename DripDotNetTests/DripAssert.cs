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
using Drip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DripDotNetTests
{
    internal static class DripAssert
    {
        internal static void Equal<TKey, TValue>(IDictionary<TKey, TValue> expected, IDictionary<TKey, TValue> actual)
        {
            if (expected == null || actual == null)
                Assert.Equal(expected, actual);

            Assert.Equal(expected.Count, actual.Count);

            foreach (var kv in expected)
                Assert.Equal(expected[kv.Key], actual[kv.Key]);
        }

        internal static void ContainsSameItems(IEnumerable expected, IEnumerable actual)
        {
            if (expected == null || actual == null)
                Assert.Equal(expected, actual);

            var actualList = new ArrayList();
            foreach (var item in actual)
                actualList.Add(item);

            int expectedCount = 0;
            foreach (var item in expected)
            {
                expectedCount++;
                Assert.True(actualList.Contains(item));
            }

            Assert.Equal(expectedCount, actualList.Count);
        }

        internal static void Success(DripResponse resp, HttpStatusCode expectedStatus=HttpStatusCode.OK)
        {
            Assert.Equal(expectedStatus, resp.StatusCode);
            Assert.True(resp.HasSuccessStatusCode());
            Assert.False(resp.HasErrors());
        }
    }
}
