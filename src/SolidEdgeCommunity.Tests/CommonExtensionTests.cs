using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class CommonExtensionTests
{
    [TestMethod]
    public void AssemblyDocument_Extensions_ShouldWork()
    {
        var mock = new Mock<SolidEdgeAssembly.AssemblyDocument>();
        var mockProps = new Mock<PropertySets>();
        var mockSummary = new Mock<SummaryInfo>();
        var mockVars = new Mock<Variables>();

        mock.SetupGet(d => d.CreatedVersion).Returns("1.0.0.0");
        mock.SetupGet(d => d.LastSavedVersion).Returns("2.0.0.0");
        mock.SetupGet(d => d.Properties).Returns(mockProps.Object);
        mock.SetupGet(d => d.SummaryInfo).Returns(mockSummary.Object);
        mock.SetupGet(d => d.Variables).Returns(mockVars.Object);

        Assert.AreEqual(new Version("1.0.0.0"), mock.Object.GetCreatedVersion());
        Assert.AreEqual(new Version("2.0.0.0"), mock.Object.GetLastSavedVersion());
        Assert.AreSame(mockProps.Object, mock.Object.GetProperties());
        Assert.AreSame(mockSummary.Object, mock.Object.GetSummaryInfo());
        Assert.AreSame(mockVars.Object, mock.Object.GetVariables());
    }

    [TestMethod]
    public void PartDocument_Extensions_ShouldWork()
    {
        var mock = new Mock<SolidEdgePart.PartDocument>();
        var mockProps = new Mock<PropertySets>();
        var mockSummary = new Mock<SummaryInfo>();
        var mockVars = new Mock<Variables>();

        mock.SetupGet(d => d.CreatedVersion).Returns("3.0.0.0");
        mock.SetupGet(d => d.LastSavedVersion).Returns("4.0.0.0");
        mock.SetupGet(d => d.Properties).Returns(mockProps.Object);
        mock.SetupGet(d => d.SummaryInfo).Returns(mockSummary.Object);
        mock.SetupGet(d => d.Variables).Returns(mockVars.Object);

        Assert.AreEqual(new Version("3.0.0.0"), mock.Object.GetCreatedVersion());
        Assert.AreEqual(new Version("4.0.0.0"), mock.Object.GetLastSavedVersion());
        Assert.AreSame(mockProps.Object, mock.Object.GetProperties());
        Assert.AreSame(mockSummary.Object, mock.Object.GetSummaryInfo());
        Assert.AreSame(mockVars.Object, mock.Object.GetVariables());
    }

    [TestMethod]
    public void SheetMetalDocument_Extensions_ShouldWork()
    {
        var mock = new Mock<SolidEdgePart.SheetMetalDocument>();
        var mockProps = new Mock<PropertySets>();
        var mockSummary = new Mock<SummaryInfo>();
        var mockVars = new Mock<Variables>();

        mock.SetupGet(d => d.CreatedVersion).Returns("5.0.0.0");
        mock.SetupGet(d => d.LastSavedVersion).Returns("6.0.0.0");
        mock.SetupGet(d => d.Properties).Returns(mockProps.Object);
        mock.SetupGet(d => d.SummaryInfo).Returns(mockSummary.Object);
        mock.SetupGet(d => d.Variables).Returns(mockVars.Object);

        Assert.AreEqual(new Version("5.0.0.0"), mock.Object.GetCreatedVersion());
        Assert.AreEqual(new Version("6.0.0.0"), mock.Object.GetLastSavedVersion());
        Assert.AreSame(mockProps.Object, mock.Object.GetProperties());
        Assert.AreSame(mockSummary.Object, mock.Object.GetSummaryInfo());
        Assert.AreSame(mockVars.Object, mock.Object.GetVariables());
    }

    [TestMethod]
    public void WeldmentDocument_Extensions_ShouldWork()
    {
        var mock = new Mock<SolidEdgePart.WeldmentDocument>();
        var mockProps = new Mock<PropertySets>();
        var mockSummary = new Mock<SummaryInfo>();
        var mockVars = new Mock<Variables>();

        mock.SetupGet(d => d.CreatedVersion).Returns("7.0.0.0");
        mock.SetupGet(d => d.LastSavedVersion).Returns("8.0.0.0");
        mock.SetupGet(d => d.Properties).Returns(mockProps.Object);
        mock.SetupGet(d => d.SummaryInfo).Returns(mockSummary.Object);
        mock.SetupGet(d => d.Variables).Returns(mockVars.Object);

        Assert.AreEqual(new Version("7.0.0.0"), mock.Object.GetCreatedVersion());
        Assert.AreEqual(new Version("8.0.0.0"), mock.Object.GetLastSavedVersion());
        Assert.AreSame(mockProps.Object, mock.Object.GetProperties());
        Assert.AreSame(mockSummary.Object, mock.Object.GetSummaryInfo());
        Assert.AreSame(mockVars.Object, mock.Object.GetVariables());
    }

    [TestMethod]
    public void Window_Extensions_ShouldReturnHandles()
    {
        var mock = new Mock<Window>();
        mock.SetupGet(w => w.DrawHwnd).Returns(123);
        mock.SetupGet(w => w.hWnd).Returns(456);

        Assert.AreEqual(new IntPtr(123), mock.Object.GetDrawHandle());
        Assert.AreEqual(new IntPtr(456), mock.Object.GetHandle());
    }

    [TestMethod]
    public void RefPlanes_Extensions_ShouldReturnCorrectItems()
    {
        var mock = new Mock<SolidEdgePart.RefPlanes>();
        var mockPlane1 = new Mock<SolidEdgePart.RefPlane>();
        var mockPlane2 = new Mock<SolidEdgePart.RefPlane>();
        var mockPlane3 = new Mock<SolidEdgePart.RefPlane>();

        mock.Setup(m => m.Item(1)).Returns(mockPlane1.Object);
        mock.Setup(m => m.Item(2)).Returns(mockPlane2.Object);
        mock.Setup(m => m.Item(3)).Returns(mockPlane3.Object);

        Assert.AreSame(mockPlane1.Object, mock.Object.GetTopPlane());
        Assert.AreSame(mockPlane2.Object, mock.Object.GetRightPlane());
        Assert.AreSame(mockPlane3.Object, mock.Object.GetFrontPlane());
    }
}