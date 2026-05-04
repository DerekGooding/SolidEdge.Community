using Moq;
using SolidEdgeCommunity.Extensions;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class EnvironmentExtensionsTests
{
    [TestMethod]
    public void GetCategoryId_ShouldReturnCorrectGuid()
    {
        // Arrange
        var guidStr = Guid.NewGuid().ToString();
        var mockEnv = new Mock<SolidEdgeFramework.Environment>();
        mockEnv.SetupGet(e => e.CATID).Returns(guidStr);

        // Act
        var guid = mockEnv.Object.GetCategoryId();

        // Assert
        Assert.AreEqual(new Guid(guidStr), guid);
    }

    [TestMethod]
    public void LookupByName_ShouldReturnCorrectEnvironment()
    {
        // Arrange
        var mockEnvs = new Mock<Environments>();
        var mockEnv = new Mock<SolidEdgeFramework.Environment>();
        mockEnv.SetupGet(e => e.Name).Returns("TestEnv");
        mockEnvs.SetupGet(e => e.Count).Returns(1);
        mockEnvs.Setup(e => e.Item(1)).Returns(mockEnv.Object);

        // Act
        var result = mockEnvs.Object.LookupByName("TestEnv");

        // Assert
        Assert.AreSame(mockEnv.Object, result);
    }

    [TestMethod]
    public void LookupByName_ShouldReturnNull_WhenNotFound()
    {
        // Arrange
        var mockEnvs = new Mock<Environments>();
        var mockEnv = new Mock<SolidEdgeFramework.Environment>();
        mockEnv.SetupGet(e => e.Name).Returns("OtherEnv");
        mockEnvs.SetupGet(e => e.Count).Returns(1);
        mockEnvs.Setup(e => e.Item(1)).Returns(mockEnv.Object);

        // Act
        var result = mockEnvs.Object.LookupByName("TestEnv");

        // Assert
        Assert.IsNull(result);
    }
}