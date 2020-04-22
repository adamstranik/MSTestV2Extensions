using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MsTestV2Extensions.UnitTests.GetUndefinedEnumValueTests
{
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum UlongEnum : ulong
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Min = ulong.MinValue,
        Max = ulong.MaxValue,
    }
    [TestClass]
    public class UlongEnumTests : GetUndefinedValueTests<UlongEnum>
    { }

    [Flags]
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum UlongFlags : ulong
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Zero = 0,
        Shift0 = 1 << 0,
        Shift1 = 1 << 1,
        Shift2 = 1 << 2,
        Shift3 = 1 << 3,
        Shift4 = 1 << 4,
    }

    [TestClass]
    public class UlongFlagsTests : GetUndefinedValueTests<UlongFlags>
    { }

    [Flags]
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum UlongAllFlags : ulong
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Zero = 0,
        Shift0 = (ulong)1 << 0,
        Shift1 = (ulong)1 << 1,
        Shift2 = (ulong)1 << 2,
        Shift3 = (ulong)1 << 3,
        Shift4 = (ulong)1 << 4,
        Shift5 = (ulong)1 << 5,
        Shift6 = (ulong)1 << 6,
        Shift7 = (ulong)1 << 7,
        Shift8 = (ulong)1 << 8,
        Shift9 = (ulong)1 << 9,

        Shift10 = (ulong)1 << 10,
        Shift11 = (ulong)1 << 11,
        Shift12 = (ulong)1 << 12,
        Shift13 = (ulong)1 << 13,
        Shift14 = (ulong)1 << 14,
        Shift15 = (ulong)1 << 15,
        Shift16 = (ulong)1 << 16,
        Shift17 = (ulong)1 << 17,
        Shift18 = (ulong)1 << 18,
        Shift19 = (ulong)1 << 19,

        Shift20 = (ulong)1 << 20,
        Shift21 = (ulong)1 << 21,
        Shift22 = (ulong)1 << 22,
        Shift23 = (ulong)1 << 23,
        Shift24 = (ulong)1 << 24,
        Shift25 = (ulong)1 << 25,
        Shift26 = (ulong)1 << 26,
        Shift27 = (ulong)1 << 27,
        Shift28 = (ulong)1 << 28,
        Shift29 = (ulong)1 << 29,

        Shift30 = (ulong)1 << 30,
        Shift31 = (ulong)1 << 31,
        Shift32 = (ulong)1 << 32,
        Shift33 = (ulong)1 << 33,
        Shift34 = (ulong)1 << 34,
        Shift35 = (ulong)1 << 35,
        Shift36 = (ulong)1 << 36,
        Shift37 = (ulong)1 << 37,
        Shift38 = (ulong)1 << 38,
        Shift39 = (ulong)1 << 39,

        Shift40 = (ulong)1 << 40,
        Shift41 = (ulong)1 << 41,
        Shift42 = (ulong)1 << 42,
        Shift43 = (ulong)1 << 43,
        Shift44 = (ulong)1 << 44,
        Shift45 = (ulong)1 << 45,
        Shift46 = (ulong)1 << 46,
        Shift47 = (ulong)1 << 47,
        Shift48 = (ulong)1 << 48,
        Shift49 = (ulong)1 << 49,

        Shift50 = (ulong)1 << 50,
        Shift51 = (ulong)1 << 51,
        Shift52 = (ulong)1 << 52,
        Shift53 = (ulong)1 << 53,
        Shift54 = (ulong)1 << 54,
        Shift55 = (ulong)1 << 55,
        Shift56 = (ulong)1 << 56,
        Shift57 = (ulong)1 << 57,
        Shift58 = (ulong)1 << 58,
        Shift59 = (ulong)1 << 59,

        Shift60 = (ulong)1 << 60,
        Shift61 = (ulong)1 << 61,
        Shift62 = (ulong)1 << 62,
        Shift63 = (ulong)1 << 63,

        Max = ulong.MaxValue,
    }

    [TestClass]
    public class UlongAllFlagsTests : ThrowsArgumentExceptionTests<UlongAllFlags>
    { }

}
