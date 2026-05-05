using Moq;
using SolidEdgeCommunity.Extensions;

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

    [TestMethod]
    public void GetOccurrenceDocument_Generic_ShouldReturnDocument()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        var mockDoc = new Mock<SolidEdgePart.PartDocument>();
        mockOcc.SetupGet(o => o.OccurrenceDocument).Returns(mockDoc.Object);

        // Act
        var result = mockOcc.Object.GetOccurrenceDocument<SolidEdgePart.PartDocument>();

        // Assert
        Assert.AreSame(mockDoc.Object, result);
    }

    [TestMethod]
    public void GetOccurrenceDocument_Generic_ShouldReturnNull_WhenThrowOnErrorFalse()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        mockOcc.SetupGet(o => o.OccurrenceDocument).Throws(new Exception());

        // Act
        var result = mockOcc.Object.GetOccurrenceDocument<SolidEdgePart.PartDocument>(false);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetBodyInversionMatrix_ShouldReturnDoubleArray()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        double[] matrixData = [1, 2, 3];
        Array matrix = matrixData;

        mockOcc.Setup(o => o.GetBodyInversionMatrix(ref It.Ref<Array>.IsAny)).Callback(new GetMatrixCallback((ref Array m) => m = matrix));

        // Act
        var result = mockOcc.Object.GetBodyInversionMatrix();

        // Assert
        CollectionAssert.AreEqual(matrixData, result);
    }

    [TestMethod]
    public void GetExplodeMatrix_ShouldReturnDoubleArray()
    {
        // Arrange
        var mockOcc = new Mock<SolidEdgeAssembly.Occurrence>();
        double[] matrixData = [4, 5, 6];
        Array matrix = matrixData;

        mockOcc.Setup(o => o.GetExplodeMatrix(ref It.Ref<Array>.IsAny)).Callback(new GetMatrixCallback((ref Array m) => m = matrix));

        // Act
        var result = mockOcc.Object.GetExplodeMatrix();

        // Assert
        CollectionAssert.AreEqual(matrixData, result);
    }

    private delegate void GetMatrixCallback(ref Array matrix);
}