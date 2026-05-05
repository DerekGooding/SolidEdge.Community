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

    [TestMethod]
    public void MessagePending_ShouldReturnWaitDefProcess()
    {
        var filter = (IMessageFilter)System.Activator.CreateInstance(typeof(OleMessageFilter), true)!;
        var result = filter.MessagePending(IntPtr.Zero, 0, 0);
        Assert.AreEqual(2, result); // PENDINGMSG_WAITDEFPROCESS
    }

    [TestMethod]
    public void Unregister_ShouldNotThrow()
    {
        OleMessageFilter.Unregister();
    }

    [TestMethod]
    public void Register_ShouldThrow_WhenNotSTA()
    {
        //mstest runs tests in MTA by default unless configured otherwise.
        if (System.Threading.Thread.CurrentThread.GetApartmentState() != System.Threading.ApartmentState.STA)
        {
            Assert.Throws<System.Exception>(() => OleMessageFilter.Register());
        }
    }
}