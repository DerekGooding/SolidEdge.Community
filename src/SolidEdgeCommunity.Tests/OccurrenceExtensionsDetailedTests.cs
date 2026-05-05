using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class OccurrenceExtensionsDetailedTests
{
    [TestMethod]
    public void GetMatrix_ShouldReturnDoubleArray_WithSpecificValues()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        double[] expectedMatrix = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        Array matrixArray = expectedMatrix;

        mockOcc.Setup(o => o.GetMatrix(ref It.Ref<Array>.IsAny))
            .Callback(new GetMatrixCallback((ref Array m) => m = matrixArray));

        // Act
        var result = mockOcc.Object.GetMatrix();

        // Assert
        CollectionAssert.AreEqual(expectedMatrix, result);
    }

    [TestMethod]
    public void GetOccurrenceDocument_ShouldCastToDocument()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        var mockDoc = new Mock<SolidEdgeDocument>();
        mockOcc.SetupGet(o => o.OccurrenceDocument).Returns(mockDoc.Object);

        // Act
        var result = mockOcc.Object.GetOccurrenceDocument<SolidEdgeDocument>();

        // Assert
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void GetBodyInversionMatrix_ShouldReturnDoubleArray()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        double[] expectedMatrix = [1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0];
        Array matrixArray = expectedMatrix;

        mockOcc.Setup(o => o.GetBodyInversionMatrix(ref It.Ref<Array>.IsAny))
            .Callback(new GetMatrixCallback((ref Array m) => m = matrixArray));

        // Act
        var result = mockOcc.Object.GetBodyInversionMatrix();

        // Assert
        CollectionAssert.AreEqual(expectedMatrix, result);
    }

    delegate void GetMatrixCallback(ref Array matrix);
}
