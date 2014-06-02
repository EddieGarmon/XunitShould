using System;
using System.Reflection;
using XunitShould.Sdk;

namespace XunitShould
{
    public static partial class Should
    {
        public static T ShouldBeAssignableTo<T>(this object @object) {
            ShouldBeAssignableTo(@object, typeof(T));
            return (T)@object;
        }

        public static void ShouldBeAssignableTo(this object @object, Type type) {
            if (type == null) {
                throw new ArgumentNullException("type");
            }

            if (@object == null || !type.GetTypeInfo()
                                        .IsAssignableFrom(@object.GetType()
                                                                 .GetTypeInfo())) {
                throw new IsAssignableToException(type, @object);
            }
        }

        /// <summary>
        ///     Verifies that an object is exactly the given type (and not a derived type).
        /// </summary>
        /// <typeparam name="T">The type the object should be</typeparam>
        /// <param name="object">The object to be evaluated</param>
        /// <returns>The object, casted to type T when successful</returns>
        /// <exception cref="IsTypeException">Thrown when the object is not the given type</exception>
        public static T ShouldBeInstanceOf<T>(this object @object) {
            ShouldBeInstanceOf(@object, typeof(T));
            return (T)@object;
        }

        /// <summary>
        ///     Verifies that an object is exactly the given type (and not a derived type).
        /// </summary>
        /// <param name="object">The object to be evaluated</param>
        /// <param name="expectedType">The type the object should be</param>
        /// <exception cref="IsTypeException">Thrown when the object is not the given type</exception>
        public static void ShouldBeInstanceOf(this object @object, Type expectedType) {
            if (expectedType == null) {
                throw new ArgumentNullException("expectedType");
            }

            if (@object == null) {
                throw new IsTypeException(expectedType.FullName, null);
            }

            Type actualType = @object.GetType();
            if (expectedType != actualType) {
                string expectedTypeName = expectedType.FullName;
                string actualTypeName = actualType.FullName;

                if (expectedTypeName == actualTypeName) {
                    expectedTypeName += String.Format(" ({0})",
                                                      expectedType.GetTypeInfo()
                                                                  .Assembly.GetName()
                                                                  .FullName);
                    actualTypeName += String.Format(" ({0})",
                                                    actualType.GetTypeInfo()
                                                              .Assembly.GetName()
                                                              .FullName);
                }

                throw new IsTypeException(expectedTypeName, actualTypeName);
            }
        }

        /// <summary>
        ///     Verifies that an object is not exactly the given type.
        /// </summary>
        /// <typeparam name="T">The type the object should not be</typeparam>
        /// <param name="object">The object to be evaluated</param>
        /// <exception cref="IsNotTypeException">Thrown when the object is the given type</exception>
        public static void ShouldNotBeInstanceOf<T>(this object @object) {
            ShouldNotBeInstanceOf(@object, typeof(T));
        }

        /// <summary>
        ///     Verifies that an object is not exactly the given type.
        /// </summary>
        /// <param name="object">The object to be evaluated</param>
        /// <param name="expectedType">The type the object should not be</param>
        /// <exception cref="IsNotTypeException">Thrown when the object is the given type</exception>
        public static void ShouldNotBeInstanceOf(this object @object, Type expectedType) {
            if (expectedType == null) {
                throw new ArgumentNullException("expectedType");
            }

            if (@object != null && expectedType.Equals(@object.GetType())) {
                throw new IsNotTypeException(expectedType, @object);
            }
        }
    }
}