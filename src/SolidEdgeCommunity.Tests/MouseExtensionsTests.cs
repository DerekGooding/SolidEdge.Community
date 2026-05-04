using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class MouseExtensionsTests
{
    [TestMethod]
    public void AddToLocateFilter_ShouldCallUnderlyingMethod()
    {
        // Arrange
        var mockMouse = new Mock<Mouse>();

        // Act
        mockMouse.Object.AddToLocateFilter(seLocateFilterConstants.seLocatePart);

        // Assert
        mockMouse.Verify(m => m.AddToLocateFilter((int)seLocateFilterConstants.seLocatePart), Times.Once);
    }

    [TestMethod]
    public void SetLocateMode_ShouldSetProperty()
    {
        // Arrange
        var mockMouse = new Mock<Mouse>();

        // Act
        mockMouse.Object.SetLocateMode(seLocateModes.seLocateQuickPick);

        // Assert
        mockMouse.VerifySet(m => m.LocateMode = (int)seLocateModes.seLocateQuickPick);
    }

    [TestMethod]
    public void GetLocateMode_ShouldReturnCorrectMode()
    {
        // Arrange
        var mockMouse = new Mock<Mouse>();
        mockMouse.SetupGet(m => m.LocateMode).Returns((int)seLocateModes.seLocateQuickPick);

        // Act
        var mode = mockMouse.Object.GetLocateMode(seLocateModes.seLocateQuickPick);

        // Assert
        Assert.AreEqual(seLocateModes.seLocateQuickPick, mode);
    }
}