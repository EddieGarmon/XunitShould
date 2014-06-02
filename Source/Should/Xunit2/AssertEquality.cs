using System;
using System.Collections.Generic;
using System.Globalization;
using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class Should
    {
        public static void ShouldEqual<T>(this T actual, T expected) {
            ShouldEqual(actual, expected, Comparers.GetEqualityComparer<T>());
        }

        public static void ShouldEqual<T>(this T actual, T expected, IEqualityComparer<T> comparer) {
            if (comparer == null) {
                throw new ArgumentNullException("comparer");
            }

            if (!comparer.Equals(actual, expected)) {
                throw new EqualException(expected, actual);
            }
        }

        public static void ShouldEqualWithinPrecision(this double actual, double expected, int precision) {
            var expectedRounded = Math.Round(expected, precision);
            var actualRounded = Math.Round(actual, precision);

            if (!Comparers.GetEqualityComparer<double>()
                          .Equals(expectedRounded, actualRounded)) {
                throw new EqualException(String.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", expectedRounded, expected),
                                         String.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", actualRounded, actual));
            }
        }

        public static void ShouldEqualWithinPrecision(this decimal actual, decimal expected, int precision) {
            var expectedRounded = Math.Round(expected, precision);
            var actualRounded = Math.Round(actual, precision);

            if (!Comparers.GetEqualityComparer<decimal>()
                          .Equals(expectedRounded, actualRounded)) {
                throw new EqualException(String.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", expectedRounded, expected),
                                         String.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", actualRounded, actual));
            }
        }

        public static void ShouldNotEqual<T>(this T actual, T expected) {
            ShouldNotEqual(actual, expected, Comparers.GetEqualityComparer<T>());
        }

        public static void ShouldNotEqual<T>(this T actual, T expected, IEqualityComparer<T> comparer) {
            if (comparer == null) {
                throw new ArgumentNullException("comparer");
            }
            if (comparer.Equals(expected, actual)) {
                throw new NotEqualException();
            }
        }
    }
}