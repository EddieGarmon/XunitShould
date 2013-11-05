using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Xunit.Sdk;

using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class ShouldXml
    {
        public static void ShouldEqual(this XAttribute actual, XAttribute expected) {
            if (!AreEqual(actual, expected)) {
                throw new EqualException(expected, actual);
            }
        }

        public static void ShouldEqual(this IEnumerable<XAttribute> actual, IEnumerable<XAttribute> expected) {
            //order does not matter
            var actualList = actual.Where(a => !a.Name.LocalName.StartsWith("xmlns"))
                                   .OrderBy(a => a.Name.ToString(), StringComparer.Ordinal)
                                   .ToList();
            var expectedList = expected.Where(a => !a.Name.LocalName.StartsWith("xmlns"))
                                       .OrderBy(a => a.Name.ToString(), StringComparer.Ordinal)
                                       .ToList();
            int index = 0;
            int end = Math.Min(actualList.Count, expectedList.Count);
            for (; index < end; index++) {
                var actualItem = actualList[index];
                var expectedItem = expectedList[index];
                if (!AreEqual(actualItem, expectedItem)) {
                    throw new EnumerableEqualException(expectedItem, actualItem, index, expectedList.Count, actualList.Count);
                }
            }
            if (index != expectedList.Count) {
                throw new EnumerableEqualException(expectedList[index], null, index, expectedList.Count, actualList.Count);
            }
            if (index != actualList.Count) {
                throw new EnumerableEqualException(null, actualList[index], index, expectedList.Count, actualList.Count);
            }
        }

        private static bool AreEqual(XAttribute left, XAttribute right) {
            if (left == null && right == null) {
                return true;
            }
            if (left == null || right == null) {
                return false;
            }
            return AreEqual(left.Name, right.Name) && left.Value == right.Value;
        }
    }
}