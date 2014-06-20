using System.Collections.Generic;
using Xunit;
using XunitShould.Sdk;

namespace XunitShould
{
    public class VerifyAssertRange
    {
        [Fact]
        public void InRange() {
            10.ShouldBeInRange(5, 15);

            Trap.Exception(() => 10.ShouldBeInRange(15, 20))
                .ShouldEqual(new InRangeException(10, 15, true, 20, true));
        }

        [Fact]
        public void InRangeWithComparer() {
            var comparer = Comparer<int>.Default;
            10.ShouldBeInRange(5, 15, comparer);

            Trap.Exception(() => 10.ShouldBeInRange(15, 20, comparer))
                .ShouldEqual(new InRangeException(10, 15, true, 20, true));
        }

        [Fact]
        public void NotInRange() {
            10.ShouldNotBeInRange(15, true, 20, true);
            10.ShouldNotBeInRange(15, false, 20, true);
            10.ShouldNotBeInRange(15, true, 20, false);
            10.ShouldNotBeInRange(15, false, 20, false);
            25.ShouldNotBeInRange(15, 20);

            Trap.Exception(() => 15.ShouldNotBeInRange(10, 20))
                .ShouldEqual(new NotInRangeException(15, 10, true, 20, true));
        }

        [Fact]
        public void NotInRangeWithComparer() {
            var comparer = Comparer<int>.Default;
            10.ShouldNotBeInRange(15, 20, comparer);
            25.ShouldNotBeInRange(15, 20, comparer);

            Trap.Exception(() => 15.ShouldNotBeInRange(10, 20, comparer))
                .ShouldEqual(new NotInRangeException(15, 10, true, 20, true));
        }
    }
}