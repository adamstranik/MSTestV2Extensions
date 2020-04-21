using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsTestV2Extensions
{
    /// <summary>
    /// Defines the extension NoExceptionThrown
    /// </summary>
    public static class NoExceptionThrownExtension
    {
        /// <summary>
        /// Performs no action, just letting know the intention: no exception is expected in this test.
        /// </summary>
        public static void NoExceptionThrown(this Assert assert)
        {
            // nothing to do, it's just a syntactic sugar
        }
    }
}
