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

        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.As<IEnumerable>().Setup(s => s.GetEnumerator()).Returns(new[] { mockSheet1.Object, mockSheet2.Object }.GetEnumerator());

        mockSheet1.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igDrawingViewSection);
        mockSheet2.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);

        mockSheet2.SetupGet(s => s.DrawingObjects).Returns((SolidEdgeFrameworkSupport.DrawingObjects)null!);

        // Act
        try
        {
            var result = mockSection.Object.EnumerateDrawingObjects().ToList();
        }
        catch
        {
            // Expected failure
        }

        // Assert
        mockSheet1.VerifyGet(s => s.DrawingObjects, Times.Never);
    }
}