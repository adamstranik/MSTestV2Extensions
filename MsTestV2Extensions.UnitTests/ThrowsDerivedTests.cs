using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MsTestV2Extensions.UnitTests
{
    public class ThrowsDerivedBaseException : Exception
    {
        public ThrowsDerivedBaseException(string message)
            : base(message)
        { }
    }

    public class ThrowsDerivedException : ThrowsDerivedBaseException
    {
        public ThrowsDerivedException(string message)
            : base(message)
        { }
    }

    [TestClass]
    public class ThrowsDerivedTests
    {
        [TestMethod]
        public void PassesIfExactExceptionIsThrown()
        {
            // Arrange
            void TestedAction() { throw new ThrowsDerivedBaseException("message"); }

            // Act
            var ex = Assert.That.ThrowsDerived<ThrowsDerivedBaseException>(TestedAction);

            // Assert
            Assert.IsInstanceOfType(ex, typeof(ThrowsDerivedBaseException), "Exception type");
            Assert.AreEqual("message", ex.Message, "Exception message");
        }

        [TestMethod]
        public void PassesIfDerivedExceptionIsThrown()
        {
            // Arrange
            void TestedAction() { throw new ThrowsDerivedException("message"); }

            // Act
            var ex = Assert.That.ThrowsDerived<ThrowsDerivedBaseException>(TestedAction);

            // Assert
            Assert.IsInstanceOfType(ex, typeof(ThrowsDerivedException), "Exception type");
            Assert.AreEqual("message", ex.Message, "Exception message");
        }

        [TestMethod]
        public void AssertionFailsIfNoExceptionIsThrown()
        {
            // Arrange
            void TestedAction() { } // does nothing i.e. it doesn't throw an exception
            var assertFailExceptionThrown = false;

            // Act
            try
            {
                // expects that some exception is thrown but it's not, assertion fails and it's
                // propagated as AssertionFailException
                Assert.That.ThrowsDerived<InvalidOperationException>(TestedAction);
            }
            catch (AssertFailedException)
            {
                assertFailExceptionThrown = true;
            }

            // Assert
            Assert.IsTrue(assertFailExceptionThrown);
        }

        [TestMethod]
        public void AssertionFailsIfNotDerivedExceptionIsThrown()
        {
            // Arrange
            void TestedAction() { throw new InvalidOperationException(); }
            var assertFailExceptionThrown = false;

            // Act
            try
            {
                Assert.That.ThrowsDerived<ThrowsDerivedBaseException>(TestedAction);
            }
            catch (AssertFailedException)
            {
                assertFailExceptionThrown = true;
            }

            // Assert
            Assert.IsTrue(assertFailExceptionThrown);
        }

        [TestMethod]
        public void ThrowsIfActionIsNull()
        {
            // Arrange

            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => Assert.That.ThrowsDerived<InvalidOperationException>(null));
        }

        [TestMethod]
        public void ThrowsIfExceptionTypeIsUsed()
        {
            // Arrange

            // Act + Assert
            Assert.ThrowsException<ArgumentException>(
                () => Assert.That.ThrowsDerived<Exception>(delegate { }));
        }
    }
}
