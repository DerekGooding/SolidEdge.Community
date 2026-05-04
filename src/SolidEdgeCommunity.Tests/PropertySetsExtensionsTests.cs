using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFileProperties;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class PropertySetsExtensionsTests
{
    [TestMethod]
    public void Close_WithSaveChangesTrue_ShouldSaveAndClose()
    {
        // Arrange
        var mockPropertySets = new Mock<SolidEdgeFileProperties.PropertySets>();

        // Act
        mockPropertySets.Object.Close(true);

        // Assert
        mockPropertySets.Verify(p => p.Save(), Times.Once);
        mockPropertySets.Verify(p => p.Close(), Times.Once);
    }

    [TestMethod]
    public void Close_WithSaveChangesFalse_ShouldOnlyClose()
    {
        // Arrange
        var mockPropertySets = new Mock<SolidEdgeFileProperties.PropertySets>();

        // Act
        mockPropertySets.Object.Close(false);

        // Assert
        mockPropertySets.Verify(p => p.Save(), Times.Never);
        mockPropertySets.Verify(p => p.Close(), Times.Once);
    }

    [TestMethod]
    public void UpdateCustomProperty_String_ShouldAddIfNotFound()
    {
        // Arrange
        var mockPropertySets = new Mock<SolidEdgeFileProperties.PropertySets>();
        var mockProps = new Mock<SolidEdgeFileProperties.Properties>();
        mockPropertySets.SetupGet(p => p["Custom"]).Returns(mockProps.Object);
        mockProps.SetupGet(p => p["TestProp"]).Throws(new System.Runtime.InteropServices.COMException());

        // Act
        mockPropertySets.Object.UpdateCustomProperty("TestProp", "Value");

        // Assert
        mockProps.Verify(p => p.Add("TestProp", "Value"), Times.Once);
    }
}
