using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using XunitShould.Sdk;

namespace XunitShould
{
    /// <summary>
    /// </summary>
    public static partial class ShouldXml
    {
        /// <summary>
        ///     Shoulds the equal.
        /// </summary>
        /// <param name="actual">The actual.</param>
        /// <param name="expected">The expected.</param>
        public static void ShouldEqual(this XElement actual, XElement expected) {
            if (actual == null && expected == null) {
                return;
            }
            if (actual == null || expected == null) {
                throw new EqualException(expected, actual);
            }
            try {
                actual.Name.ShouldEqual(expected.Name);
                actual.Attributes()
                      .ShouldEqual(expected.Attributes());
                if (actual.HasElements) {
                    actual.Elements()
                          .ShouldEqual(expected.Elements());
                }
                else {
                    actual.Value.ShouldEqual(expected.Value);
                }
            }
            catch (Exception ex) {
                throw new AggregateException(new EqualException(expected, actual), ex).Flatten();
            }
        }

        /// <summary>
        ///     Shoulds the equal.
        /// </summary>
        /// <param name="actual">The actual.</param>
        /// <param name="expected">The expected.</param>
        public static void ShouldEqual(this IEnumerable<XElement> actual, IEnumerable<XElement> expected) {
            var actualList = actual.ToList();
            var expectedList = expected.ToList();
            //order matters? depends on schema, so currently we have to assume that it does.
            int index = 0;
            int max = Math.Min(actualList.Count, expectedList.Count);
            for (; index < max; index++) {
                var actualItem = actualList[index];
                var expectedItem = expectedList[index];
                try {
                    actualItem.ShouldEqual(expectedItem);
                }
                catch (Exception ex) {
                    throw new AggregateException(new EnumerableEqualException(expectedItem, actualItem, index, expectedList.Count, actualList.Count), ex)
                        .Flatten();
                }
            }
            if (index != expectedList.Count) {
                throw new EnumerableEqualException(expectedList[index], null, index, expectedList.Count, actualList.Count);
            }
            if (index != actualList.Count) {
                throw new EnumerableEqualException(null, actualList[index], index, expectedList.Count, actualList.Count);
            }
        }

        /// <summary>
        ///     Shoulds the equal.
        /// </summary>
        /// <param name="actual">The actual.</param>
        /// <param name="expectedXml">The expected XML.</param>
        public static void ShouldEqual(this XElement actual, string expectedXml) {
            XElement expected = XElement.Parse(expectedXml);
            actual.ShouldEqual(expected);
        }
    }
}