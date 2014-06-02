using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;

namespace XunitShould
{
    public class VerifyAssertAttributes
    {
        [Fact]
        public void EqualList() {
            var expected = new List<XAttribute> { new XAttribute("name1", "value1"), new XAttribute("name2", "value2") };
            var actual = new[] { new XAttribute("name2", "value2"), new XAttribute("name1", "value1") };
            actual.ShouldEqual(expected);
        }
    }
}