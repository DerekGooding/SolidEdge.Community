using SolidEdgeCommunity;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class OleMessageFilterTests
{
    [TestMethod]
    public void HandleInComingCall_ShouldReturnIsHandled()
    {
        var filter = (IMessageFilter)System.Activator.CreateInstance(typeof(OleMessageFilter), true)!;
        var result = filter.HandleInComingCall(0, IntPtr.Zero, 0, IntPtr.Zero);
        Assert.AreEqual(0, result); // SERVERCALL_ISHANDLED
    }

    [TestMethod]
    public void RetryRejectedCall_ShouldReturn99_WhenRetryLater()
    {
        var filter = (IMessageFilter)System.Activator.CreateInstance(typeof(OleMessageFilter), true)!;
        var result = filter.RetryRejectedCall(IntPtr.Zero, 0, 2); // SERVERCALL_RETRYLATER
        Assert.AreEqual(99, result);
    }

    [TestMethod]
    public void RetryRejectedCall_ShouldReturnMinusOne_WhenNotRetryLater()
    {
        var filter = (IMessageFilter)System.Activator.CreateInstance(typeof(OleMessageFilter), true)!;
        var result = filter.RetryRejectedCall(IntPtr.Zero, 0, 1); // SERVERCALL_REJECTED
        Assert.AreEqual(-1, result);
    }
}
