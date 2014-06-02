using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using XunitShould.Sdk;

namespace XunitShould
{
    [Serializable]
    [DebuggerDisplay("Count = {_innerExceptions.Count}")]
    internal class AggregateException : XunitException
    {
        private readonly List<Exception> _innerExceptions;

        public AggregateException(IEnumerable<Exception> innerExceptions) {
            if (innerExceptions == null) {
                throw new ArgumentNullException("innerExceptions");
            }
            _innerExceptions = new List<Exception>(innerExceptions);
        }

        public AggregateException(params Exception[] innerExceptions) {
            if (innerExceptions == null) {
                throw new ArgumentNullException("innerExceptions");
            }
            _innerExceptions = new List<Exception>(innerExceptions);
        }

        protected AggregateException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            var innerExceptions = info.GetValue("InnerExceptions", typeof(Exception[])) as Exception[];
            if (innerExceptions == null) {
                throw new SerializationException("No inner exceptions found.");
            }

            _innerExceptions = new List<Exception>(innerExceptions);
        }

        public IEnumerable<Exception> InnerExceptions {
            get { return _innerExceptions; }
        }

        public override string Message {
            get {
                return _innerExceptions.Aggregate("One or more exceptions occured:",
                                                  (current, ex) =>
                                                  String.Format(CultureInfo.InvariantCulture, "{0}{1}---- {2}{1}", current, Environment.NewLine, ex));
            }
        }

        public AggregateException Flatten() {
            // Initialize a collection to contain the flattened exceptions.
            var flattenedExceptions = new List<Exception>();

            // Create a list to remember all aggregates to be flattened, this will be accessed like a FIFO queue
            var exceptionsToFlatten = new List<AggregateException> { this };
            int nDequeueIndex = 0;

            // Continue removing and recursively flattening exceptions, until there are no more.
            while (exceptionsToFlatten.Count > nDequeueIndex) {
                // dequeue one from exceptionsToFlatten 
                IList<Exception> currentInnerExceptions = exceptionsToFlatten[nDequeueIndex++]._innerExceptions;

                foreach (Exception currentInnerException in currentInnerExceptions) {
                    if (currentInnerException == null) {
                        continue;
                    }

                    var currentInnerAsAggregate = currentInnerException as AggregateException;

                    // If this exception is an aggregate, keep it around for later.  Otherwise, 
                    // simply add it to the list of flattened exceptions to be returned.
                    if (currentInnerAsAggregate != null) {
                        exceptionsToFlatten.Add(currentInnerAsAggregate);
                    }
                    else {
                        flattenedExceptions.Add(currentInnerException);
                    }
                }
            }

            return new AggregateException(flattenedExceptions);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);

            var innerExceptions = new Exception[_innerExceptions.Count];
            _innerExceptions.CopyTo(innerExceptions, 0);
            info.AddValue("InnerExceptions", innerExceptions, typeof(Exception[]));
        }
    }
}