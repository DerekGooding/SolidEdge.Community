using Moq;
using SolidEdgeCommunity.Extensions;
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
    public void GetActiveDocument_Generic_ShouldReturnDocument_WhenExists()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        var mockDoc = new Mock<SolidEdgeAssembly.AssemblyDocument>();
        mockApp.SetupGet(a => a.ActiveDocument).Returns(mockDoc.Object);

        // Act
        var doc = mockApp.Object.GetActiveDocument<SolidEdgeAssembly.AssemblyDocument>();

        // Assert
        Assert.AreSame(mockDoc.Object, doc);
    }

    [TestMethod]
    public void GetActiveDocument_Generic_ShouldReturnNull_WhenNotExistsAndThrowOnErrorFalse()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        mockApp.SetupGet(a => a.ActiveDocument).Throws(new System.Runtime.InteropServices.COMException());

        // Act
        var doc = mockApp.Object.GetActiveDocument<SolidEdgeAssembly.AssemblyDocument>(false);

        // Assert
        Assert.IsNull(doc);
    }

    [TestMethod]
    public void GetEnvironment_ShouldReturnCorrectEnvironment()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var mockApp = new Mock<Application>();
        var mockEnvs = new Mock<Environments>();
        var mockEnv = new Mock<SolidEdgeFramework.Environment>();
        
        mockApp.SetupGet(a => a.Environments).Returns(mockEnvs.Object);
        mockEnvs.SetupGet(e => e.Count).Returns(1);
        mockEnvs.Setup(e => e.Item(1)).Returns(mockEnv.Object);
        mockEnv.SetupGet(e => e.CATID).Returns(guid.ToString());

        // Act
        var result = mockApp.Object.GetEnvironment(guid.ToString());

        // Assert
        Assert.AreSame(mockEnv.Object, result);
    }

    [TestMethod]
    public void GetEnvironment_ShouldReturnNull_WhenNotFound()
    {
        // Arrange
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var mockApp = new Mock<Application>();
        var mockEnvs = new Mock<Environments>();
        var mockEnv = new Mock<SolidEdgeFramework.Environment>();
        
        mockApp.SetupGet(a => a.Environments).Returns(mockEnvs.Object);
        mockEnvs.SetupGet(e => e.Count).Returns(1);
        mockEnvs.Setup(e => e.Item(1)).Returns(mockEnv.Object);
        mockEnv.SetupGet(e => e.CATID).Returns(guid1.ToString());

        // Act
        var result = mockApp.Object.GetEnvironment(guid2.ToString());

        // Assert
        Assert.IsNull(result);
    }

    delegate void GetGlobalParameterDelegate(SolidEdgeFramework.ApplicationGlobalConstants globalConstant, ref object? value);

    [TestMethod]
    public void GetGlobalParameter_ShouldReturnCorrectValue()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        object? expectedValue = 123;
        mockApp.Setup(a => a.GetGlobalParameter(It.IsAny<SolidEdgeFramework.ApplicationGlobalConstants>(), ref It.Ref<object?>.IsAny))
               .Callback(new GetGlobalParameterDelegate((SolidEdgeFramework.ApplicationGlobalConstants c, ref object? v) => v = expectedValue));

        // Act
        var value = mockApp.Object.GetGlobalParameter((SolidEdgeFramework.ApplicationGlobalConstants)100);

        // Assert
        Assert.AreEqual(expectedValue, value);
    }

    [TestMethod]
    public void GetGlobalParameter_Generic_ShouldReturnCorrectValue()
    {
        // Arrange
        var mockApp = new Mock<Application>();
        object? expectedValue = "Test";
        mockApp.Setup(a => a.GetGlobalParameter(It.IsAny<SolidEdgeFramework.ApplicationGlobalConstants>(), ref It.Ref<object?>.IsAny))
               .Callback(new GetGlobalParameterDelegate((SolidEdgeFramework.ApplicationGlobalConstants c, ref object? v) => v = expectedValue));

        // Act
        var value = mockApp.Object.GetGlobalParameter<string>((SolidEdgeFramework.ApplicationGlobalConstants)100);

        // Assert
        Assert.AreEqual("Test", value);
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