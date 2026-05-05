using Moq;
using SolidEdgeCommunity.Extensions;
using System.Collections;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class SectionExtensionsTests
{
    [TestMethod]
    public void EnumerateDrawingViews_ShouldReturnViewsFromAllSheets()
    {
        // Arrange
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockViews = new Mock<SolidEdgeDraft.DrawingViews>();
        var mockView = new Mock<SolidEdgeDraft.DrawingView>();

        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);

        mockSheet.Setup(s => s.DrawingViews).Returns(mockViews.Object);
        mockViews.SetupGet(v => v.Count).Returns(1);
        mockViews.Setup(v => v.Item(1)).Returns(mockView.Object);

        // Act
        var result = mockSection.Object.EnumerateDrawingViews().ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(mockView.Object, result[0]);
    }

    [TestMethod]
    public void EnumerateDrawingObjects_ShouldFilterHiddenSections()
    {
        // Arrange
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet1 = new Mock<SolidEdgeDraft.Sheet>();
        var mockSheet2 = new Mock<SolidEdgeDraft.Sheet>();
        var mockSheet3 = new Mock<SolidEdgeDraft.Sheet>();
        var mockSheet4 = new Mock<SolidEdgeDraft.Sheet>();

        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(4);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet1.Object);
        mockSheets.Setup(s => s.Item(2)).Returns(mockSheet2.Object);
        mockSheets.Setup(s => s.Item(3)).Returns(mockSheet3.Object);
        mockSheets.Setup(s => s.Item(4)).Returns(mockSheet4.Object);

        mockSheet1.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igDrawingViewSection);
        mockSheet2.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igBlockViewSection);
        mockSheet3.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);
        mockSheet4.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igBackgroundSection);

        var mockDrawingObjects = new Mock<SolidEdgeFrameworkSupport.DrawingObjects>();
        mockSheet3.SetupGet(s => s.DrawingObjects).Returns(mockDrawingObjects.Object);
        mockSheet4.SetupGet(s => s.DrawingObjects).Returns(mockDrawingObjects.Object);
        mockDrawingObjects.SetupGet(d => d.Count).Returns(0);

        // Act
        var result = mockSection.Object.EnumerateDrawingObjects().ToList();

        // Assert
        mockSheet1.VerifyGet(s => s.DrawingObjects, Times.Never);
        mockSheet2.VerifyGet(s => s.DrawingObjects, Times.Never);
        mockSheet3.VerifyGet(s => s.DrawingObjects, Times.AtLeastOnce);
        mockSheet4.VerifyGet(s => s.DrawingObjects, Times.AtLeastOnce);
    }

    [TestMethod]
    public void EnumerateDrawingObjects_Generic_ShouldReturnCorrectType()
    {
        // Arrange
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockDrawingObjects = new Mock<SolidEdgeFrameworkSupport.DrawingObjects>();
        var mockLine = new Mock<SolidEdgeFrameworkSupport.Line2d>();
        var mockCircle = new Mock<SolidEdgeFrameworkSupport.Circle2d>();

        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);

        mockSheet.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);
        mockSheet.SetupGet(s => s.DrawingObjects).Returns(mockDrawingObjects.Object);

        mockDrawingObjects.SetupGet(d => d.Count).Returns(2);
        mockDrawingObjects.Setup(d => d.Item(1)).Returns(mockLine.Object);
        mockDrawingObjects.Setup(d => d.Item(2)).Returns(mockCircle.Object);

        // Act
        var result = mockSection.Object.EnumerateDrawingObjects<SolidEdgeFrameworkSupport.Line2d>().ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(mockLine.Object, result[0]);
    }

    [TestMethod]
    public void EnumerateDrawingObjects_ShouldWork()
    {
        // Arrange
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockDrawingObjects = new Mock<SolidEdgeFrameworkSupport.DrawingObjects>();
        var obj = new object();

        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);

        mockSheet.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);
        mockSheet.SetupGet(s => s.DrawingObjects).Returns(mockDrawingObjects.Object);

        mockDrawingObjects.SetupGet(d => d.Count).Returns(1);
        mockDrawingObjects.Setup(d => d.Item(1)).Returns(obj);

        // Act
        var result = mockSection.Object.EnumerateDrawingObjects().ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(obj, result[0]);
    }
}