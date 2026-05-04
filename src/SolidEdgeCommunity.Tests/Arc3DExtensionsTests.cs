using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class Arc3DExtensionsTests
{
    [TestMethod]
    public void SafeGetKeypointPosition_ShouldReturnArray()
    {
        // Arrange
        var mockArc = new Mock<SolidEdgePart.Arc3D>();
        double[] positionData = [1.0, 2.0, 3.0];
        Array positionArray = positionData;

        mockArc.Setup(a => a.GetKeypointPosition(It.IsAny<SolidEdgePart.Sketch3DKeypointType>(), ref It.Ref<Array>.IsAny))
            .Callback(new GetKeypointPositionCallback((SolidEdgePart.Sketch3DKeypointType type, ref Array pos) => pos = positionArray));

        // Act
        var result = mockArc.Object.SafeGetKeypointPosition((SolidEdgePart.Sketch3DKeypointType)0);

        // Assert
        CollectionAssert.AreEqual(positionData, result);
        }

        [TestMethod]
        public void SafeGetKeypointPosition_ShouldReturnEmpty_OnException_WhenThrowOnErrorFalse()
        {
        // Arrange
        var mockArc = new Mock<SolidEdgePart.Arc3D>();
        mockArc.Setup(a => a.GetKeypointPosition(It.IsAny<SolidEdgePart.Sketch3DKeypointType>(), ref It.Ref<Array>.IsAny))
            .Throws(new System.Runtime.InteropServices.COMException());

        // Act
        var result = mockArc.Object.SafeGetKeypointPosition((SolidEdgePart.Sketch3DKeypointType)0, false);

        // Assert
        Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void SafeGetKeypointPosition_ShouldThrow_OnException_WhenThrowOnErrorTrue()
        {
        // Arrange
        var mockArc = new Mock<SolidEdgePart.Arc3D>();
        mockArc.Setup(a => a.GetKeypointPosition(It.IsAny<SolidEdgePart.Sketch3DKeypointType>(), ref It.Ref<Array>.IsAny))
            .Throws(new System.Runtime.InteropServices.COMException());

        // Act & Assert
        Assert.Throws<System.Runtime.InteropServices.COMException>(() => mockArc.Object.SafeGetKeypointPosition((SolidEdgePart.Sketch3DKeypointType)0, true));
        }


    delegate void GetKeypointPositionCallback(SolidEdgePart.Sketch3DKeypointType type, ref Array position);
}
