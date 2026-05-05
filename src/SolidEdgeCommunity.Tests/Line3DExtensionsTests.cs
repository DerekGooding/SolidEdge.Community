using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class Line3DExtensionsTests
{
    [TestMethod]
    public void SafeGetKeypointPosition_ShouldReturnArray()
    {
        // Arrange
        var mockLine = new Mock<SolidEdgePart.Line3D>();
        double[] positionData = [4.0, 5.0, 6.0];
        Array positionArray = positionData;

        mockLine.Setup(l => l.GetKeypointPosition(It.IsAny<SolidEdgePart.Sketch3DKeypointType>(), ref It.Ref<Array>.IsAny))
            .Callback(new GetKeypointPositionCallback((SolidEdgePart.Sketch3DKeypointType type, ref Array pos) => pos = positionArray));

        // Act
        var result = mockLine.Object.SafeGetKeypointPosition((SolidEdgePart.Sketch3DKeypointType)0);

        // Assert
        CollectionAssert.AreEqual(positionData, result);
    }

    [TestMethod]
    public void SafeGetKeypointPosition_ShouldReturnEmptyArray_WhenExceptionAndThrowOnErrorFalse()
    {
        // Arrange
        var mockLine = new Mock<SolidEdgePart.Line3D>();
        mockLine.Setup(l => l.GetKeypointPosition(It.IsAny<SolidEdgePart.Sketch3DKeypointType>(), ref It.Ref<Array>.IsAny))
            .Throws(new Exception());

        // Act
        var result = mockLine.Object.SafeGetKeypointPosition((SolidEdgePart.Sketch3DKeypointType)0, false);

        // Assert
        Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void SafeGetKeypointPosition_ShouldThrow_WhenExceptionAndThrowOnErrorTrue()
    {
        // Arrange
        var mockLine = new Mock<SolidEdgePart.Line3D>();
        mockLine.Setup(l => l.GetKeypointPosition(It.IsAny<SolidEdgePart.Sketch3DKeypointType>(), ref It.Ref<Array>.IsAny))
            .Throws(new Exception());

        // Act & Assert
        Assert.Throws<Exception>(() => mockLine.Object.SafeGetKeypointPosition((SolidEdgePart.Sketch3DKeypointType)0, true));
    }

    private delegate void GetKeypointPositionCallback(SolidEdgePart.Sketch3DKeypointType type, ref Array position);
}