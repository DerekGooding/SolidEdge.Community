using SolidEdgeCommunity;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class IsolatedTaskProxyTests
{
    private class TestProxy : IsolatedTaskProxy
    {
        public void TestInvoke(Action action) => InvokeSTAThread(action);
        public T TestInvoke<T>(Func<T> func) => InvokeSTAThread(func);
    }

    [TestMethod]
    public void InvokeSTAThread_ShouldExecuteAction()
    {
        var proxy = new TestProxy();
        bool executed = false;
        proxy.TestInvoke(() => executed = true);
        Assert.IsTrue(executed);
    }

    [TestMethod]
    public void InvokeSTAThread_ShouldReturnResult()
    {
        var proxy = new TestProxy();
        var result = proxy.TestInvoke(() => "Success");
        Assert.AreEqual("Success", result);
    }
}
