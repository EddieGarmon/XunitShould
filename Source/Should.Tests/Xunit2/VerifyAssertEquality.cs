using System;
using System.Collections.Generic;

using Xunit;

using XunitShould.Sdk;

namespace XunitShould
{
    public class VerifyAssertEquality
    {
        [Fact]
        public void EmptyEqual() {
            var empty = new List<string>();
            empty.ShouldEqual(new string[0]);
            empty.ShouldEqual(new List<string>());
        }

        [Fact]
        public void Int32List() {
            var items = new List<int> { 1, 2, 3, 4, 5 };
            items.ShouldEqual(new[] { 1, 2, 3, 4, 5 });
            items.ShouldEqual(1, 2, 3, 4, 5);
        }

        [Fact]
        public void MiddleNotEqual() {
            var items = new List<string> { "1", "2", "3" };
            Action testCode = () => items.ShouldEqual("1", "3", "5");
            testCode.ShouldThrow(new EnumerableEqualException("3", "2", 1, 3, 3));
        }

        [Fact]
        public void MoreInActual() {
            var items = new List<string> { "1", "2", "3", "4", "5" };
            Action testCode = () => items.ShouldEqual("1", "2", "3", "4");
            testCode.ShouldThrow(new EnumerableEqualException(null, "5", 4, 4, 5));
        }

        [Fact]
        public void MoreInExpected() {
            var items = new List<string> { "1", "2", "3", "4" };
            Action testCode = () => items.ShouldEqual("1", "2", "3", "4", "5");
            testCode.ShouldThrow(new EnumerableEqualException("5", null, 4, 5, 4));
        }

        [Fact]
        public void StringList() {
            var items = new List<string> { "1", "2", "3", "4", "5" };
            items.ShouldEqual(new[] { "1", "2", "3", "4", "5" });
            items.ShouldEqual("1", "2", "3", "4", "5");
        }
    }
}