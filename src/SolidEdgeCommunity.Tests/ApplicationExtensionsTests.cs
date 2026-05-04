using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;
using System.Diagnostics;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class ApplicationExtensionsTests
{
    [TestMethod]
    public void GetActiveDocument_ShouldReturnDocument_WhenExists()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        var mockDoc = new Mock<SolidEdgeDocument>();
        mockApp.SetupGet(a => a.ActiveDocument).Returns(mockDoc.Object);

        // Act
        var doc = mockApp.Object.GetActiveDocument();

        // Assert
        Assert.AreSame(mockDoc.Object, doc);
    }

    [TestMethod]
    public void GetActiveDocument_ShouldThrow_WhenNotExistsAndThrowOnErrorTrue()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        mockApp.SetupGet(a => a.ActiveDocument).Throws(new System.Runtime.InteropServices.COMException());

        // Act & Assert
        Assert.Throws<System.Runtime.InteropServices.COMException>(() => mockApp.Object.GetActiveDocument(true));
    }

    [TestMethod]
    public void GetActiveDocument_ShouldReturnNull_WhenNotExistsAndThrowOnErrorFalse()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        mockApp.SetupGet(a => a.ActiveDocument).Throws(new System.Runtime.InteropServices.COMException());

        // Act
        var doc = mockApp.Object.GetActiveDocument(false);

        // Assert
        Assert.IsNull(doc);
    }

    [TestMethod]
    public void GetActiveEnvironment_ShouldReturnCorrectEnvironment()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        var mockEnvironments = new Mock<Environments>();
        var mockEnv = new Mock<SolidEdgeFramework.Environment>();
        
        mockApp.SetupGet(a => a.Environments).Returns(mockEnvironments.Object);
        mockApp.SetupGet(a => a.ActiveEnvironment).Returns("Part");
        mockEnvironments.Setup(e => e.Item("Part")).Returns(mockEnv.Object);

        // Act
        var env = mockApp.Object.GetActiveEnvironment();

        // Assert
        Assert.AreSame(mockEnv.Object, env);
    }

    [TestMethod]
    public void GetVersion_ShouldReturnCorrectVersion()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        mockApp.SetupGet(a => a.Version).Returns("222.0.0.0");

        // Act
        var version = mockApp.Object.GetVersion();

        // Assert
        Assert.AreEqual(new Version("222.0.0.0"), version);
    }

    [TestMethod]
    public void GetProcess_ShouldReturnCurrentProcess()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        int currentPid = Process.GetCurrentProcess().Id;
        mockApp.SetupGet(a => a.ProcessID).Returns(currentPid);

        // Act
        var process = mockApp.Object.GetProcess();

        // Assert
        Assert.AreEqual(currentPid, process.Id);
    }

    [TestMethod]
    public void GetWindowHandle_ShouldReturnCorrectHandle()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        mockApp.SetupGet(a => a.hWnd).Returns(12345);

        // Act
        var handle = mockApp.Object.GetWindowHandle();

        // Assert
        Assert.AreEqual(new IntPtr(12345), handle);
    }
}
