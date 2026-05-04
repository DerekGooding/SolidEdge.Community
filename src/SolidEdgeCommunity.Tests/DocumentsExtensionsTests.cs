using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class DocumentsExtensionsTests
{
    [TestMethod]
    public void Add_Generic_AssemblyDocument_ShouldReturnAssemblyDocument()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();
        var mockAssy = new Mock<SolidEdgeAssembly.AssemblyDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_AssemblyDocument)).Returns(mockAssy.Object);

        // Act
        var result = mockDocs.Object.Add<SolidEdgeAssembly.AssemblyDocument>();

        // Assert
        Assert.AreSame(mockAssy.Object, result);
    }

    [TestMethod]
    public void Add_Generic_DraftDocument_ShouldReturnDraftDocument()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();
        var mockDraft = new Mock<SolidEdgeDraft.DraftDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_DraftDocument)).Returns(mockDraft.Object);

        // Act
        var result = mockDocs.Object.Add<SolidEdgeDraft.DraftDocument>();

        // Assert
        Assert.AreSame(mockDraft.Object, result);
    }

    [TestMethod]
    public void Add_Generic_PartDocument_ShouldReturnPartDocument()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();
        var mockPart = new Mock<SolidEdgePart.PartDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_PartDocument)).Returns(mockPart.Object);

        // Act
        var result = mockDocs.Object.Add<SolidEdgePart.PartDocument>();

        // Assert
        Assert.AreSame(mockPart.Object, result);
    }

    [TestMethod]
    public void Add_Generic_SheetMetalDocument_ShouldReturnSheetMetalDocument()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();
        var mockSheetMetal = new Mock<SolidEdgePart.SheetMetalDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_SheetMetalDocument)).Returns(mockSheetMetal.Object);

        // Act
        var result = mockDocs.Object.Add<SolidEdgePart.SheetMetalDocument>();

        // Assert
        Assert.AreSame(mockSheetMetal.Object, result);
    }

    [TestMethod]
    public void Add_Generic_UnsupportedType_ShouldThrow()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => mockDocs.Object.Add<string>());
    }

    [TestMethod]
    public void Open_Generic_ShouldReturnDocument()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeDocument>();
        mockDocs.Setup(d => d.Open("test.par", It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>())).Returns(mockDoc.Object);

        // Act
        var result = mockDocs.Object.Open<SolidEdgeDocument>("test.par");

        // Assert
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void OpenInBackground_Generic_ShouldReturnDocument()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeDocument>();
        const ulong JDOCUMENTPROP_NOWINDOW = 0x00000008;
        mockDocs.Setup(d => d.Open("test.par", JDOCUMENTPROP_NOWINDOW, It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>())).Returns(mockDoc.Object);

        // Act
        var result = mockDocs.Object.OpenInBackground<SolidEdgeDocument>("test.par");

        // Assert
        Assert.AreSame(mockDoc.Object, result);
    }
}