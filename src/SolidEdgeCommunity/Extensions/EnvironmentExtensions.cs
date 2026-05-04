using System;

namespace SolidEdgeCommunity.Extensions;

/// <summary>
/// SolidEdgeFramework.Environment extension methods.
/// </summary>
public static class EnvironmentExtensions
{
    /// <summary>
    /// Returns a Guid representing the environment category.
    /// </summary>
    public static Guid GetCategoryId(this Environment environment) => new(environment.CATID);

    /// <summary>
    /// Returns a Type representing the corresponding command constants for a particular environment.
    /// </summary>
    /// <param name="environment"></param>
    /// <returns></returns>
    public static Type GetCommandConstantType(this Environment environment)
    {
        var categoryId = environment.GetCategoryId();

        if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Application))
        {
            return typeof(SolidEdgeConstants.SolidEdgeCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Assembly))
        {
            return typeof(AssemblyCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.DMAssembly))
        {
            return typeof(AssemblyCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.CuttingPlaneLine))
        {
            return typeof(CuttingPlaneLineCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Draft))
        {
            return typeof(DetailCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.DrawingViewEdit))
        {
            return typeof(DrawingViewEditCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Explode))
        {
            return typeof(ExplodeCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Layout))
        {
            return typeof(LayoutCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Sketch))
        {
            return typeof(LayoutInPartCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Motion))
        {
            return typeof(MotionCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Part))
        {
            return typeof(PartCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.DMPart))
        {
            return typeof(PartCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Profile))
        {
            return typeof(ProfileCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.ProfileHole))
        {
            return typeof(ProfileHoleCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.ProfilePattern))
        {
            return typeof(ProfilePatternCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.ProfileRevolved))
        {
            return typeof(ProfileRevolvedCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.SheetMetal))
        {
            return typeof(SheetMetalCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.DMSheetMetal))
        {
            return typeof(SheetMetalCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Simplify))
        {
            return typeof(SimplifyCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Studio))
        {
            return typeof(StudioCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.XpresRoute))
        {
            return typeof(TubingCommandConstants);
        }
        else if (categoryId.Equals(SolidEdgeSDK.EnvironmentCategories.Weldment))
        {
            return typeof(WeldmentCommandConstants);
        }

        return null;
    }

    /// <summary>
    /// Returns a SolidEdgeFramework.Environment by name.
    /// </summary>
    public static Environment LookupByName(this Environments environments, string name)
    {
        for (int i = 1; i <= environments.Count; i++)
        {
            var environment = environments.Item(i);
            if (environment.Name.Equals(name))
            {
                return environment;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns a SolidEdgeFramework.Environment by category id..
    /// </summary>
    public static Environment LookupByCategoryId(this Environments environments, Guid categoryId)
    {
        for (int i = 1; i <= environments.Count; i++)
        {
            var environment = environments.Item(i);
            if (environment.GetCategoryId().Equals(categoryId))
            {
                return environment;
            }
        }

        return null;
    }
}