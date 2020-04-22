using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsTestV2Extensions.UnitTests.GetUndefinedEnumValueTests
{
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum UShortEnum : ushort
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Min = ushort.MinValue,
        Max = ushort.MaxValue
    }
    [TestClass]
    public class UshortEnumTests : GetUndefinedValueTests<UShortEnum>
    { }
}
