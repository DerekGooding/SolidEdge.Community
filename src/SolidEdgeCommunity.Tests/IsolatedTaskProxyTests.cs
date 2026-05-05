namespace SolidEdgeCommunity.Tests;

[TestClass]
public class IsolatedTaskProxyTests
{
    private class TestProxy : IsolatedTaskProxy
    {
        public void TestInvoke(Action action) => InvokeSTAThread(action);
        public void TestInvoke<T1>(Action<T1> action, T1 arg1) => InvokeSTAThread(action, arg1);
        public void TestInvoke<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2) => InvokeSTAThread(action, arg1, arg2);
        public void TestInvoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3) => InvokeSTAThread(action, arg1, arg2, arg3);
        public void TestInvoke<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => InvokeSTAThread(action, arg1, arg2, arg3, arg4);

        public T TestInvoke<T>(Func<T> func) => InvokeSTAThread(func);
        public T TestInvoke<T1, T>(Func<T1, T> func, T1 arg1) => InvokeSTAThread(func, arg1);
        public T TestInvoke<T1, T2, T>(Func<T1, T2, T> func, T1 arg1, T2 arg2) => InvokeSTAThread(func, arg1, arg2);
        public T TestInvoke<T1, T2, T3, T>(Func<T1, T2, T3, T> func, T1 arg1, T2 arg2, T3 arg3) => InvokeSTAThread(func, arg1, arg2, arg3);
        public T TestInvoke<T1, T2, T3, T4, T>(Func<T1, T2, T3, T4, T> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => InvokeSTAThread(func, arg1, arg2, arg3, arg4);
    }

    [TestMethod]
    public void InvokeSTAThread_Overloads_ShouldWork()
    {
        var proxy = new TestProxy();

        proxy.TestInvoke((int a) => Assert.AreEqual(1, a), 1);
        proxy.TestInvoke((int a, int b) => Assert.AreEqual(3, a + b), 1, 2);
        proxy.TestInvoke((int a, int b, int c) => Assert.AreEqual(6, a + b + c), 1, 2, 3);
        proxy.TestInvoke((int a, int b, int c, int d) => Assert.AreEqual(10, a + b + c + d), 1, 2, 3, 4);

        Assert.AreEqual(1, proxy.TestInvoke((int a) => a, 1));
        Assert.AreEqual(3, proxy.TestInvoke((int a, int b) => a + b, 1, 2));
        Assert.AreEqual(6, proxy.TestInvoke((int a, int b, int c) => a + b + c, 1, 2, 3));
        Assert.AreEqual(10, proxy.TestInvoke((int a, int b, int c, int d) => a + b + c + d, 1, 2, 3, 4));
    }

    [TestMethod]
    public void InvokeSTAThread_ShouldThrow_WhenTargetThrows()
    {
        var proxy = new TestProxy();
        Assert.Throws<Exception>(() => proxy.TestInvoke(() => throw new InvalidOperationException()));
    }

    [TestMethod]
    public void InitializeLifetimeService_ShouldReturnNull()
    {
        var proxy = new TestProxy();
#pragma warning disable CS0618
        Assert.IsNull(proxy.InitializeLifetimeService());
#pragma warning restore CS0618
    }
}