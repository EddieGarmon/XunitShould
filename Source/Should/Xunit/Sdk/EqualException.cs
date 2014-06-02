using System.Runtime.Serialization;

namespace XunitShould.Sdk
{
    public class EqualException : Xunit.Sdk.EqualException
    {
        public EqualException(object expected, object actual)
            : base(expected, actual) { }

        public EqualException(object expected, object actual, bool skipPositionCheck)
            : base(expected, actual, skipPositionCheck) { }

        protected EqualException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}