using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsTestV2Extensions.UnitTests.GetUndefinedEnumValueTests
{
    public enum IntEnum : int
    {
        Min = int.MinValue,
        Zero = 0,
        Max = int.MaxValue,
    }
    [TestClass]
    public class IntEnumTests : GetUndefinedValueTests<IntEnum>
    { }

}
