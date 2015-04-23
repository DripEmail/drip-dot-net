using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DropDotNetTests
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
    }
}
