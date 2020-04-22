using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace MsTestV2Extensions.UnitTests
{
    [TestClass]
    public class EnumValuesProviderAttributeTests
    {
        private readonly MethodInfo _methodInfo = new DynamicMethod("MethodName", typeof(int), new[] { typeof(int) });

        /// <summary>
        /// Arbitrary anum to test the provider
        /// </summary>
        private readonly AttributeTargets[] _enumValues = (AttributeTargets[])Enum.GetValues(typeof(AttributeTargets));

        [TestMethod]
        public void ReturnsAllEnumValues()
        {
            // Arrange
            var sut = new EnumValuesProviderAttribute(typeof(AttributeTargets));

            // Act
            var actualValues = sut.GetData(_methodInfo);

            // Assert
            Assert.IsNotNull(actualValues);

            var actualValuesConverted = actualValues.Select(x => x[0]).ToArray();
            CollectionAssert.AreEqual(_enumValues, actualValuesConverted);
        }

        [TestMethod]
        public void ReturnsAllEnumValuesExceptExplicitlyOmitted()
        {
            // Arrange
            var explicitlyOmitted = new[] { AttributeTargets.Enum, AttributeTargets.All };
            var expectedValues = _enumValues.Except(explicitlyOmitted).ToArray();

            var sut = new EnumValuesProviderAttribute(typeof(AttributeTargets), AttributeTargets.Enum, AttributeTargets.All);

            // Act
            var actualValues = sut.GetData(_methodInfo);

            // Assert
            Assert.IsNotNull(actualValues);

            var actualValuesConverted = actualValues.Select(x => x[0]).ToArray();
            CollectionAssert.AreEqual(expectedValues, actualValuesConverted);
        }

        [TestMethod]
        [EnumValuesProvider(typeof(AttributeTargets))]
        public void AttributeCanBeUsedWithTestMethodAndProvidesExpectedValues(AttributeTargets value)
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(_enumValues.Contains(value));
        }

        [TestMethod]
        [EnumValuesProvider(typeof(AttributeTargets), AttributeTargets.ReturnValue, AttributeTargets.Delegate)]
        public void AttributeCanBeUsedWithTestMethodAndProvidesExpectedValuesExceptOfExplicitlyOmittedOnes(AttributeTargets value)
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(_enumValues.Contains(value));
        }

        [TestMethod]
        [EnumValuesProvider(null)]
        public void ProvidesUnexpectedTypesInDataSourceIfInputTypeIsNull(
            EnumValuesProviderAttribute.ProvidedTypeIsNull value)
        {
            _ = value?.ToString();
        }

        [TestMethod]
        [EnumValuesProvider(typeof(int))]
        public void ProvidesUnexpectedTypesInDataSourceIfInputTypeIsNotEnum(
            EnumValuesProviderAttribute.ProvidedTypeIsNotEnum value)
        {
            _ = value?.ToString();
        }

        [TestMethod]
        [EnumValuesProvider(typeof(AttributeTargets), 1, "foo")]
        public void ProvidesUnexpectedTypesInDataSourceIfTypeOfOmittedValuesContainsBadType(
            EnumValuesProviderAttribute.OmittedValuesContainsBadType value)
        {
            _ = value?.ToString();
        }

    }

}
