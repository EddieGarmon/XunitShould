using Xunit;

namespace XunitShould
{
    public class VerifyAssertFalse
    {
        [Fact]
        public void WhenFalse() {
            false.ShouldBeFalse();
        }

        [Fact]
        public void WhenTrue_Throws() {
            Trap.Exception(() => true.ShouldBeFalse())
                .Message.ShouldEndWith("False() Failure");
        }

        [Fact]
        public void WhenTrue_ThrowsWithGeneratedMessage() {
            Trap.Exception(() => true.ShouldBeFalse(() => DynamicText))
                .ShouldHaveMessage(DynamicText);
        }

        [Fact]
        public void WhenTrue_ThrowsWithMessage() {
            Trap.Exception(() => true.ShouldBeFalse("static"))
                .ShouldHaveMessage("static");
        }

        private const string DynamicText = "dynamic, lol";
    }
}