using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace XunitShould.Sdk
{
    internal static class Comparers
    {
        internal static readonly IEqualityComparer DefaultEnumerableItemComparer =
            new AssertEqualityComparerAdapter<object>(new AssertEqualityComparer<object>());

        internal static readonly TypeInfo NullableTypeInfo = typeof(Nullable<>).GetTypeInfo();

        public static IComparer<T> GetComparer<T>() where T : IComparable {
            return new AssertComparer<T>();
        }

        public static IComparer<string> GetComparer(StringComparison comparisonType) {
            switch (comparisonType) {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture;

                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase;

                case StringComparison.Ordinal:
                    return StringComparer.Ordinal;

                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase;

                default:
                    throw new ArgumentOutOfRangeException("comparisonType");
            }
        }

        public static IEqualityComparer<T> GetEqualityComparer<T>(bool skipTypeCheck = false, IEqualityComparer innerComparer = null) {
            return new AssertEqualityComparer<T>(skipTypeCheck, innerComparer);
        }
    }
}