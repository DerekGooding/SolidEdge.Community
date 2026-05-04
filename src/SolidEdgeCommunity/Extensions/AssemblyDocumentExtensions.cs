using System;

namespace SolidEdgeCommunity.Extensions;

/// <summary>
/// SolidEdgeAssembly.AssemblyDocument extension methods.
/// </summary>
public static class AssemblyDocumentExtensions
{
    /// <summary>
    /// Returns the version of Solid Edge that was used to create the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Version GetCreatedVersion(this SolidEdgeAssembly.AssemblyDocument document) => new(document.CreatedVersion);

    /// <summary>
    /// Returns the version of Solid Edge that was used the last time the referenced document was saved.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Version GetLastSavedVersion(this SolidEdgeAssembly.AssemblyDocument document) => new(document.LastSavedVersion);

    /// <summary>
    /// Returns the properties for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static PropertySets GetProperties(this SolidEdgeAssembly.AssemblyDocument document) => document.Properties as PropertySets;

    /// <summary>
    /// Returns the summary information property set for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static SummaryInfo GetSummaryInfo(this SolidEdgeAssembly.AssemblyDocument document) => document.SummaryInfo as SummaryInfo;

    /// <summary>
    /// Returns a collection of variables for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Variables GetVariables(this SolidEdgeAssembly.AssemblyDocument document) => document.Variables as Variables;
}