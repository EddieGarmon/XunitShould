using System.Xml.Linq;
using Xunit;
using XunitShould.Sdk;

namespace XunitShould
{
    public class VerifyAssertNames
    {
        [Fact]
        public void EqualEmptyNamespace() {
            XName expected = XName.Get("Hello");
            XName actual = "Hello";
            actual.ShouldEqual(expected);
        }

        [Fact]
        public void EqualWithNamespace() {
            XName expected = XName.Get("{world}Hello");
            XNamespace world = "world";
            XName actual = world + "Hello";
            actual.ShouldEqual(expected);
        }

        [Fact]
        public void MismatchedLocalNameThrows() {
            XName expected = XName.Get("{world}HiThere");
            XName actual = XName.Get("{world}Hello");
            Record.Exception(() => actual.ShouldEqual(expected))
                  .ShouldEqual(new EqualException(expected, actual));
        }

        [Fact]
        public void MismatchedNamespacesThrows() {
            XName expected = XName.Get("Hello");
            XName actual = XName.Get("{world}Hello");
            Record.Exception(() => actual.ShouldEqual(expected))
                  .ShouldEqual(new EqualException(expected, actual));
        }
    }
}