using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsTestV2Extensions.UnitTests.GetUndefinedEnumValueTests
{
#pragma warning disable CA1028 // Enum Storage should be Int32
    public enum ShortEnum : short
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        Min = short.MinValue,
        Zero = 0,
        Max = short.MaxValue,
    }
    [TestClass]
    public class ShortEnumTests : GetUndefinedValueTests<ShortEnum>
    { }
}
