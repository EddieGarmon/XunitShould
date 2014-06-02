using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;

namespace XunitShould
{
    public static partial class Should
    {
        /// <summary>
        ///     Verifies that two objects are equal, using a default comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="expected">The expected value</param>
        /// <exception cref="EqualException">Thrown when the objects are not equal</exception>
        public static void ShouldEqual<T>(this T actual, T expected) {
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     Verifies that two objects are equal, using a custom comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="expected">The expected value</param>
        /// <param name="comparer">The comparer used to compare the two objects</param>
        /// <exception cref="EqualException">Thrown when the objects are not equal</exception>
        public static void ShouldEqual<T>(this T actual, T expected, IEqualityComparer<T> comparer) {
            Assert.Equal(expected, actual, comparer);
        }

        public static void ShouldEqualWithinPrecision(this double actual, double expected, int precision) {
            Assert.Equal(expected, actual, precision);
        }

        public static void ShouldEqualWithinPrecision(this decimal actual, decimal expected, int precision) {
            Assert.Equal(expected, actual, precision);
        }

        /// <summary>
        ///     Verifies that two objects are not equal, using a default comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="actual">The actual object</param>
        /// <param name="expected">The expected object</param>
        /// <exception cref="NotEqualException">Thrown when the objects are equal</exception>
        public static void ShouldNotEqual<T>(this T actual, T expected) {
            Assert.NotEqual(expected, actual);
        }

        /// <summary>
        ///     Verifies that two objects are not equal, using a custom comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="actual">The actual object</param>
        /// <param name="expected">The expected object</param>
        /// <param name="comparer">The comparer used to examine the objects</param>
        /// <exception cref="NotEqualException">Thrown when the objects are equal</exception>
        public static void ShouldNotEqual<T>(this T actual, T expected, IEqualityComparer<T> comparer) {
            Assert.NotEqual(expected, actual, comparer);
        }
    }
}