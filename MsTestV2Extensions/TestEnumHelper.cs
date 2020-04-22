using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MsTestV2Extensions
{
    /// <summary>
    /// Groups generally usable methods related to enumerations.
    /// </summary>
    public static class TestEnumHelper
    {
        /// <summary>
        /// Returns a value that is not defined for <typeparamref name="TEnum"/>.
        /// </summary>
        /// <typeparam name="TEnum">The enum type to return.</typeparam>
        /// <returns>Undefined value for <typeparamref name="TEnum"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If <typeparamref name="TEnum"/> contains all possible values for given underlying
        /// type.
        /// </exception>
        /// <remarks>
        /// <para>
        /// According to <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum">
        /// Enumeration types (C# reference)</a>: An enumeration type (or enum type) is a value
        /// type defined by a set of named constants of the underlying <a href= "https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types">
        /// integral numeric type</a>.
        /// </para>
        /// <para>
        /// <a href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#left-shift-operator">
        /// Bitwise and shift operators (C# reference)</a> states there are types whose result
        /// of bit-shift operation is not the original type, but <see cref="int"/>.For these types,
        /// bit-shift operations can not be used and iterating through the possible values is slow.
        /// </para>
        /// </remarks>
        public static TEnum GetUndefinedValue<TEnum>()
            where TEnum : Enum
        {
            var enumType = typeof(TEnum);
            var isFlagsEnum = enumType.GetCustomAttribute<FlagsAttribute>() != null;

            IEnumerable<dynamic> possibleValues;

            switch (Type.GetTypeCode(enumType))
            {
                case TypeCode.SByte:
                    possibleValues = isFlagsEnum ?
                        new DoublePreviousByIncrement<sbyte>(0, sbyte.MaxValue).Get() :
                        new ContinuousSequence<sbyte>(sbyte.MinValue, sbyte.MaxValue).Get();
                    break;
                case TypeCode.Byte:
                    possibleValues = isFlagsEnum ?
                        new DoublePreviousByIncrement<byte>(0, byte.MaxValue).Get() :
                        new ContinuousSequence<byte>(byte.MinValue, byte.MaxValue).Get();
                    break;
                case TypeCode.Int16:
                    possibleValues = isFlagsEnum ?
                        new DoublePreviousByIncrement<short>(0, short.MaxValue).Get() :
                        new ContinuousSequence<short>(short.MinValue, short.MaxValue).Get();
                    break;
                case TypeCode.UInt16:
                    possibleValues = isFlagsEnum ?
                        new DoublePreviousByIncrement<ushort>(0, ushort.MaxValue).Get() :
                        new ContinuousSequence<ushort>(ushort.MinValue, ushort.MaxValue).Get();
                    break;
                case TypeCode.Int32:
                    possibleValues = isFlagsEnum ?
                        new DoublePreviousByBitShift<int>(0, int.MaxValue).Get() :
                        new ContinuousSequence<int>(int.MinValue, int.MaxValue).Get();
                    break;
                case TypeCode.UInt32:
                    possibleValues = isFlagsEnum ?
                        new DoublePreviousByBitShift<uint>(0, uint.MaxValue).Get() :
                        new ContinuousSequence<uint>(uint.MinValue, uint.MaxValue).Get();
                    break;
                case TypeCode.Int64:
                    possibleValues = isFlagsEnum ?
                        new DoublePreviousByBitShift<long>(0, long.MaxValue).Get() :
                        new ContinuousSequence<long>(long.MinValue, long.MaxValue).Get();
                    break;
                case TypeCode.UInt64:
                    possibleValues = isFlagsEnum ?
                        new DoublePreviousByBitShift<ulong>(0, ulong.MaxValue).Get() :
                        new ContinuousSequence<ulong>(ulong.MinValue, ulong.MaxValue).Get();
                    break;

                default:
                    throw new NotImplementedException(Type.GetTypeCode(enumType).ToString());
            }

            return (TEnum)GetFirstUndefined(enumType, possibleValues);
        }

        #region GetUndefinedValue helpers
        private static object GetFirstUndefined(Type enumType, IEnumerable<dynamic> possibleValues)
        {
            try
            {
                return possibleValues.First(value => !Enum.IsDefined(enumType, value));
            }
            catch (InvalidOperationException ex)
            {
                // We define ArgumentException if all possible enum values are defined.
                var message = $"{enumType.Name} defines all possible values.";
                throw new ArgumentException(message, ex);
            }
        }

        private abstract class PossibleValuesProvider<T>
        {
            protected readonly dynamic _min;
            protected readonly dynamic _max;

            protected PossibleValuesProvider(T min, T max)
            {
                _min = min;
                _max = max;
            }

            public abstract IEnumerable<dynamic> Get();
        }

        private class DoublePreviousByBitShift<T> : PossibleValuesProvider<T>
        {
            public DoublePreviousByBitShift(T min, T max)
                : base(min, max)
            { }

            public override IEnumerable<dynamic> Get()
            {
                var current = _min;
                yield return current;

                if (current == 0)
                {
                    yield return ++current;
                }

                while (current <= _max && current > _min)
                {
                    current <<= 1;
                    if (current == 0)
                    {
                        break;
                    }
                    yield return current;
                }

                yield return _max;

                yield break;
            }
        }

        private class DoublePreviousByIncrement<T> : PossibleValuesProvider<T>
        {
            public DoublePreviousByIncrement(T min, T max)
                : base(min, max)
            { }

            public override IEnumerable<dynamic> Get()
            {
                var current = _min;

                yield return current;

                if (current == 0)
                {
                    yield return ++current;
                }

                var target = current << 1;
                while (target <= _max && target > _min)
                {
                    for (var i = current; i < target; i++)
                    {
                        current++;
                    }
                    yield return current;
                    target = current << 1;
                }

                yield return _max;

                yield break;
            }
        }

        private class ContinuousSequence<T> : PossibleValuesProvider<T>
        {
            public ContinuousSequence(T min, T max)
                : base(min, max)
            { }

            public override IEnumerable<dynamic> Get()
            {
                var current = _min;

                yield return current;

                do
                {
                    yield return ++current;
                }
                while (current <= _max && current > _min);

                yield break;
            }
        }

        #endregion
    }
}
