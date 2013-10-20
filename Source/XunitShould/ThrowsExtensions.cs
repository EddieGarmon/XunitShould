using System;
using System.Collections.Generic;

using Xunit;

namespace XunitShould
{
    public static class ThrowsExtensions
    {
        public static void ShouldThrow<T>(this Action testCode, string message = null) where T : Exception {
            var thrown = Assert.Throws<T>(() => testCode());
            if (message != null) {
                Assert.Equal(message, thrown.Message);
            }
        }

        public static void ShouldThrow<T>(this Action testCode, T exception) where T : Exception {
            var thrown = Assert.Throws<T>(() => testCode());
            Assert.Equal(exception, thrown, ExceptionComparer.Instance);
        }

        public static void ShouldThrow<T>(this Func<object> testCode, string message = null) where T : Exception {
            var thrown = Assert.Throws<T>(() => testCode());
            if (message != null) {
                Assert.Equal(message, thrown.Message);
            }
        }

        public static void ShouldThrow<T>(this Func<object> testCode, T exception) where T : Exception {
            var thrown = Assert.Throws<T>(() => testCode());
            Assert.Equal(exception, thrown, ExceptionComparer.Instance);
        }

        private class ExceptionComparer : IEqualityComparer<Exception>
        {
            public bool Equals(Exception x, Exception y) {
                return (x.GetType() == y.GetType()) && (x.Message == y.Message);
            }

            public int GetHashCode(Exception obj) {
                return obj.GetHashCode();
            }

            public static readonly ExceptionComparer Instance = new ExceptionComparer();
        }
    }
}