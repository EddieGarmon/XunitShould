using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class Should
    {
        public static void ShouldBeEmpty(this IEnumerable series) {
            Assert.Empty(series);
        }

        public static void ShouldContain<T>(this IEnumerable<T> series, T expected) {
            Assert.Contains(expected, series);
        }

        public static void ShouldContain<T>(this IEnumerable<T> series, T expected, IEqualityComparer<T> comparer) {
            Assert.Contains(expected, series, comparer);
        }

        public static void ShouldEnumerateEqual<T>(this IEnumerable<T> actual, params T[] expected) {
            ShouldEnumerateEqual(actual, expected, Comparer<T>.Default);
        }

        public static void ShouldEnumerateEqual<T>(this IEnumerable<T> actual, IEnumerable<T> expected) {
            ShouldEnumerateEqual(actual, expected, Comparer<T>.Default);
        }

        public static void ShouldEnumerateEqual<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IComparer<T> comparer) {
            List<T> actualList = actual.ToList();
            List<T> expectedList = expected.ToList();

            int index = 0;
            int lastBoth = Math.Min(actualList.Count, expectedList.Count);
            for (; index < lastBoth; index++) {
                T actualItem = actualList[index];
                T expectedItem = expectedList[index];
                if (comparer.Compare(actualItem, expectedItem) != 0) {
                    throw new EnumerableEqualException(expectedItem, actualItem, index, expectedList.Count, actualList.Count);
                }
            }
            if (index != expectedList.Count) {
                throw new EnumerableEqualException(expectedList[index], null, index, expectedList.Count, actualList.Count);
            }
            if (index != actualList.Count) {
                throw new EnumerableEqualException(null, actualList[index], index, expectedList.Count, actualList.Count);
            }
        }

        public static T ShouldHaveSingle<T>(this IEnumerable<T> actual, Predicate<T> filter) {
            return Assert.Single(actual, filter);
        }

        public static void ShouldNotBeEmpty(this IEnumerable series) {
            Assert.NotEmpty(series);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> series, T expected) {
            Assert.DoesNotContain(expected, series);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> series, T expected, IEqualityComparer<T> comparer) {
            Assert.DoesNotContain(expected, series, comparer);
        }
    }
}