using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class OccurrenceExtensionsTests
{
    [TestMethod]
    public void GetOccurrenceDocument_ShouldReturnDocument()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        var mockDoc = mockOcc.As<SolidEdgeDocument>();

        // Act
        var result = mockOcc.Object.GetOccurrenceDocument();

        // Assert
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void GetMatrix_ShouldReturnDoubleArray()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        double[] matrixData = [1, 0, 0, 0, 1, 0, 0, 0, 1, 10, 20, 30];
        Array matrix = matrixData;
        
        mockOcc.Setup(o => o.GetMatrix(ref It.Ref<Array>.IsAny)).Callback(new GetMatrixCallback((ref Array m) => m = matrix));

        // Act
        var result = mockOcc.Object.GetMatrix();

        // Assert
        CollectionAssert.AreEqual(matrixData, result);
    }

    delegate void GetMatrixCallback(ref Array matrix);
}
