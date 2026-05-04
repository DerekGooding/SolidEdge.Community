using System;
using System.Collections.Generic;

namespace SolidEdgeCommunity.Extensions;

/// <summary>
/// SolidEdgeDraft.DraftDocument extensions.
/// </summary>
public static class DraftDocumentExtensions
{
    /// <summary>
    /// Returns an enumerable collection of drawing objects.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static IEnumerable<object> EnumerateDrawingObjects(this SolidEdgeDraft.DraftDocument document)
    {
        foreach (SolidEdgeDraft.Section section in document.Sections)
        {
            foreach (var drawingObject in section.EnumerateDrawingObjects())
            {
                yield return drawingObject;
            }
        }
    }

    /// <summary>
    /// Returns an enumerable collection of drawing objects of the specified type.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static IEnumerable<T> EnumerateDrawingObjects<T>(this SolidEdgeDraft.DraftDocument document) where T : class
    {
        foreach (SolidEdgeDraft.Section section in document.Sections)
        {
            foreach (var drawingObject in section.EnumerateDrawingObjects<T>())
            {
                yield return drawingObject;
            }
        }
    }

    /// <summary>
    /// Returns an enumerable collection of drawing objects in the specified section.
    /// </summary>
    /// <param name="document"></param>
    /// <param name="sectionType"></param>
    /// <returns></returns>
    public static IEnumerable<object> EnumerateDrawingObjects(this SolidEdgeDraft.DraftDocument document, SolidEdgeDraft.SheetSectionTypeConstants sectionType)
    {
        foreach (SolidEdgeDraft.Section section in document.Sections)
        {
            if (section.Type == sectionType)
            {
                foreach (var drawingObject in section.EnumerateDrawingObjects())
                {
                    yield return drawingObject;
                }
            }
        }
    }

    /// <summary>
    /// Returns an enumerable collection of drawing objects of the specified type in the specified section.
    /// </summary>
    /// <param name="document"></param>
    /// <param name="sectionType"></param>
    /// <returns></returns>
    public static IEnumerable<object> EnumerateDrawingObjects<T>(this SolidEdgeDraft.DraftDocument document, SolidEdgeDraft.SheetSectionTypeConstants sectionType) where T : class
    {
        foreach (SolidEdgeDraft.Section section in document.Sections)
        {
            if (section.Type == sectionType)
            {
                foreach (var drawingObject in section.EnumerateDrawingObjects<T>())
                {
                    yield return drawingObject;
                }
            }
        }
    }

    /// <summary>
    /// Returns an enumerable collection of drawing views.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static IEnumerable<SolidEdgeDraft.DrawingView> EnumerateDrawingViews(this SolidEdgeDraft.DraftDocument document)
    {
        foreach (SolidEdgeDraft.Sheet sheet in document.Sheets)
        {
            foreach (SolidEdgeDraft.DrawingView drawingView in sheet.DrawingViews)
            {
                yield return drawingView;
            }
        }
    }

    /// <summary>
    /// Returns an enumerable collection of drawing views in the specified section.
    /// </summary>
    /// <param name="document"></param>
    /// <param name="sectionType"></param>
    /// <returns></returns>
    public static IEnumerable<SolidEdgeDraft.DrawingView> EnumerateDrawingViews(this SolidEdgeDraft.DraftDocument document, SolidEdgeDraft.SheetSectionTypeConstants sectionType)
    {
        foreach (SolidEdgeDraft.Section section in document.Sections)
        {
            if (section.Type == sectionType)
            {
                foreach (SolidEdgeDraft.DrawingView drawingView in section.EnumerateDrawingViews())
                {
                    yield return drawingView;
                }
            }
        }
    }

    /// <summary>
    /// Returns the version of Solid Edge that was used to create the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Version GetCreatedVersion(this SolidEdgeDraft.DraftDocument document) => new(document.CreatedVersion);

    /// <summary>
    /// Returns the version of Solid Edge that was used the last time the referenced document was saved.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Version GetLastSavedVersion(this SolidEdgeDraft.DraftDocument document) => new(document.LastSavedVersion);

    /// <summary>
    /// Returns the properties for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static PropertySets GetProperties(this SolidEdgeDraft.DraftDocument document) => document.Properties as PropertySets;

    /// <summary>
    /// Returns the summary information property set for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static SummaryInfo GetSummaryInfo(this SolidEdgeDraft.DraftDocument document) => document.SummaryInfo as SummaryInfo;

    /// <summary>
    /// Returns a collection of variables for the referenced document.
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public static Variables GetVariables(this SolidEdgeDraft.DraftDocument document) => document.Variables as Variables;
}