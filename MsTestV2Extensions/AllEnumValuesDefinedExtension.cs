using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MsTestV2Extensions
{
    /// <summary>
    /// Defines extension
    /// </summary>
    public static class AllEnumValuesDefinedExtension
    {
        /// <summary>
        /// Returns value or <see cref="DataRowAttribute"/> at <paramref name="index"/> position.
        /// </summary>
        /// <typeparam name="T">Type of value at <paramref name="index"/> position.</typeparam>
        /// <param name="attributeProvider">The attribute provider.</param>
        /// <param name="index">Zero-based index.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="attributeProvider"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// If <paramref name="index"/> is invalid.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// If <paramref name="index"/> value if not type of <typeparamref name="T"/>.
        /// </exception>
        private static IEnumerable<T> GetDataRowValues<T>(
            ICustomAttributeProvider attributeProvider,
            int index)
        {
            if (attributeProvider == null)
                throw new ArgumentNullException(nameof(attributeProvider));

            // end of contract check

            var rawValues = attributeProvider.GetCustomAttributes(typeof(DataRowAttribute), false);

            return rawValues.Select(val =>
            {
                var value = ((DataRowAttribute)val).Data[index];
                if (value.GetType() == typeof(T))
                {
                    return (T)value;
                }
                else
                {
                    throw new InvalidCastException($"Specified position");
                }
            });
        }


        /// <summary>
        /// Asserts that all values of <typeparamref name="TEnum"/> are specified for the test in
        /// <see cref="DataRowAttribute"/>s.
        /// </summary>
        /// <typeparam name="TEnum">The enum whose values to test.</typeparam>
        /// <param name="assert">
        /// The instance of <see cref="Assert"/> the extension belongs to. Theoretically, the value
        /// can be <see langword="null"/>.
        /// </param>
        /// <param name="callingMethod">
        /// The test method with <see cref="DataRowAttribute"/>.
        /// </param>
        /// <param name="position">
        /// Zero-based position of enum specification in <see cref="DataRowAttribute"/>.
        /// </param>
        /// <param name="notDefinedOnPurpose">
        /// A list of <typeparamref name="TEnum"/> values that are not defined on purpose for some
        /// reason.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="callingMethod"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="AssertFailedException">
        /// If assertion fail.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// If <paramref name="position"/> is an invalid index.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// If <paramref name="position"/>-th value is not type of <typeparamref name="TEnum"/>.
        /// </exception>
        /// <remarks>
        /// <para>
        /// The extension is useful if a conversion from one enum to another is required. Using
        /// this extension, it's possible to check that all (or required) enum values are defined.
        /// If the enum changes and new values are added, the tests start to fail unless new
        /// <see cref="DataRowAttribute"/> is added or the value is explicitly omitted.
        /// </para>
        /// </remarks>
        public static void AllEnumValuesDefined<TEnum>(this Assert assert,
            MethodBase callingMethod, int position, params TEnum[] notDefinedOnPurpose)
            where TEnum : Enum
        {
            if (callingMethod == null)
                throw new ArgumentNullException(nameof(callingMethod));

            var definedValues = GetDataRowValues<TEnum>(callingMethod, position).ToList();
            definedValues.AddRange(notDefinedOnPurpose);

            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();

            var filteredValues = enumValues.Except(definedValues);
            CollectionAssert.IsSubsetOf(enumValues.ToArray(), definedValues,
                $"Missing values: {string.Join(", ", filteredValues)}");
        }
    }
}
