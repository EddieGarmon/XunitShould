using System.Xml.Linq;
using Xunit;

namespace XunitShould
{
    public class VerifyAssertElements
    {
        [Fact]
        public void AttributesNotEqual() {
            var actualAttribute = new XAttribute("Test", "Pass");
            var actualElement = new XElement("Simple", actualAttribute);
            var expectedAttribute = new XAttribute("Test2", "Fail");
            var expectedElement = new XElement("Simple", expectedAttribute);
            var thrown = Trap.Exception(() => actualElement.ShouldEqual(expectedElement));
            //var inner = thrown.InnerExceptions.ToList();
            //inner.Count.ShouldEqual(2);
            //inner[0].Message.ShouldEqual(new EqualException(expectedElement, actualElement).Message);
            //inner[1].Message.ShouldEqual(new EnumerableEqualException(expectedAttribute, actualAttribute, 0, 1, 1).Message);
        }

        [Fact]
        public void NestedElementEqual() {
            var value = new XElement("Simple", new XElement("Test", "Pass"));
            value.ShouldEqual("<Simple><Test>Pass</Test></Simple>");
        }

        [Fact]
        public void SimpleElementEqual() {
            var value = new XElement("Simple", new XAttribute("Test", "Pass"));
            value.ShouldEqual("<Simple Test='Pass' />");
        }
    }
}