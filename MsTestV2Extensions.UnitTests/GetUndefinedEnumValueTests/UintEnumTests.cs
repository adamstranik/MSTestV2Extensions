using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MsTestV2Extensions.UnitTests.GetUndefinedEnumValueTests
{
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum UintEnum : uint
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Min = uint.MinValue,
        Max = uint.MaxValue,
    }
    [TestClass]
    public class UintEnumTests : GetUndefinedValueTests<UintEnum>
    { }

    [Flags]
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum UintAllFlags : uint
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Zero = (uint)0,
        Shift0 = (uint)1 << 0,
        Shift1 = (uint)1 << 1,
        Shift2 = (uint)1 << 2,
        Shift3 = (uint)1 << 3,
        Shift4 = (uint)1 << 4,
        Shift5 = (uint)1 << 5,
        Shift6 = (uint)1 << 6,
        Shift7 = (uint)1 << 7,
        Shift8 = (uint)1 << 8,
        Shift9 = (uint)1 << 9,
        Shift10 = (uint)1 << 10,
        Shift11 = (uint)1 << 11,
        Shift12 = (uint)1 << 12,
        Shift13 = (uint)1 << 13,
        Shift14 = (uint)1 << 14,
        Shift15 = (uint)1 << 15,
        Shift16 = (uint)1 << 16,
        Shift17 = (uint)1 << 17,
        Shift18 = (uint)1 << 18,
        Shift19 = (uint)1 << 19,
        Shift20 = (uint)1 << 20,
        Shift21 = (uint)1 << 21,
        Shift22 = (uint)1 << 22,
        Shift23 = (uint)1 << 23,
        Shift24 = (uint)1 << 24,
        Shift25 = (uint)1 << 25,
        Shift26 = (uint)1 << 26,
        Shift27 = (uint)1 << 27,
        Shift28 = (uint)1 << 28,
        Shift29 = (uint)1 << 29,
        Shift30 = (uint)1 << 30,
        Shift31 = (uint)1 << 31,
        Max = uint.MaxValue,
    }

    [TestClass]
    public class UintAllFlagsTest : ThrowsArgumentExceptionTests<UintAllFlags>
    { }

}
