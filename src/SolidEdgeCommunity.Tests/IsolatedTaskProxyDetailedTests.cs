using Moq;
using SolidEdgeCommunity;
using SolidEdgeFramework;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class IsolatedTaskProxyDetailedTests
{
    private class TestProxy : IsolatedTaskProxy
    {
        public T TestUnwrap<T>(object rcw) where T : class => UnwrapRuntimeCallableWrapper<T>(rcw);
    }

    [TestMethod]
    public void UnwrapRuntimeCallableWrapper_ShouldCastCorrectly()
    {
        var proxy = new TestProxy();
        var mockApp = new Mock<Application>();
        
        var result = proxy.TestUnwrap<Application>(mockApp.Object);
        
        Assert.AreSame(mockApp.Object, result);
    }

    [TestMethod]
    public void UnwrapRuntimeCallableWrapper_ShouldReturnNull_OnInvalidCast()
    {
        var proxy = new TestProxy();
        
        var result = proxy.TestUnwrap<Application>(new object());
        
        Assert.IsNull(result);
    }

    [TestMethod]
    public void Application_Property_ShouldWork()
    {
        var proxy = new TestProxy();
        var mockApp = new Mock<Application>();
        proxy.Application = mockApp.Object;
        Assert.AreSame(mockApp.Object, proxy.Application);
    }

    [TestMethod]
    public void Document_Property_ShouldWork()
    {
        var proxy = new TestProxy();
        var mockDoc = new Mock<SolidEdgeDocument>();
        proxy.Document = mockDoc.Object;
        Assert.AreSame(mockDoc.Object, proxy.Document);
    }
}
