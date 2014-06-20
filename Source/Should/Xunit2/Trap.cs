using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace XunitShould
{
    /// <summary>
    /// Allows the user to record actions for a test.
    /// </summary>
    public static class Trap
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
                var task = testCode() as Task;
                //if we were passes a task, we must wait on it...
                if (task != null) {
                    return AwaitTask(task)
                        .Result;
                }
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }

        private static async Task<Exception> AwaitTask(Task task) {
            try {
                await task;
                return null;
            }
            catch (Exception ex) {
                return ex;
            }
        }
    }
}