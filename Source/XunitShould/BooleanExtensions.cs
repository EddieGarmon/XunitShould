using System;

using Xunit.Sdk;

namespace XunitShould
{
    /// <summary>
    ///     Extensions which provide assertions to classes derived from <see cref="bool" />.
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        ///     Verifies that the condition is false.
        /// </summary>
        /// <param name="condition">The condition to be tested</param>
        /// <exception cref="FalseException">Thrown if the condition is not false</exception>
        public static void ShouldBeFalse(this bool condition) {
            if (condition) {
                throw new FalseException(null);
            }
        }

        /// <summary>
        ///     Verifies that the condition is false.
        /// </summary>
        /// <param name="condition">The condition to be tested</param>
        /// <param name="userMessage">The message to show when the condition is not false</param>
        /// <exception cref="FalseException">Thrown if the condition is not false</exception>
        public static void ShouldBeFalse(this bool condition, string userMessage) {
            if (condition) {
                throw new FalseException(userMessage);
            }
        }

        /// <summary>
        ///     Verifies that the condition is false.
        /// </summary>
        /// <param name="condition">The condition to be tested</param>
        /// <param name="messageGenerator">The message to show when the condition is not false</param>
        /// <exception cref="FalseException">Thrown if the condition is not false</exception>
        public static void ShouldBeFalse(this bool condition, Func<string> messageGenerator) {
            if (condition) {
                throw new FalseException(messageGenerator());
            }
        }

        /// <summary>
        ///     Verifies that an expression is true.
        /// </summary>
        /// <param name="condition">The condition to be inspected</param>
        /// <exception cref="TrueException">Thrown when the condition is false</exception>
        public static void ShouldBeTrue(this bool condition) {
            if (!condition) {
                throw new TrueException(null);
            }
        }

        /// <summary>
        ///     Verifies that an expression is true.
        /// </summary>
        /// <param name="condition">The condition to be inspected</param>
        /// <param name="userMessage">The message to be shown when the condition is false</param>
        /// <exception cref="TrueException">Thrown when the condition is false</exception>
        public static void ShouldBeTrue(this bool condition, string userMessage) {
            if (!condition) {
                throw new TrueException(userMessage);
            }
        }

        /// <summary>
        ///     Verifies that an expression is true.
        /// </summary>
        /// <param name="condition">The condition to be inspected</param>
        /// <param name="messageGenerator">The message to be shown when the condition is false</param>
        /// <exception cref="TrueException">Thrown when the condition is false</exception>
        public static void ShouldBeTrue(this bool condition, Func<string> messageGenerator) {
            if (!condition) {
                throw new TrueException(messageGenerator());
            }
        }
    }
}