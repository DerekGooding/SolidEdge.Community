using Moq;
using SolidEdgeCommunity;
using System.Runtime.InteropServices.ComTypes;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class EventSinkTests
{
    private class TestEventSink : EventSink<IConnectionPoint>
    {
        public TestEventSink() : base() { }
        public TestEventSink(object source) : base(source) { }
    }

    [TestMethod]
    public void Connect_ShouldAdviseConnectionPoint()
    {
        // Arrange
        var mockContainer = new Mock<IConnectionPointContainer>();
        var mockCP = new Mock<IConnectionPoint>();
        int cookie = 123;

        mockContainer.Setup(c => c.FindConnectionPoint(ref It.Ref<Guid>.IsAny, out It.Ref<IConnectionPoint>.IsAny))
            .Callback(new FindCPCallback((ref Guid g, out IConnectionPoint cp) => cp = mockCP.Object));
        
        mockCP.Setup(cp => cp.Advise(It.IsAny<object>(), out cookie));

        // Act
        using var sink = new TestEventSink();
        sink.Connect(mockContainer.Object);

        // Assert
        mockCP.Verify(cp => cp.Advise(sink, out It.Ref<int>.IsAny), Times.Once);
    }

    [TestMethod]
    public void Disconnect_ShouldUnadviseConnectionPoint()
    {
        // Arrange
        var mockContainer = new Mock<IConnectionPointContainer>();
        var mockCP = new Mock<IConnectionPoint>();
        int cookie = 123;

        mockContainer.Setup(c => c.FindConnectionPoint(ref It.Ref<Guid>.IsAny, out It.Ref<IConnectionPoint>.IsAny))
            .Callback(new FindCPCallback((ref Guid g, out IConnectionPoint cp) => cp = mockCP.Object));
        
        mockCP.Setup(cp => cp.Advise(It.IsAny<object>(), out cookie));

        // Act
        var sink = new TestEventSink();
        sink.Connect(mockContainer.Object);
        sink.Disconnect();

        // Assert
        mockCP.Verify(cp => cp.Unadvise(cookie), Times.Once);
    }

    delegate void FindCPCallback(ref Guid guid, out IConnectionPoint cp);
}
