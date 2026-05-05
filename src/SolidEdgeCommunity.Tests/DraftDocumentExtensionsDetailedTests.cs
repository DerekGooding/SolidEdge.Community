using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;
using System.Collections;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class DraftDocumentExtensionsDetailedTests
{
    [TestMethod]
    public void EnumerateDrawingObjects_ShouldReturnObjectsFromAllSections()
    {
        // Arrange
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        var mockSections = new Mock<SolidEdgeDraft.Sections>();
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var obj = new object();

        mockDoc.Setup(d => d.Sections).Returns(mockSections.Object);
        mockSections.SetupGet(s => s.Count).Returns(1);
        mockSections.Setup(s => s.Item(1)).Returns(mockSection.Object);
        
        // Mock SectionExtensions.EnumerateDrawingObjects
        // This is tricky because it's an extension method. 
        // In reality, Section.Sheets is what gets enumerated.
        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockDrawingObjects = new Mock<SolidEdgeFrameworkSupport.DrawingObjects>();
        
        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);
        mockSheet.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);
        mockSheet.SetupGet(s => s.DrawingObjects).Returns(mockDrawingObjects.Object);
        mockDrawingObjects.SetupGet(d => d.Count).Returns(1);
        mockDrawingObjects.Setup(d => d.Item(1)).Returns(obj);

        // Act
        var result = mockDoc.Object.EnumerateDrawingObjects().ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(obj, result[0]);
    }

    [TestMethod]
    public void EnumerateDrawingObjects_WithTypeFilter_ShouldReturnFilteredObjects()
    {
        // Arrange
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        var mockSections = new Mock<SolidEdgeDraft.Sections>();
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var mockLine = new Mock<SolidEdgeFrameworkSupport.Line2d>();

        mockDoc.Setup(d => d.Sections).Returns(mockSections.Object);
        mockSections.SetupGet(s => s.Count).Returns(1);
        mockSections.Setup(s => s.Item(1)).Returns(mockSection.Object);
        
        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockDrawingObjects = new Mock<SolidEdgeFrameworkSupport.DrawingObjects>();
        
        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);
        mockSheet.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);
        mockSheet.SetupGet(s => s.DrawingObjects).Returns(mockDrawingObjects.Object);
        mockDrawingObjects.SetupGet(d => d.Count).Returns(1);
        mockDrawingObjects.Setup(d => d.Item(1)).Returns(mockLine.Object);

        // Act
        var result = mockDoc.Object.EnumerateDrawingObjects<SolidEdgeFrameworkSupport.Line2d>().ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(mockLine.Object, result[0]);
    }

    [TestMethod]
    public void EnumerateDrawingObjects_WithSectionType_ShouldReturnFilteredObjects()
    {
        // Arrange
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        var mockSections = new Mock<SolidEdgeDraft.Sections>();
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var obj = new object();

        mockDoc.Setup(d => d.Sections).Returns(mockSections.Object);
        mockSections.SetupGet(s => s.Count).Returns(1);
        mockSections.Setup(s => s.Item(1)).Returns(mockSection.Object);
        mockSection.SetupGet(s => s.Type).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);

        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockDrawingObjects = new Mock<SolidEdgeFrameworkSupport.DrawingObjects>();

        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);
        mockSheet.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);
        mockSheet.SetupGet(s => s.DrawingObjects).Returns(mockDrawingObjects.Object);
        mockDrawingObjects.SetupGet(d => d.Count).Returns(1);
        mockDrawingObjects.Setup(d => d.Item(1)).Returns(obj);

        // Act
        var result = mockDoc.Object.EnumerateDrawingObjects(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection).ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(obj, result[0]);
    }

    [TestMethod]
    public void EnumerateDrawingObjects_Generic_WithSectionType_ShouldReturnFilteredObjects()
    {
        // Arrange
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        var mockSections = new Mock<SolidEdgeDraft.Sections>();
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var mockLine = new Mock<SolidEdgeFrameworkSupport.Line2d>();

        mockDoc.Setup(d => d.Sections).Returns(mockSections.Object);
        mockSections.SetupGet(s => s.Count).Returns(1);
        mockSections.Setup(s => s.Item(1)).Returns(mockSection.Object);
        mockSection.SetupGet(s => s.Type).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);

        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockDrawingObjects = new Mock<SolidEdgeFrameworkSupport.DrawingObjects>();

        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);
        mockSheet.SetupGet(s => s.SectionType).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);
        mockSheet.SetupGet(s => s.DrawingObjects).Returns(mockDrawingObjects.Object);
        mockDrawingObjects.SetupGet(d => d.Count).Returns(1);
        mockDrawingObjects.Setup(d => d.Item(1)).Returns(mockLine.Object);

        // Act
        var result = mockDoc.Object.EnumerateDrawingObjects<SolidEdgeFrameworkSupport.Line2d>(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection).ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(mockLine.Object, result[0]);
    }

    [TestMethod]
    public void EnumerateDrawingViews_WithSectionType_ShouldReturnFilteredViews()
    {
        // Arrange
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        var mockSections = new Mock<SolidEdgeDraft.Sections>();
        var mockSection = new Mock<SolidEdgeDraft.Section>();
        var mockView = new Mock<SolidEdgeDraft.DrawingView>();

        mockDoc.Setup(d => d.Sections).Returns(mockSections.Object);
        mockSections.SetupGet(s => s.Count).Returns(1);
        mockSections.Setup(s => s.Item(1)).Returns(mockSection.Object);
        mockSection.SetupGet(s => s.Type).Returns(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection);

        var mockSheets = new Mock<SolidEdgeDraft.SectionSheets>();
        var mockSheet = new Mock<SolidEdgeDraft.Sheet>();
        var mockViews = new Mock<SolidEdgeDraft.DrawingViews>();

        mockSection.Setup(s => s.Sheets).Returns(mockSheets.Object);
        mockSheets.SetupGet(s => s.Count).Returns(1);
        mockSheets.Setup(s => s.Item(1)).Returns(mockSheet.Object);
        mockSheet.SetupGet(s => s.DrawingViews).Returns(mockViews.Object);
        mockViews.SetupGet(v => v.Count).Returns(1);
        mockViews.Setup(v => v.Item(1)).Returns(mockView.Object);

        // Act
        var result = mockDoc.Object.EnumerateDrawingViews(SolidEdgeDraft.SheetSectionTypeConstants.igWorkingSection).ToList();

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreSame(mockView.Object, result[0]);
    }

    [TestMethod]
    public void DraftDocument_Properties_ShouldWork()
    {
        var mock = new Mock<SolidEdgeDraft.DraftDocument>();
        var mockProps = new Mock<PropertySets>();
        var mockSummary = new Mock<SummaryInfo>();
        var mockVars = new Mock<Variables>();

        mock.SetupGet(d => d.Properties).Returns(mockProps.Object);
        mock.SetupGet(d => d.SummaryInfo).Returns(mockSummary.Object);
        mock.SetupGet(d => d.Variables).Returns(mockVars.Object);
        mock.SetupGet(d => d.LastSavedVersion).Returns("1.0.0.0");

        Assert.AreSame(mockProps.Object, mock.Object.GetProperties());
        Assert.AreSame(mockSummary.Object, mock.Object.GetSummaryInfo());
        Assert.AreSame(mockVars.Object, mock.Object.GetVariables());
        Assert.AreEqual(new Version("1.0.0.0"), mock.Object.GetLastSavedVersion());
    }
}
