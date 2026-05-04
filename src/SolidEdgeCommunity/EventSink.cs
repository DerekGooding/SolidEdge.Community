using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace SolidEdgeCommunity;

public abstract class EventSink<T> : IDisposable where T : class
{
    private IConnectionPoint _connectionPoint;
    private int _cookie;

    protected EventSink()
    {
    }

    /// <summary>
    /// Establishes a connection between a connection point object and the client's sink.
    /// </summary>
    /// <param name="source">An event source that implements IConnectionPointContainer.</param>
    protected EventSink(object source) => Connect(source); // If event source is specified, automatically connect.

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Clean up all managed resources
        }

        Disconnect();
    }

    /// <summary>
    /// Establishes a connection between a connection point object and the client's sink.
    /// </summary>
    /// <param name="source">An event source that implements IConnectionPointContainer.</param>
    public void Connect(object source)
    {
        bool lockTaken = false;
        IConnectionPointContainer container = null;

        try
        {
            Monitor.Enter(this, ref lockTaken);

            // If previous call was made, disconnect existing connection.
            if (container != null)
            {
                Disconnect();
            }

            // QueryInterface for IConnectionPointContainer.
            container = (IConnectionPointContainer)source;

            var item = typeof(T).GUID;
            // Find the connection point by the GUID of type T.
            container.FindConnectionPoint(ref item, out _connectionPoint);

            // Establish the sink connection.
            _connectionPoint?.Advise(this, out _cookie);
        }
        finally
        {
            if (lockTaken)
            {
                Monitor.Exit(this);
            }
        }
    }

    /// <summary>
    /// Terminates the advisory connection between a connection point and a client's sink.
    /// </summary>
    public void Disconnect()
    {
        bool lockTaken = false;

        try
        {
            Monitor.Enter(this, ref lockTaken);

            // Terminate the sink connection.
            _connectionPoint?.Unadvise(_cookie);
        }
        finally
        {
            if (_connectionPoint != null)
            {
                try
                {
                    var count = System.Runtime.InteropServices.Marshal.ReleaseComObject(_connectionPoint);
                }
                catch
                {
                }

                _connectionPoint = null;
                _cookie = 0;
            }

            if (lockTaken)
            {
                Monitor.Exit(this);
            }
        }
    }
}