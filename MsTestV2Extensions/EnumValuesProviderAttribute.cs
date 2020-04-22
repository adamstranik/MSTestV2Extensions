using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MsTestV2Extensions
{
    /// <summary>
    /// The enum values provider attribute; used to get all values of an enum.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Because throwing exceptions from an attribute constructor is not recommended and can lead
    /// to very confusing behavior, another approach is used here.
    /// </para>
    /// <para>
    /// In case of some exceptional state during the attribute construction, the attribute provides
    /// uncommon and definitely unexpected types. This behavior leads to the test failure with
    /// understandable message. The following unexpected types are provided:
    /// <list type="bullet">
    /// <item>
    ///     <term><see cref="ProvidedTypeIsNull"/></term>
    ///     <description>If the instance of <see cref="Type"/> object representing enum type is null.</description>
    /// </item>
    /// <item>
    ///     <term><see cref="ProvidedTypeIsNotEnum"/></term>
    ///     <description>If the instance of <see cref="Type"/> object does not represent an enum.</description>
    /// </item>
    /// <item>
    ///     <term><see cref="OmittedValuesContainsBadType"/></term>
    ///     <description>
    ///     If the provided omitted values contain an object of different <see cref="Type"/> than
    ///     the <see cref="Type"/> representing the enum.
    ///     </description>
    /// </item>
    /// </list>
    /// </para>
    /// <para>
    /// For information about custom data sources see Meziantou's post
    /// <a href="https://www.meziantou.net/mstest-v2-data-tests.htm#custom-datasource">
    ///  MSTest v2: Data tests</a>.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EnumValuesProviderAttribute : Attribute, ITestDataSource
    {
        private readonly IEnumerable<object[]> _dataSource;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA1034 // Nested types should not be visible
        public class ProvidedTypeIsNull { }
        public class ProvidedTypeIsNotEnum { }
        public class OmittedValuesContainsBadType { }
#pragma warning restore CA1034 // Nested types should not be visible
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Initializes a new instance of <see cref="EnumValuesProviderAttribute"/>.
        /// </summary>
        /// <param name="enumType">The type of enum whose values to get.</param>
        /// <param name="omittedValues">The values that should not be returned.</param>
        public EnumValuesProviderAttribute(Type enumType, params object[] omittedValues)
        {
            var dataSource = new List<object[]>();
            var incorrectType = omittedValues.Where(x => x.GetType() != enumType);
            if (enumType == null)
            {
                dataSource.Add(new[] { new ProvidedTypeIsNull() });
            }
            else if (!enumType.IsEnum)
            {
                dataSource.Add(new[] { new ProvidedTypeIsNotEnum() });
            }
            else if (incorrectType.Any())
            {
                dataSource.Add(new[] { new OmittedValuesContainsBadType() });
            }
            else
            {
                var enumValues = Enum.GetValues(enumType);
                foreach (var enumValue in enumValues)
                {
                    if (!omittedValues.Contains(enumValue))
                    {
                        dataSource.Add(new[] { enumValue });
                    }
                }
            }

            _dataSource = dataSource;
        }

        /// <summary>
        /// Provides the data.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            return _dataSource;
        }

        /// <summary>
        /// Provides data to be displayed in parameter-driven test.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            return data == null ?
                string.Empty :
                string.Join(", ", data);
        }
    }
}
