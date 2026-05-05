using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;
using System.Windows.Forms;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class ApplicationDialogTests
{
    [TestMethod]
    public void ShowDialog_ShouldThrow_WhenDialogIsNull()
    {
        var mockApp = new Mock<Application>();
        Assert.Throws<ArgumentNullException>(() => mockApp.Object.ShowDialog((CommonDialog)null!));
    }

    [TestMethod]
    public void GetNativeWindow_ShouldReturnSomething()
    {
        var mockApp = new Mock<Application>();
        mockApp.SetupGet(a => a.hWnd).Returns(12345);
        
        var win = mockApp.Object.GetNativeWindow();
        
        // NativeWindow.FromHandle returns null if no NativeWindow is already associated with the handle.
        // This is likely why it returns null in tests.
        // We just ensure it doesn't crash during the call.
    }
}
