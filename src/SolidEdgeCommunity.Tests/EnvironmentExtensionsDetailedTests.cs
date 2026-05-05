using Moq;
using SolidEdgeCommunity.Extensions;
using SolidEdgeFramework;

namespace SolidEdgeCommunity.Tests;

[TestClass]
public class EnvironmentExtensionsDetailedTests
{
    [TestMethod]
    public void GetCommandConstantType_ShouldReturnCorrectTypes()
    {
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Application, typeof(SolidEdgeConstants.SolidEdgeCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Assembly, typeof(AssemblyCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.DMAssembly, typeof(AssemblyCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.CuttingPlaneLine, typeof(CuttingPlaneLineCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Draft, typeof(DetailCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.DrawingViewEdit, typeof(DrawingViewEditCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Explode, typeof(ExplodeCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Layout, typeof(LayoutCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Sketch, typeof(LayoutInPartCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Motion, typeof(MotionCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Part, typeof(PartCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.DMPart, typeof(PartCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Profile, typeof(ProfileCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.ProfileHole, typeof(ProfileHoleCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.ProfilePattern, typeof(ProfilePatternCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.ProfileRevolved, typeof(ProfileRevolvedCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.SheetMetal, typeof(SheetMetalCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.DMSheetMetal, typeof(SheetMetalCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Simplify, typeof(SimplifyCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Studio, typeof(StudioCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.XpresRoute, typeof(TubingCommandConstants));
        VerifyEnv(SolidEdgeSDK.EnvironmentCategories.Weldment, typeof(WeldmentCommandConstants));
        
        // Test unknown category
        VerifyEnv(Guid.NewGuid(), null);
    }

    private void VerifyEnv(Guid catid, Type? expectedType)
    {
        var mockEnv = new Mock<SolidEdgeFramework.Environment>();
        mockEnv.SetupGet(e => e.CATID).Returns(catid.ToString());
        Assert.AreEqual(expectedType, mockEnv.Object.GetCommandConstantType());
    }

    [TestMethod]
    public void LookupByCategoryId_ShouldReturnCorrectEnvironment()
    {
        // Arrange
        var mockEnvs = new Mock<Environments>();
        var mockEnv = new Mock<SolidEdgeFramework.Environment>();
        var guid = Guid.NewGuid();
        mockEnv.SetupGet(e => e.CATID).Returns(guid.ToString());
        mockEnvs.SetupGet(e => e.Count).Returns(1);
        mockEnvs.Setup(e => e.Item(1)).Returns(mockEnv.Object);

        // Act
        var result = mockEnvs.Object.LookupByCategoryId(guid);

        // Assert
        Assert.AreSame(mockEnv.Object, result);
    }
}
