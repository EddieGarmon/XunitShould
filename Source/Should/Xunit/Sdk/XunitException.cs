using System.Runtime.Serialization;

namespace Xunit.Sdk
{
    public class XunitException : AssertException
    {
        public XunitException() { }

        public XunitException(string message)
            : base(message) { }

        protected XunitException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}