using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class SolidEdgeDocumentExtensionsTests
{
    [TestMethod]
    public void GetCreatedVersion_ShouldReturnCorrectVersion()
    {
        // Arrange
        var mockDocument = new Mock<SolidEdgeDocument>();
        mockDocument.SetupGet(d => d.CreatedVersion).Returns("1.2.3.4");

        // Act
        var version = mockDocument.Object.GetCreatedVersion();

        // Assert
        Assert.AreEqual(new Version("1.2.3.4"), version);
    }

    [TestMethod]
    public void GetLastSavedVersion_ShouldReturnCorrectVersion()
    {
        // Arrange
        var mockDocument = new Mock<SolidEdgeDocument>();
        mockDocument.SetupGet(d => d.LastSavedVersion).Returns("5.6.7.8");

        // Act
        var version = mockDocument.Object.GetLastSavedVersion();

        // Assert
        Assert.AreEqual(new Version("5.6.7.8"), version);
    }

    [TestMethod]
    public void GetProperties_ShouldReturnPropertySets()
    {
        // Arrange
        var mockDocument = new Mock<SolidEdgeDocument>();
        var mockProperties = new Mock<PropertySets>();
        mockDocument.SetupGet(d => d.Properties).Returns(mockProperties.Object);

        // Act
        var properties = mockDocument.Object.GetProperties();

        // Assert
        Assert.AreSame(mockProperties.Object, properties);
    }

    [TestMethod]
    public void GetSummaryInfo_ShouldReturnSummaryInfo()
    {
        // Arrange
        var mockDocument = new Mock<SolidEdgeDocument>();
        var mockSummaryInfo = new Mock<SummaryInfo>();
        mockDocument.SetupGet(d => d.SummaryInfo).Returns(mockSummaryInfo.Object);

        // Act
        var summaryInfo = mockDocument.Object.GetSummaryInfo();

        // Assert
        Assert.AreSame(mockSummaryInfo.Object, summaryInfo);
    }

    [TestMethod]
    public void GetVariables_ShouldReturnVariables()
    {
        // Arrange
        var mockDocument = new Mock<SolidEdgeDocument>();
        var mockVariables = new Mock<Variables>();
        mockDocument.SetupGet(d => d.Variables).Returns(mockVariables.Object);

        // Act
        var variables = mockDocument.Object.GetVariables();

        // Assert
        Assert.AreSame(mockVariables.Object, variables);
    }
}