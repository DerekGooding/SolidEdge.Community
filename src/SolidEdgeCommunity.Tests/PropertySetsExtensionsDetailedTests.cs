using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class PropertySetsExtensionsDetailedTests
{
    [TestMethod]
    public void UpdateCustomProperty_String_ShouldAddIfNotFound()
    {
        // Arrange
        var mockPropertySets = new Mock<SolidEdgeFileProperties.PropertySets>();
        var mockProps = new Mock<SolidEdgeFileProperties.Properties>();
        string val = "Value";
        mockPropertySets.SetupGet(p => p["Custom"]).Returns(mockProps.Object);
        mockProps.SetupGet(p => p["TestProp"]).Throws(new System.Runtime.InteropServices.COMException());

        // Act
        mockPropertySets.Object.UpdateCustomProperty("TestProp", val);

        // Assert
        mockProps.Verify(p => p.Add("TestProp", val), Times.Once);
    }

    [TestMethod]
    public void UpdateCustomProperty_Int_ShouldUpdateValue_WhenExists()
    {
        // Arrange
        var mockPropertySets = new Mock<SolidEdgeFileProperties.PropertySets>();
        var mockProps = new Mock<SolidEdgeFileProperties.Properties>();
        var mockProp = new Mock<SolidEdgeFileProperties.Property>();
        mockPropertySets.SetupGet(p => p["Custom"]).Returns(mockProps.Object);
        mockProps.SetupGet(p => p["TestProp"]).Returns(mockProp.Object);

        // Act
        mockPropertySets.Object.UpdateCustomProperty("TestProp", 123);

        // Assert
        mockProp.VerifySet(p => p.Value = 123);
    }

    [TestMethod]
    public void UpdateCustomProperty_OtherTypes_ShouldWork()
    {
        var mockPropertySets = new Mock<SolidEdgeFileProperties.PropertySets>();
        var mockProps = new Mock<SolidEdgeFileProperties.Properties>();
        var mockProp = new Mock<SolidEdgeFileProperties.Property>();
        mockPropertySets.SetupGet(p => p["Custom"]).Returns(mockProps.Object);
        mockProps.SetupGet(p => p[It.IsAny<string>()]).Returns(mockProp.Object);

        var now = DateTime.Now;
        mockPropertySets.Object.UpdateCustomProperty("bool", true);
        mockPropertySets.Object.UpdateCustomProperty("double", 1.23);
        mockPropertySets.Object.UpdateCustomProperty("DateTime", now);

        mockProp.VerifySet(p => p.Value = true);
        mockProp.VerifySet(p => p.Value = 1.23);
        mockProp.VerifySet(p => p.Value = now);
    }

    [TestMethod]
    public void Close_ShouldWork()
    {
        var mock = new Mock<SolidEdgeFileProperties.PropertySets>();
        
        mock.Object.Close(true);
        mock.Verify(p => p.Save(), Times.Once);
        mock.Verify(p => p.Close(), Times.Once);

        mock.Invocations.Clear();

        mock.Object.Close(false);
        mock.Verify(p => p.Save(), Times.Never);
        mock.Verify(p => p.Close(), Times.Once);
    }
}
