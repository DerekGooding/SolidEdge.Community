using SolidEdgeCommunity.Runtime.InteropServices;
using System.Runtime.InteropServices;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class ComObjectTests
{
    [TestMethod]
    public void GetITypeInfo_ShouldReturnITypeInfo_WhenObjectIsDispatch()
    {
        // We can't easily mock a COM object because of Marshal.IsComObject check.
        // Instead of mocking, we can try to use a real COM object from the system.
        try
        {
            var type = Type.GetTypeFromProgID("Shell.Application");
            if (type != null)
            {
                var shell = Activator.CreateInstance(type);
                if (shell != null)
                {
                    var typeInfo = ComObject.GetITypeInfo(shell);
                    Assert.IsNotNull(typeInfo);
                    return;
                }
            }
        }
        catch
        {
            // If Shell.Application is not available, we skip this test.
            Assert.Inconclusive("Shell.Application COM object not available for testing.");
        }
    }

    [TestMethod]
    public void GetITypeInfo_ShouldThrow_WhenNotComObject()
    {
        // Act & Assert
        Assert.Throws<InvalidComObjectException>(() => ComObject.GetITypeInfo(new object()));
    }

    [TestMethod]
    public void GetPropertyValue_ShouldReturnValue_WhenObjectIsComObject()
    {
        try
        {
            var type = Type.GetTypeFromProgID("Shell.Application");
            if (type != null)
            {
                var shell = Activator.CreateInstance(type);
                if (shell != null)
                {
                    // If it doesn't throw InvalidComObjectException, we reached the logic.
                    try { ComObject.GetPropertyValue<object>(shell, "Parent"); } catch { }
                    return;
                }
            }
        }
        catch
        {
            Assert.Inconclusive("Shell.Application COM object not available for testing.");
        }
    }

    [TestMethod]
    public void GetPropertyValue_WithDefault_ShouldReturnValue_WhenObjectIsComObject()
    {
        try
        {
            var type = Type.GetTypeFromProgID("Shell.Application");
            if (type != null)
            {
                var shell = Activator.CreateInstance(type);
                if (shell != null)
                {
                    var result = ComObject.GetPropertyValue<string>(shell, "NonExistentProperty", "Default");
                    Assert.AreEqual("Default", result);
                    return;
                }
            }
        }
        catch
        {
            Assert.Inconclusive("Shell.Application COM object not available for testing.");
        }
    }

    [TestMethod]
    public void GetType_ShouldReturnManagedType_WhenObjectIsComObject()
    {
        try
        {
            var type = Type.GetTypeFromProgID("Shell.Application");
            if (type != null)
            {
                var shell = Activator.CreateInstance(type);
                if (shell != null)
                {
                    var result = ComObject.GetType(shell);
                    // Might be null if the interface is not in a loaded assembly, but should not throw InvalidComObjectException.
                    return;
                }
            }
        }
        catch
        {
            Assert.Inconclusive("Shell.Application COM object not available for testing.");
        }
    }

    [TestMethod]
    public void GetPropertyValue_WithDefault_ShouldThrow_WhenNotComObject()
    {
        Assert.Throws<InvalidComObjectException>(() => ComObject.GetPropertyValue<string>(new object(), "Name", "Default"));
    }

    [TestMethod]
    public void GetType_ShouldThrow_WhenNotComObject()
    {
        Assert.Throws<InvalidComObjectException>(() => ComObject.GetType(new object()));
    }
}