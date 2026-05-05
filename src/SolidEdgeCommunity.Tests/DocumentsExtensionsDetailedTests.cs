using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class DocumentsExtensionsDetailedTests
{
    [TestMethod]
    public void Open_WithParameters_ShouldCallUnderlyingMethod()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeDocument>();
        
        mockDocs.Setup(d => d.Open("test.par", It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>()))
                .Returns(mockDoc.Object);

        // Act
        // This extension method has 7 parameters total (including 'this' documents)
        // Public static TDocumentType Open<TDocumentType>(this Documents documents, string Filename, object DocRelationAutoServer, object AltPath, object RecognizeFeaturesIfPartTemplate, object RevisionRuleOption, object StopFileOpenIfRevisionRuleNotApplicable)
        var result = mockDocs.Object.Open<SolidEdgeDocument>("test.par", null!, null!, null!, null!, null!);

        // Assert
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void OpenInBackground_WithParameters_ShouldCallUnderlyingMethod()
    {
        // Arrange
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeDocument>();
        const ulong JDOCUMENTPROP_NOWINDOW = 0x00000008;

        mockDocs.Setup(d => d.Open("test.par", JDOCUMENTPROP_NOWINDOW, It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>(), It.IsAny<object>()))
                .Returns(mockDoc.Object);

        // Act
        // This extension method has 6 parameters total (including 'this' documents)
        // Public static TDocumentType OpenInBackground<TDocumentType>(this Documents documents, string Filename, object AltPath, object RecognizeFeaturesIfPartTemplate, object RevisionRuleOption, object StopFileOpenIfRevisionRuleNotApplicable)
        var result = mockDocs.Object.OpenInBackground<SolidEdgeDocument>("test.par", null!, null!, null!, null!);

        // Assert
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void Add_Generic_WithUnsupportedType_ShouldThrow()
    {
        var mockDocs = new Mock<Documents>();
        Assert.Throws<NotSupportedException>(() => mockDocs.Object.Add<object>());
    }

    [TestMethod]
    public void AddAssemblyDocument_WithTemplate_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeAssembly.AssemblyDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_AssemblyDocument, "template")).Returns(mockDoc.Object);

        var result = mockDocs.Object.AddAssemblyDocument("template");
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void AddDraftDocument_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_DraftDocument)).Returns(mockDoc.Object);

        var result = mockDocs.Object.AddDraftDocument();
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void AddDraftDocument_WithTemplate_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeDraft.DraftDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_DraftDocument, "template")).Returns(mockDoc.Object);

        var result = mockDocs.Object.AddDraftDocument("template");
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void AddPartDocument_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgePart.PartDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_PartDocument)).Returns(mockDoc.Object);

        var result = mockDocs.Object.AddPartDocument();
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void AddPartDocument_WithTemplate_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgePart.PartDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_PartDocument, "template")).Returns(mockDoc.Object);

        var result = mockDocs.Object.AddPartDocument("template");
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void AddSheetMetalDocument_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgePart.SheetMetalDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_SheetMetalDocument)).Returns(mockDoc.Object);

        var result = mockDocs.Object.AddSheetMetalDocument();
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void AddSheetMetalDocument_WithTemplate_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgePart.SheetMetalDocument>();
        mockDocs.Setup(d => d.Add(SolidEdgeSDK.PROGID.SolidEdge_SheetMetalDocument, "template")).Returns(mockDoc.Object);

        var result = mockDocs.Object.AddSheetMetalDocument("template");
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void Open_Simple_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeDocument>();
        mockDocs.Setup(d => d.Open("test.par")).Returns(mockDoc.Object);

        var result = mockDocs.Object.Open<SolidEdgeDocument>("test.par");
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void OpenInBackground_Simple_ShouldWork()
    {
        var mockDocs = new Mock<Documents>();
        var mockDoc = new Mock<SolidEdgeDocument>();
        const ulong JDOCUMENTPROP_NOWINDOW = 0x00000008;
        mockDocs.Setup(d => d.Open("test.par", JDOCUMENTPROP_NOWINDOW)).Returns(mockDoc.Object);

        var result = mockDocs.Object.OpenInBackground<SolidEdgeDocument>("test.par");
        Assert.AreSame(mockDoc.Object, result);
    }
}
