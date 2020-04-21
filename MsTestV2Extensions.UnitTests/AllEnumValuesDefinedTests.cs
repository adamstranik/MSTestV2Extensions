using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;

namespace MsTestV2Extensions.UnitTests
{
    [TestClass]
    public class AllEnumValuesDefinedTests
    {
        public enum TestEnum
        {
            One,
            Two,
            Three
        }

        [TestMethod]
        public void ThrowsIfCallingMethodIsNull()
        {
            // Arrange

            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(
                () => Assert.That.AllEnumValuesDefined<TestEnum>(null, default));
        }

        [TestMethod]
        [DataRow(TestEnum.One)]
        [DataRow(TestEnum.Two)]
        [DataRow(TestEnum.Three)]
        public void ThrowsIfPositionIsNegative(TestEnum value)
        {
            // Arrange
            var callingMethod = MethodBase.GetCurrentMethod();

            // Act + Assert
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Assert.That.AllEnumValuesDefined<TestEnum>(callingMethod, -1));

            // Assert
        }

        [TestMethod]
        [DataRow(TestEnum.One)]
        [DataRow(TestEnum.Two)]
        [DataRow(TestEnum.Three)]
        public void ThrowsIfPositionIsBeyondDataRowParams(TestEnum value)
        {
            // Arrange
            // this must be outside Assert.ThrowsException because input parameter Action is sometimes
            // considered as method so MethodBase.GetCurrentMethod inside the Action claims it has
            // no DataRowAttribute
            var callingMethod = MethodBase.GetCurrentMethod();

            // Act + Assert
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Assert.That.AllEnumValuesDefined<TestEnum>(callingMethod, 1));

            // Assert
        }

        [TestMethod]
        [DataRow(TestEnum.One, 0)]
        [DataRow(TestEnum.Two, 1)]
        [DataRow(TestEnum.Three, 2)]
        public void ThrowsIfEnumPosReferencesAnotherType(TestEnum enumValue, int dummyValue)
        {
            // Arrange
            var a = enumValue.ToString().Length + dummyValue;  // to use the enumValue and dummyValue, without this line, the project can not be compiled

            // this must be outside Assert.ThrowsException because input parameter Action is sometimes
            // considered as method so MethodBase.GetCurrentMethod inside the Action claims it has
            // no DataRowAttribute
            var callingMethod = MethodBase.GetCurrentMethod();

            // Act+ Assert
            Assert.ThrowsException<InvalidCastException>(
                () => Assert.That.AllEnumValuesDefined<TestEnum>(callingMethod, 1));
        }


        [TestMethod]
        public void AssertionFailIfNoDataRowAttributeSpecified()
        {
            // Arrange
            var expected = $"Missing values: {string.Join(", ", Enum.GetValues(typeof(TestEnum)).Cast<TestEnum>())}";

            // this must be outside Assert.ThrowsException because input parameter Action is sometimes
            // considered as method so MethodBase.GetCurrentMethod inside the Action claims it has
            // no DataRowAttribute
            var callingMethod = MethodBase.GetCurrentMethod();

            // Act + Assert
            var ex = Assert.ThrowsException<AssertFailedException>(
                () => Assert.That.AllEnumValuesDefined<TestEnum>(callingMethod, default));

            Assert.IsTrue(ex.Message.Contains(expected));
        }

        [TestMethod]
        [DataRow(TestEnum.One)]
        [DataRow(TestEnum.Two)]
        [DataRow(TestEnum.Three)]
        public void PassesIfAllEnumsAreDefinedAsDataRowAttribute(TestEnum value)
        {
            // Arrange

            // Act
            Assert.That.AllEnumValuesDefined<TestEnum>(MethodBase.GetCurrentMethod(), 0);

            // Assert
            Assert.That.NoExceptionThrown();
        }

        [TestMethod]
        [DataRow(TestEnum.One)]
        [DataRow(TestEnum.Two)]
        public void PassesIfValuesInDataRowsAttributesAndValuesNotDefinedOnPurposeDefineAllEnumValues(TestEnum value)
        {
            // Arrange

            // Act
            Assert.That.AllEnumValuesDefined(MethodBase.GetCurrentMethod(), 0, TestEnum.Three);

            // Assert
            Assert.That.NoExceptionThrown();
        }

        [TestMethod]
        [DataRow(TestEnum.One)]
        [DataRow(TestEnum.Two)]
        [DataRow(TestEnum.Two)]
        public void PassesIfDataRowAttributesContainSomeValueMultipleTimes(TestEnum value)
        {
            // Arrange

            // Act
            Assert.That.AllEnumValuesDefined(MethodBase.GetCurrentMethod(), 0, TestEnum.Three);

            // Assert
            Assert.That.NoExceptionThrown();
        }


    }
}
