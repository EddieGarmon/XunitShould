using System;
using System.Collections.Generic;

using Xunit;

using XunitShould.Sdk;

namespace XunitShould.Tests
{
    public class VerifyRanges
    {
        [Fact]
        public void InRange() {
            10.ShouldBeInRange(5, 15);

            Action testCode = () => 10.ShouldBeInRange(15, 20);
            testCode.ShouldThrow(new InRangeException(10, 15, true, 20, true));
        }

        [Fact]
        public void InRangeWithComparer() {
            var comparer = Comparer<int>.Default;
            10.ShouldBeInRange(5, 15, comparer);

            Action testCode = () => 10.ShouldBeInRange(15, 20, comparer);
            testCode.ShouldThrow(new InRangeException(10, 15, true, 20, true));
        }

        [Fact]
        public void NotInRange() {
            10.ShouldNotBeInRange(15, true, 20, true);
            10.ShouldNotBeInRange(15, false, 20, true);
            10.ShouldNotBeInRange(15, true, 20, false);
            10.ShouldNotBeInRange(15, false, 20, false);
            25.ShouldNotBeInRange(15, 20);

            Action testCode = () => 15.ShouldNotBeInRange(10, 20);
            testCode.ShouldThrow(new NotInRangeException(15, 10, true, 20, true));
        }

        [Fact]
        public void NotInRangeWithComparer() {
            var comparer = Comparer<int>.Default;
            10.ShouldNotBeInRange(15, 20, comparer);
            25.ShouldNotBeInRange(15, 20, comparer);

            Action testCode = () => 15.ShouldNotBeInRange(10, 20, comparer);
            testCode.ShouldThrow(new NotInRangeException(15, 10, true, 20, true));
        }
    }
}