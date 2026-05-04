using System;

namespace SolidEdgeCommunity.Extensions;

/// <summary>
/// SolidEdgePart.PartDocument extension methods.
/// </summary>
public static class PartDocumentExtensions
{
    /// <summary>
    /// Returns the version of Solid Edge that was used to create the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Version GetCreatedVersion(this SolidEdgePart.PartDocument document) => new(document.CreatedVersion);

    /// <summary>
    /// Returns the version of Solid Edge that was used the last time the referenced document was saved.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Version GetLastSavedVersion(this SolidEdgePart.PartDocument document) => new(document.LastSavedVersion);

    /// <summary>
    /// Returns the properties for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static PropertySets GetProperties(this SolidEdgePart.PartDocument document) => document.Properties as PropertySets;

    /// <summary>
    /// Returns the summary information property set for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static SummaryInfo GetSummaryInfo(this SolidEdgePart.PartDocument document) => document.SummaryInfo as SummaryInfo;

    /// <summary>
    /// Returns a collection of variables for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Variables GetVariables(this SolidEdgePart.PartDocument document) => document.Variables as Variables;
}