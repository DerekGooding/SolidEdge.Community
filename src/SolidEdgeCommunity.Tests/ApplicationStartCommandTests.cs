using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class ApplicationStartCommandTests
{
    [TestMethod]
    public void StartCommand_Overloads_ShouldCallBaseMethod()
    {
        var mockApp = new Mock<Application>();

        // We use (EnumTypeName)0 because the exact constant names are hard to guess from here
        // and any valid enum value will trigger the extension method.
        mockApp.Object.StartCommand((AssemblyCommandConstants)0);
        mockApp.Object.StartCommand((CuttingPlaneLineCommandConstants)0);
        mockApp.Object.StartCommand((DetailCommandConstants)0);
        mockApp.Object.StartCommand((DrawingViewEditCommandConstants)0);
        mockApp.Object.StartCommand((ExplodeCommandConstants)0);
        mockApp.Object.StartCommand((LayoutCommandConstants)0);
        mockApp.Object.StartCommand((LayoutInPartCommandConstants)0);
        mockApp.Object.StartCommand((MotionCommandConstants)0);
        mockApp.Object.StartCommand((PartCommandConstants)0);
        mockApp.Object.StartCommand((ProfileCommandConstants)0);
        mockApp.Object.StartCommand((ProfileHoleCommandConstants)0);
        mockApp.Object.StartCommand((ProfilePatternCommandConstants)0);
        mockApp.Object.StartCommand((ProfileRevolvedCommandConstants)0);
        mockApp.Object.StartCommand((SheetMetalCommandConstants)0);
        mockApp.Object.StartCommand((SimplifyCommandConstants)0);
        mockApp.Object.StartCommand((StudioCommandConstants)0);
        mockApp.Object.StartCommand((TubingCommandConstants)0);
        mockApp.Object.StartCommand((WeldmentCommandConstants)0);

        mockApp.Verify(a => a.StartCommand(It.IsAny<SolidEdgeFramework.SolidEdgeCommandConstants>()), Times.Exactly(18));
    }
}
