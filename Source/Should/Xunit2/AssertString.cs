using System;
using System.Text.RegularExpressions;
using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class Should
    {
        public static void ShouldContain(this string actual, string fragment, StringComparison comparisonType = StringComparison.CurrentCulture) {
            if (actual == null || actual.IndexOf(fragment, comparisonType) < 0) {
                throw new ContainsException(fragment, actual);
            }
        }

        public static void ShouldEndWith(this string actual, string ending, StringComparison comparisonType = StringComparison.CurrentCulture) {
            if (ending == null || actual == null || !actual.EndsWith(ending, comparisonType)) {
                throw new EndsWithException(ending, actual);
            }
        }

        public static void ShouldEqual(this string actual, string expected, StringComparison comparisonType) {
            if (actual == null && expected == null) {
                return;
            }
            if (Comparers.GetComparer(comparisonType)
                         .Compare(actual, expected) != 0) {
                throw new EqualException(expected, actual);
            }
        }

        public static void ShouldEqual(this string actual,
                                       string expected,
                                       bool ignoreCase = false,
                                       bool ignoreLineEndingDifferences = false,
                                       bool ignoreWhiteSpaceDifferences = false) {
            // Start out assuming that one of the values is null
            int expectedIndex = -1;
            int actualIndex = -1;
            int expectedLength = 0;
            int actualLength = 0;

            if (expected == null) {
                if (actual == null) {
                    return;
                }
            }
            else if (actual != null) {
                // Walk the string, keeping separate indices since we can skip variable amounts of
                // data based on ignoreLineEndingDifferences and ignoreWhiteSpaceDifferences.
                expectedIndex = 0;
                actualIndex = 0;
                expectedLength = expected.Length;
                actualLength = actual.Length;

                while (expectedIndex < expectedLength && actualIndex < actualLength) {
                    char expectedChar = expected[expectedIndex];
                    char actualChar = actual[actualIndex];

                    if (ignoreLineEndingDifferences && IsLineEnding(expectedChar) && IsLineEnding(actualChar)) {
                        expectedIndex = SkipLineEnding(expected, expectedIndex);
                        actualIndex = SkipLineEnding(actual, actualIndex);
                    }
                    else if (ignoreWhiteSpaceDifferences && IsWhiteSpace(expectedChar) && IsWhiteSpace(actualChar)) {
                        expectedIndex = SkipWhitespace(expected, expectedIndex);
                        actualIndex = SkipWhitespace(actual, actualIndex);
                    }
                    else {
                        if (ignoreCase) {
                            expectedChar = Char.ToUpperInvariant(expectedChar);
                            actualChar = Char.ToUpperInvariant(actualChar);
                        }

                        if (expectedChar != actualChar) {
                            break;
                        }

                        expectedIndex++;
                        actualIndex++;
                    }
                }
            }

            if (expectedIndex < expectedLength || actualIndex < actualLength) {
                throw new EqualException(expected, actual, expectedIndex, actualIndex);
            }
        }

        public static void ShouldMatch(this string actual, string regexPattern) {
            if (regexPattern == null) {
                throw new ArgumentNullException("regexPattern");
            }

            if (actual == null || !Regex.IsMatch(actual, regexPattern)) {
                throw new MatchesException(regexPattern, actual);
            }
        }

        public static void ShouldMatch(this string actual, Regex pattern) {
            if (pattern == null) {
                throw new ArgumentNullException("pattern");
            }

            if (actual == null || !pattern.IsMatch(actual)) {
                throw new MatchesException(pattern.ToString(), actual);
            }
        }

        public static void ShouldNotContain(this string actual, string fragment, StringComparison comparisonType = StringComparison.CurrentCulture) {
            if (actual != null && actual.IndexOf(fragment, comparisonType) >= 0) {
                throw new DoesNotContainException(fragment);
            }
        }

        public static void ShouldNotEndWith(this string actual, string ending, StringComparison comparisonType = StringComparison.CurrentCulture) {
            if (actual != null && actual.EndsWith(ending, comparisonType)) {
                throw new DoesNotEndWithException(ending, actual);
            }
        }

        public static void ShouldNotMatch(this string actual, string regexPattern) {
            if (regexPattern == null) {
                throw new ArgumentNullException("regexPattern");
            }

            if (actual != null && Regex.IsMatch(actual, regexPattern)) {
                throw new DoesNotMatchException(regexPattern, actual);
            }
        }

        public static void ShouldNotMatch(this string actual, Regex pattern) {
            if (pattern == null) {
                throw new ArgumentNullException("pattern");
            }

            if (actual != null && pattern.IsMatch(actual)) {
                throw new DoesNotMatchException(pattern.ToString(), actual);
            }
        }

        public static void ShouldNotStartWith(this string actual, string begining, StringComparison comparisonType = StringComparison.CurrentCulture) {
            if (actual != null && actual.StartsWith(begining, comparisonType)) {
                throw new DoesNotStartWithException(begining, actual);
            }
        }

        public static void ShouldStartWith(this string actual, string begining, StringComparison comparisonType = StringComparison.CurrentCulture) {
            if (begining == null || actual == null || !actual.StartsWith(begining, comparisonType)) {
                throw new StartsWithException(begining, actual);
            }
        }

        private static bool IsLineEnding(char c) {
            return c == '\r' || c == '\n';
        }

        private static bool IsWhiteSpace(char c) {
            return c == ' ' || c == '\t';
        }

        private static int SkipLineEnding(string value, int index) {
            if (value[index] == '\r') {
                ++index;
            }
            if (index < value.Length && value[index] == '\n') {
                ++index;
            }

            return index;
        }

        private static int SkipWhitespace(string value, int index) {
            while (index < value.Length) {
                switch (value[index]) {
                    case ' ':
                    case '\t':
                        index++;
                        break;

                    default:
                        return index;
                }
            }

            return index;
        }
    }
}