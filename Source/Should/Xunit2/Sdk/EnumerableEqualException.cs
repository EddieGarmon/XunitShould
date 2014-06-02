using System;
using System.Globalization;

namespace XunitShould.Sdk
{
    public class EnumerableEqualException : XunitException
    {
        private readonly string _actual;
        private readonly int _actualCount;
        private readonly int _atIndex;
        private readonly string _expected;
        private readonly int _expectedCount;

        public EnumerableEqualException(object expected, object actual, int atIndex, int expectedCount, int actualCount) {
            _expected = expected == null ? "(null)" : expected.ToString();
            _actual = actual == null ? "(null)" : actual.ToString();
            _atIndex = atIndex;
            _expectedCount = expectedCount;
            _actualCount = actualCount;
        }

        public string Actual {
            get { return _actual; }
        }

        public int ActualCount {
            get { return _actualCount; }
        }

        public int AtIndex {
            get { return _atIndex; }
        }

        public string Expected {
            get { return _expected; }
        }

        public int ExpectedCount {
            get { return _expectedCount; }
        }

        public override string Message {
            get {
                return string.Format(CultureInfo.CurrentCulture,
                                     "Enumerables not equal at index: {0}{5}(Expected has {1} items, Actual has {2} items){5}Expected:  {3}{5}Actual: {4}",
                                     _atIndex,
                                     _expectedCount,
                                     _actualCount,
                                     _expected,
                                     _actual,
                                     Environment.NewLine);
            }
        }
    }
}