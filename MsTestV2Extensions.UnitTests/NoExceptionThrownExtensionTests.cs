using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MsTestV2Extensions.UnitTests
{
    [TestClass]
    public class NoExceptionThrownExtensionTests
    {
        [TestMethod]
        public void DoesNotThrowIfCalledAsExtension()
        {
            // Arrange

            // Act
            Assert.That.NoExceptionThrown();

            // Assert
            // Nothing to assert here
        }

        public static IEnumerable<object[]> AssertInstanceProvider
        {
            get
            {
                yield return new object[] { Assert.That };
                yield return new object[] { null };
            }
        }

        [TestMethod]
        [DynamicData(nameof(AssertInstanceProvider), DynamicDataSourceType.Property)]
        public void DoesNotThrowIfCalledAsStaticMethod(Assert instance)
        {
            // Arrange

            // Act
            NoExceptionThrownExtension.NoExceptionThrown(instance);

            // Assert
            // Nothing to assert here
        }
    }
}
