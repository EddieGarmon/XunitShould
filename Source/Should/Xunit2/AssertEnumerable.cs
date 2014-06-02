using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class Should
    {
        public static void ShouldBeEmpty(this IEnumerable series) {
            if (series == null) {
                throw new ArgumentNullException("series");
            }

            if (series.GetEnumerator()
                      .MoveNext()) {
                throw new EmptyException();
            }
        }

        public static void ShouldContain<T>(this IEnumerable<T> series, T expected) {
            ShouldContain(series, expected, Comparers.GetEqualityComparer<T>());
        }

        public static void ShouldContain<T>(this IEnumerable<T> series, T expected, IEqualityComparer<T> comparer) {
            if (comparer == null) {
                throw new ArgumentNullException("comparer");
            }

            if (series != null) {
                foreach (T item in series) {
                    if (comparer.Equals(expected, item)) {
                        return;
                    }
                }
            }
            throw new ContainsException(expected);
        }

        public static void ShouldEnumerateEqual(this IEnumerable actual, IEnumerable expected) {
            ShouldEnumerateEqual(actual.Cast<object>(), expected.Cast<object>());
        }

        public static void ShouldEnumerateEqual(this IEnumerable actual, IEnumerable expected, IComparer comparer) {
            if (comparer == null) {
                throw new ArgumentNullException("comparer");
            }

            ShouldEnumerateEqual(actual.Cast<object>(), expected.Cast<object>(), (IComparer<object>)comparer);
        }

        public static void ShouldEnumerateEqual<T>(this IEnumerable<T> actual, params T[] expected) {
            ShouldEnumerateEqual(actual, expected, Comparer<T>.Default);
        }

        public static void ShouldEnumerateEqual<T>(this IEnumerable<T> actual, IEnumerable<T> expected) {
            ShouldEnumerateEqual(actual, expected, Comparer<T>.Default);
        }

        public static void ShouldEnumerateEqual<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IComparer<T> comparer) {
            if (comparer == null) {
                throw new ArgumentNullException("comparer");
            }

            List<T> actualList = actual.ToList();
            List<T> expectedList = expected.ToList();

            int index = 0;
            int shortestLength = Math.Min(actualList.Count, expectedList.Count);
            for (; index < shortestLength; index++) {
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

        public static T ShouldHaveSingle<T>(this IEnumerable<T> actual, Predicate<T> filter = null) {
            if (actual == null) {
                throw new ArgumentNullException("actual");
            }
            filter = filter ?? (item => true);

            int count = 0;
            T result = default(T);

            foreach (T item in actual) {
                if (filter(item)) {
                    result = item;
                    ++count;
                }
            }

            if (count != 1) {
                throw new SingleException(count);
            }

            return result;
        }

        public static void ShouldNotBeEmpty(this IEnumerable series) {
            if (series == null) {
                throw new ArgumentNullException("series");
            }
            if (!series.GetEnumerator()
                       .MoveNext()) {
                throw new NotEmptyException();
            }
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> series, T expected) {
            ShouldNotContain(series, expected, Comparers.GetEqualityComparer<T>());
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> series, T expected, IEqualityComparer<T> comparer) {
            if (comparer == null) {
                throw new ArgumentNullException("comparer");
            }

            if (series != null) {
                foreach (T item in series) {
                    if (comparer.Equals(expected, item)) {
                        throw new DoesNotContainException(expected);
                    }
                }
            }
        }
    }
}