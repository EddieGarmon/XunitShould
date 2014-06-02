using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace XunitShould.Sdk
{
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    public class DoesNotEndWithException : XunitException
    {
        public DoesNotEndWithException(string expected, string actual)
            : base(
                String.Format(CultureInfo.CurrentCulture,
                              "DoesNotEndWith() Failure:{2}Expected: {0}{2}Actual:   {1}",
                              expected ?? "(null)",
                              actual ?? "(null)",
                              Environment.NewLine)) { }
    }
}