using System.Xml.Linq;
using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class ShouldXml
    {
        public static void ShouldEqual(this XName actual, XName expected) {
            if (!AreEqual(actual, expected)) {
                throw new EqualException(expected, actual);
            }
        }

        public static void ShouldEqual(this XName actual, string expected) {
            if (actual == null && expected == null) {
                return;
            }
            if (actual == null || expected == null || actual.ToString() != expected) {
                throw new EqualException(expected, actual);
            }
        }

        private static bool AreEqual(XName left, XName right) {
            if (left == null && right == null) {
                return true;
            }
            if (left == null || right == null) {
                return false;
            }
            return left.ToString() == right.ToString();
        }
    }
}