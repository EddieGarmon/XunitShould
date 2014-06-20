using System;
using Xunit;

namespace XunitShould
{
    public partial class VerifyTrap
    {
        [Fact]
        public void WithException() {
            Exception exception = Trap.Exception(() => { throw new Exception("11235831459"); });
            exception.ShouldNotBeNull();
            exception.Message.ShouldEqual("11235831459");
        }

        [Fact]
        public void WithoutException() {
            Exception exception = Trap.Exception(() => { });
            exception.ShouldBeNull();
        }
    }
}