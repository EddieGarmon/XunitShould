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
            empty.ShouldEnumerateEqual(new string[0]);
            empty.ShouldEnumerateEqual(new List<string>());
        }

        [Fact]
        public void Int32List() {
            var items = new List<int> { 1, 2, 3, 4, 5 };
            items.ShouldEnumerateEqual(new[] { 1, 2, 3, 4, 5 });
            items.ShouldEnumerateEqual(1, 2, 3, 4, 5);
        }

        [Fact]
        public void MiddleNotEqual() {
            var items = new List<string> { "1", "2", "3" };
            Trap.Exception(() => items.ShouldEnumerateEqual("1", "3", "5"))
                .ShouldEqual(new EnumerableEqualException("3", "2", 1, 3, 3));
        }

        [Fact]
        public void MoreInActual() {
            var items = new List<string> { "1", "2", "3", "4", "5" };
            Trap.Exception(() => items.ShouldEnumerateEqual("1", "2", "3", "4"))
                .ShouldEqual(new EnumerableEqualException(null, "5", 4, 4, 5));
        }

        [Fact]
        public void MoreInExpected() {
            var items = new List<string> { "1", "2", "3", "4" };
            Trap.Exception(() => items.ShouldEnumerateEqual("1", "2", "3", "4", "5"))
                .ShouldEqual(new EnumerableEqualException("5", null, 4, 5, 4));
        }

        [Fact]
        public void StringList() {
            var items = new List<string> { "1", "2", "3", "4", "5" };
            items.ShouldEnumerateEqual(new[] { "1", "2", "3", "4", "5" });
            items.ShouldEnumerateEqual("1", "2", "3", "4", "5");
        }
    }
}