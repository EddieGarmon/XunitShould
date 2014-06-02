using System;
using System.Diagnostics.CodeAnalysis;

namespace XunitShould
{
    /// <summary>
    /// Allows the user to record actions for a test.
    /// </summary>
    public static class Record
    {
        /// <summary>
        /// Records any exception which is thrown by the given code.
        /// </summary>
        /// <param name="testCode">The code which may thrown an exception.</param>
        /// <returns>Returns the exception that was thrown by the code; null, otherwise.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The caught exception is resurfaced to the user.")]
        public static Exception Exception(Action testCode) {
            if (testCode == null) {
                throw new ArgumentNullException("testCode");
            }

            try {
                testCode();
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }

        /// <summary>
        /// Records any exception which is thrown by the given code that has
        /// a return value. Generally used for testing property accessors.
        /// </summary>
        /// <param name="testCode">The code which may thrown an exception.</param>
        /// <returns>Returns the exception that was thrown by the code; null, otherwise.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The caught exception is resurfaced to the user.")]
        public static Exception Exception(Func<object> testCode) {
            if (testCode == null) {
                throw new ArgumentNullException("testCode");
            }

            try {
                testCode();
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }
    }
}