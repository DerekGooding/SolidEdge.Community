using System.Collections.Generic;

namespace SolidEdgeCommunity.Extensions;

/// <summary>
/// SolidEdgeDraft.Section extensions.
/// </summary>
public static class SectionExtensions
{
    /// <summary>
    /// Returns an enumerable collection of drawing objects.
    /// </summary>
    /// <param name="section"></param>
    /// <returns></returns>
    public static IEnumerable<object> EnumerateDrawingObjects(this SolidEdgeDraft.Section section)
    {
        foreach (var drawingObject in EnumerateDrawingObjects<object>(section))
        {
            yield return drawingObject;
        }
    }

    /// <summary>
    /// Returns an enumerable collection of drawing objects of the specified type.
    /// </summary>
    public static IEnumerable<T> EnumerateDrawingObjects<T>(this SolidEdgeDraft.Section section) where T : class
    {
        var sheets = section.Sheets;

        for (int i = 1; i <= sheets.Count; i++)
        {
            var sheet = sheets.Item(i);

            // The following section types are hidden and should not be used.
            if (sheet.SectionType == SolidEdgeDraft.SheetSectionTypeConstants.igDrawingViewSection) continue;
            if (sheet.SectionType == SolidEdgeDraft.SheetSectionTypeConstants.igBlockViewSection) continue;

            // Should work but throws an exception...
            //foreach (var drawingObject in sheet.DrawingObjects)

            var drawingObjects = sheet.DrawingObjects;

            for (int j = 1; j <= drawingObjects.Count; j++)
            {
                var drawingObject = drawingObjects.Item(j);

                if (drawingObject is T t)
                {
                    yield return t;
                }
            }
        }
    }

    /// <summary>
    /// Returns an enumerable collection of drawing views.
    /// </summary>
    /// <param name="section"></param>
    /// <returns></returns>
    public static IEnumerable<SolidEdgeDraft.DrawingView> EnumerateDrawingViews(this SolidEdgeDraft.Section section)
    {
        var sheets = section.Sheets;

        for (int i = 1; i <= sheets.Count; i++)
        {
            var sheet = sheets.Item(i);
            var drawingViews = sheet.DrawingViews;

            for (int j = 1; j <= drawingViews.Count; j++)
            {
                yield return drawingViews.Item(j);
            }
        }
    }
}