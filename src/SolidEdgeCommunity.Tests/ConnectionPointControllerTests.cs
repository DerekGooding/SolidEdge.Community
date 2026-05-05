using Moq;
using System.Runtime.InteropServices.ComTypes;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class ConnectionPointControllerTests
{
    [TestMethod]
    public void AdviseSink_ShouldNotAdviseTwice()
    {
        // Arrange
        var sink = new object();
        var controller = new ConnectionPointController(sink);
        var mockContainer = new Mock<IConnectionPointContainer>();
        var mockCP = new Mock<IConnectionPoint>();
        int cookie = 123;

        mockContainer.Setup(c => c.FindConnectionPoint(ref It.Ref<Guid>.IsAny, out It.Ref<IConnectionPoint>.IsAny))
            .Callback(new FindCPCallback((ref Guid g, out IConnectionPoint cp) => cp = mockCP.Object));

        mockCP.Setup(cp => cp.Advise(sink, out cookie));

        // Act
        controller.AdviseSink<IConnectionPoint>(mockContainer.Object);
        controller.AdviseSink<IConnectionPoint>(mockContainer.Object);

        // Assert
        mockCP.Verify(cp => cp.Advise(sink, out It.Ref<int>.IsAny), Times.Once);
    }

    [TestMethod]
    public void IsSinkAdvised_ShouldReturnCorrectValue()
    {
        // Arrange
        var sink = new object();
        var controller = new ConnectionPointController(sink);
        var mockContainer = new Mock<IConnectionPointContainer>();
        var mockCP = new Mock<IConnectionPoint>();
        int cookie = 123;

        mockContainer.Setup(c => c.FindConnectionPoint(ref It.Ref<Guid>.IsAny, out It.Ref<IConnectionPoint>.IsAny))
            .Callback(new FindCPCallback((ref Guid g, out IConnectionPoint cp) => cp = mockCP.Object));

        mockCP.Setup(cp => cp.Advise(sink, out cookie));

        // Act & Assert
        Assert.IsFalse(controller.IsSinkAdvised<IConnectionPoint>(mockContainer.Object));
        controller.AdviseSink<IConnectionPoint>(mockContainer.Object);
        Assert.IsTrue(controller.IsSinkAdvised<IConnectionPoint>(mockContainer.Object));
    }

    [TestMethod]
    public void UnadviseSink_ShouldWork()
    {
        // Arrange
        var sink = new object();
        var controller = new ConnectionPointController(sink);
        var mockContainer = new Mock<IConnectionPointContainer>();
        var mockCP = new Mock<IConnectionPoint>();
        int cookie = 123;

        mockContainer.Setup(c => c.FindConnectionPoint(ref It.Ref<Guid>.IsAny, out It.Ref<IConnectionPoint>.IsAny))
            .Callback(new FindCPCallback((ref Guid g, out IConnectionPoint cp) => cp = mockCP.Object));

        mockCP.Setup(cp => cp.Advise(sink, out cookie));
        controller.AdviseSink<IConnectionPoint>(mockContainer.Object);

        // Act
        controller.UnadviseSink<IConnectionPoint>(mockContainer.Object);

        // Assert
        mockCP.Verify(cp => cp.Unadvise(cookie), Times.Once);
        Assert.IsFalse(controller.IsSinkAdvised<IConnectionPoint>(mockContainer.Object));
    }

    [TestMethod]
    public void UnadviseAllSinks_ShouldWork()
    {
        // Arrange
        var sink = new object();
        var controller = new ConnectionPointController(sink);
        var mockContainer = new Mock<IConnectionPointContainer>();
        var mockCP = new Mock<IConnectionPoint>();
        int cookie = 123;

        mockContainer.Setup(c => c.FindConnectionPoint(ref It.Ref<Guid>.IsAny, out It.Ref<IConnectionPoint>.IsAny))
            .Callback(new FindCPCallback((ref Guid g, out IConnectionPoint cp) => cp = mockCP.Object));

        mockCP.Setup(cp => cp.Advise(sink, out cookie));
        controller.AdviseSink<IConnectionPoint>(mockContainer.Object);

        // Act
        controller.UnadviseAllSinks();

        // Assert
        mockCP.Verify(cp => cp.Unadvise(cookie), Times.Once);
        Assert.IsFalse(controller.IsSinkAdvised<IConnectionPoint>(mockContainer.Object));
    }

    private delegate void FindCPCallback(ref Guid guid, out IConnectionPoint cp);
}