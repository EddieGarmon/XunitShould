using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace XunitShould.Sdk
{
    /// <summary>
    /// Exception thrown when a set is not a proper subset of another set.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    public class ProperSubsetException : AssertActualExpectedException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ProperSubsetException"/> class.
        /// </summary>
        public ProperSubsetException(IEnumerable expected, IEnumerable actual)
            : base(expected, actual, "ProperSubset() Failure") { }
    }
}