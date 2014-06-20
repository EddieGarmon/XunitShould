using System;
using System.Threading.Tasks;
using Xunit;

namespace XunitShould
{
    public partial class VerifyTrap
    {
        [Fact]
        public void WithException_Async() {
            Exception exception = Trap.Exception(() => Task.Run(() => { throw new Exception("11235831459"); }));
            exception.ShouldNotBeNull();
            exception.Message.ShouldEqual("11235831459");
        }
    }
}