using System;
using System.Collections.Generic;
using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class Should
    {
        public static void ShouldBeProperSubset<T>(this ISet<T> actual, ISet<T> expectedSuperset) {
            if (expectedSuperset == null) {
                throw new ArgumentNullException("expectedSuperset");
            }

            if (actual == null || !actual.IsProperSubsetOf(expectedSuperset)) {
                throw new ProperSubsetException(expectedSuperset, actual);
            }
        }

        public static void ShouldBeProperSuperset<T>(this ISet<T> actual, ISet<T> expectedSubset) {
            if (expectedSubset == null) {
                throw new ArgumentNullException("expectedSubset");
            }

            if (actual == null || !actual.IsProperSupersetOf(expectedSubset)) {
                throw new ProperSupersetException(expectedSubset, actual);
            }
        }

        public static void ShouldBeSubset<T>(this ISet<T> actual, ISet<T> expectedSuperset) {
            if (expectedSuperset == null) {
                throw new ArgumentNullException("expectedSuperset");
            }

            if (actual == null || !actual.IsSubsetOf(expectedSuperset)) {
                throw new SubsetException(expectedSuperset, actual);
            }
        }

        public static void ShouldBeSuperset<T>(this ISet<T> actual, ISet<T> expectedSubset) {
            if (expectedSubset == null) {
                throw new ArgumentNullException("expectedSubset");
            }

            if (actual == null || !actual.IsSupersetOf(expectedSubset)) {
                throw new SupersetException(expectedSubset, actual);
            }
        }
    }
}