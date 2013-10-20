using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Xunit.Sdk;

namespace XunitShould
{
    public static class XAttributeExtensions
    {
        public static void ShouldEqual(this XAttribute actual, XAttribute expected) {
            actual.Name.ShouldEqual(expected.Name);
            if (actual.Value != expected.Value) {
                throw new EqualException(expected, actual);
            }
        }

        public static void ShouldEqual(this IEnumerable<XAttribute> actual, IEnumerable<XAttribute> expected) {
            //order does not matter
            var actualList = new List<XAttribute>(actual);
            TrimNamespaceAttributes(actualList);
            actualList.Sort((left, right) => string.CompareOrdinal(left.Name.ToString(), right.Name.ToString()));
            var expectedList = new List<XAttribute>(expected);
            TrimNamespaceAttributes(expectedList);
            expectedList.Sort((left, right) => string.CompareOrdinal(left.Name.ToString(), right.Name.ToString()));
            if (actualList.Count != expectedList.Count) {
                throw new EqualException(String.Join(" ",
                                                     expectedList.Select(x => x.ToString())
                                                                 .ToArray()),
                                         String.Join(" ",
                                                     actualList.Select(x => x.ToString())
                                                               .ToArray()));
            }
            for (int i = 0; i < actualList.Count; i++) {
                actualList[i].ShouldEqual(expectedList[i]);
            }
        }

        private static void TrimNamespaceAttributes(IList<XAttribute> attributes) {
            for (int i = 0; i < attributes.Count; i++) {
                if (!attributes[i].Name.LocalName.StartsWith("xmlns")) {
                    continue;
                }
                attributes.RemoveAt(i);
                i--;
            }
        }
    }
}