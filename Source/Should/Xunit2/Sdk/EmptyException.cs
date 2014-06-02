using System.Diagnostics.CodeAnalysis;

namespace XunitShould.Sdk
{
    /// <summary>
    /// Exception thrown when a collection is unexpectedly not empty.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    public class EmptyException : XunitException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EmptyException"/> class.
        /// </summary>
        public EmptyException()
            : base("Empty() Failure") { }
    }
}