using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;
using System.Collections;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class DraftDocumentExtensionsTests
{
    [TestMethod]
    public void EnumerateDrawingViews_ShouldReturnAllViews()
    {
        // Arrange
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        var mockSheets = new Mock<SolidEdgeDraft.Sheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockViews = new Mock<SolidEdgeDraft.DrawingViews>();
        var mockView = new Mock<SolidEdgeDraft.DrawingView>();

        mockDoc.Setup(d => d.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);
        
        mockSheet.Setup(s => s.DrawingViews).Returns(mockViews.Object);
        mockViews.SetupGet(v => v.Count).Returns(1);
        mockViews.Setup(v => v.Item(1)).Returns(mockView.Object);

        // Act
        var result = mockDoc.Object.EnumerateDrawingViews().ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(mockView.Object, result[0]);
    }

    [TestMethod]
    public void GetCreatedVersion_ShouldReturnCorrectVersion()
    {
        // Arrange
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        mockDoc.SetupGet(d => d.CreatedVersion).Returns("1.2.3.4");

        // Act
        var version = mockDoc.Object.GetCreatedVersion();

        // Assert
        Assert.AreEqual(new Version("1.2.3.4"), version);
    }

    [TestMethod]
    public void GetProperties_ShouldReturnPropertySets()
    {
        // Arrange
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        var mockProps = new Mock<PropertySets>();
        mockDoc.SetupGet(d => d.Properties).Returns(mockProps.Object);

        // Act
        var props = mockDoc.Object.GetProperties();

        // Assert
        Assert.AreSame(mockProps.Object, props);
    }
}
