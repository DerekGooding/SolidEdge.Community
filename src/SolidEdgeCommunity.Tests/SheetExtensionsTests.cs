using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class SheetExtensionsTests
{
    [TestMethod]
    public void GetEnhancedMetafile_ShouldCallCopyEMFToClipboard()
    {
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        
        try
        {
            mockSheet.Object.GetEnhancedMetafile();
        }
        catch (Exception ex)
        {
            // We expect it to fail because we are not running in a real SE environment
            // and the clipboard might not have the data.
            // But we verify that CopyEMFToClipboard was called.
            Assert.IsTrue(ex.Message.Contains("clipboard") || ex.Message.Contains("available"));
        }

        mockSheet.Verify(s => s.CopyEMFToClipboard(), Times.Once());
    }

    [TestMethod]
    public void SaveAsEnhancedMetafile_ShouldCallCopyEMFToClipboard()
    {
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        
        try
        {
            mockSheet.Object.SaveAsEnhancedMetafile("test.emf");
        }
        catch (Exception ex)
        {
            Assert.IsTrue(ex.Message.Contains("clipboard") || ex.Message.Contains("available"));
        }

        mockSheet.Verify(s => s.CopyEMFToClipboard(), Times.Once());
    }
}
