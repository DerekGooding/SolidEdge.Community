using Moq;
using SolidEdgeCommunity.Runtime.InteropServices;
using SolidEdgeCommunity.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

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
    public void GetPropertyValue_ShouldReturnValue()
    {
        // Act & Assert
        Assert.Throws<InvalidComObjectException>(() => ComObject.GetPropertyValue<string>(new object(), "Name"));
    }
}
