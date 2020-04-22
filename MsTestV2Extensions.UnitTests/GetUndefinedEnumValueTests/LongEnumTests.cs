using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MsTestV2Extensions.UnitTests.GetUndefinedEnumValueTests
{
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum LongEnum : long
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Min = long.MinValue,
        Zero = 0,
        Max = long.MaxValue,
    }
    [TestClass]
    public class LongEnumTests : GetUndefinedValueTests<LongEnum>
    { }

    [Flags]
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum LongAllFlags : long
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Zero = 0,
        Shift0 = (long)1 << 0,
        Shift1 = (long)1 << 1,
        Shift2 = (long)1 << 2,
        Shift3 = (long)1 << 3,
        Shift4 = (long)1 << 4,
        Shift5 = (long)1 << 5,
        Shift6 = (long)1 << 6,
        Shift7 = (long)1 << 7,
        Shift8 = (long)1 << 8,
        Shift9 = (long)1 << 9,

        Shift10 = (long)1 << 10,
        Shift11 = (long)1 << 11,
        Shift12 = (long)1 << 12,
        Shift13 = (long)1 << 13,
        Shift14 = (long)1 << 14,
        Shift15 = (long)1 << 15,
        Shift16 = (long)1 << 16,
        Shift17 = (long)1 << 17,
        Shift18 = (long)1 << 18,
        Shift19 = (long)1 << 19,

        Shift20 = (long)1 << 20,
        Shift21 = (long)1 << 21,
        Shift22 = (long)1 << 22,
        Shift23 = (long)1 << 23,
        Shift24 = (long)1 << 24,
        Shift25 = (long)1 << 25,
        Shift26 = (long)1 << 26,
        Shift27 = (long)1 << 27,
        Shift28 = (long)1 << 28,
        Shift29 = (long)1 << 29,

        Shift30 = (long)1 << 30,
        Shift31 = (long)1 << 31,
        Shift32 = (long)1 << 32,
        Shift33 = (long)1 << 33,
        Shift34 = (long)1 << 34,
        Shift35 = (long)1 << 35,
        Shift36 = (long)1 << 36,
        Shift37 = (long)1 << 37,
        Shift38 = (long)1 << 38,
        Shift39 = (long)1 << 39,

        Shift40 = (long)1 << 40,
        Shift41 = (long)1 << 41,
        Shift42 = (long)1 << 42,
        Shift43 = (long)1 << 43,
        Shift44 = (long)1 << 44,
        Shift45 = (long)1 << 45,
        Shift46 = (long)1 << 46,
        Shift47 = (long)1 << 47,
        Shift48 = (long)1 << 48,
        Shift49 = (long)1 << 49,

        Shift50 = (long)1 << 50,
        Shift51 = (long)1 << 51,
        Shift52 = (long)1 << 52,
        Shift53 = (long)1 << 53,
        Shift54 = (long)1 << 54,
        Shift55 = (long)1 << 55,
        Shift56 = (long)1 << 56,
        Shift57 = (long)1 << 57,
        Shift58 = (long)1 << 58,
        Shift59 = (long)1 << 59,

        Shift60 = (long)1 << 60,
        Shift61 = (long)1 << 61,
        Shift62 = (long)1 << 62,
        Shift63 = (long)1 << 63,

        Max = long.MaxValue,
    }

    [TestClass]
    public class LongAllFlagsTests : ThrowsArgumentExceptionTests<LongAllFlags>
    { }

}
