using System;
using Xunit;

namespace XunitShould
{
    public partial class VerifyRecord
    {
        [Fact]
        public void WithException() {
            Exception exception = Record.Exception(() => { throw new Exception("11235831459"); });
            exception.ShouldNotBeNull();
            exception.Message.ShouldEqual("11235831459");
        }

        [Fact]
        public void WithoutException() {
            Exception exception = Record.Exception(() => { });
            exception.ShouldBeNull();
        }
    }
}