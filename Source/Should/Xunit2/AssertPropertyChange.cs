using System;
using System.ComponentModel;
using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class Should
    {
        /// <summary>
        /// Verifies that the provided object raised INotifyPropertyChanged.PropertyChanged
        /// as a result of executing the given test code.
        /// </summary>
        /// <param name="object">The object which should raise the notification</param>
        /// <param name="propertyName">The property name for which the notification should be raised</param>
        /// <param name="testCode">The test code which should cause the notification to be raised</param>
        /// <exception cref="PropertyChangedException">Thrown when the notification is not raised</exception>
        public static void ShouldChangeProperty(this INotifyPropertyChanged @object, string propertyName, Action testCode) {
            if (@object == null) {
                throw new ArgumentNullException("object");
            }
            if (testCode == null) {
                throw new ArgumentNullException("testCode");
            }

            bool propertyChangeHappened = false;

            PropertyChangedEventHandler handler = (sender, args) => {
                                                      if (propertyName.Equals(args.PropertyName, StringComparison.OrdinalIgnoreCase)) {
                                                          propertyChangeHappened = true;
                                                      }
                                                  };

            @object.PropertyChanged += handler;

            try {
                testCode();
                if (!propertyChangeHappened) {
                    throw new PropertyChangedException(propertyName);
                }
            }
            finally {
                @object.PropertyChanged -= handler;
            }
        }
    }
}