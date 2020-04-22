using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MsTestV2Extensions.UnitTests.GetUndefinedEnumValueTests
{
    public abstract class GetUndefinedValueTests<TEnum>
    where TEnum : Enum
    {
        [TestMethod]
        public void ReturnsUndefinedValue()
        {
            // Arrange

            // Act
            var actual = TestEnumHelper.GetUndefinedValue<TEnum>();

            // Assert
            Assert.IsFalse(Enum.IsDefined(actual.GetType(), actual));
        }
    }

    public abstract class ThrowsArgumentExceptionTests<TEnum>
        where TEnum : Enum
    {
        [TestMethod]
        public void ThrowsIfAllEnumValuesAreDefined()
        {
            // Arrange

            // Act + Assert
            Assert.ThrowsException<ArgumentException>(
                () => TestEnumHelper.GetUndefinedValue<TEnum>());
        }
    }

}
