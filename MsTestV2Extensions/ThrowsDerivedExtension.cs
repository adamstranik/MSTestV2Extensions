using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MsTestV2Extensions
{
    /// <summary>
    /// Defines extension.
    /// </summary>
    public static class AssertThrowsDerivedExtension
    {
        /// <summary>
        /// Contains list of exceptions types that are forbidden to be used in
        /// <see cref="ThrowsDerived{Texception}"/>.
        /// </summary>
        public static readonly IEnumerable<Type> AssertThrowsDerivedForbiddenTypes = new[]
        {
            typeof(Exception), // should catch all exceptions including assertion exceptions

            // the following types shall hide exceptions used by MSTest to signalize that assertion failed
            typeof(UnitTestAssertException),     // Derived from Exception
            typeof(AssertFailedException),       // Derived from UnitTestAssertException
            typeof(AssertInconclusiveException), // Derived from UnitTestAssertException
        };

        /// <summary>
        /// Tests whether the code specified by the <paramref name="action"/> throws
        /// <typeparamref name="TException"/> or derived and throws
        /// <see cref="AssertFailedException"/> if it does not.
        /// </summary>
        /// <typeparam name="TException">
        /// The base exception type that can be thrown.
        /// </typeparam>
        /// <param name="assert">
        /// The instance of <see cref="Assert"/> the extension belongs to. Theoretically, the value
        /// can be <see langword="null"/>.
        /// </param>
        /// <param name="action">
        /// The action supposed to throw <typeparamref name="TException"/> or derived.
        /// </param>
        /// <returns>
        /// Not-<see langword="null"/> exception thrown by <paramref name="action"/>.
        /// </returns>
        /// <exception cref="AssertFailedException">
        /// If the <paramref name="action"/> does not throw an exception or throws different
        /// exception than <typeparamref name="TException"/> or derived.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <typeparamref name="TException"/> is forbidden. To see forbidden exception types, see
        /// <see cref="AssertThrowsDerivedForbiddenTypes"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="action"/> is null.
        /// </exception>
        public static TException ThrowsDerived<TException>(this Assert assert, Action action)
            where TException : Exception
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (AssertThrowsDerivedForbiddenTypes.Contains(typeof(TException)))
                throw new ArgumentException(
                    $"Usage of the following exception types is forbidden: {string.Join(", ", AssertThrowsDerivedForbiddenTypes)}");

            // end of contract check

            try
            {
                action();
            }
            catch (TException ex)
            {
                // thrown exception is derived from T
                return ex;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)// I need to...
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Assert.Fail($"Exception of type {typeof(TException)} or derived is expected but {ex.GetType()} was thrown.");
            }

            Assert.Fail("Exception expected.");
            return null; // This is weird, but without this return the code wont compile. I think it's unreachable.
        }
    }

}
