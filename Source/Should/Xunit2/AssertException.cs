using System;
using System.Collections.Generic;

namespace XunitShould
{
    public static partial class Should
    {
        public static void ShouldEqual(this Exception actual, Exception expected) {
            ShouldEqual(actual, expected, ExceptionComparer.Instance);
        }

        public static void ShouldHaveMessage(this Exception exception, string message) {
            if (exception == null) {
                throw new ArgumentNullException("exception");
            }
            ShouldEqual(exception.Message, message);
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