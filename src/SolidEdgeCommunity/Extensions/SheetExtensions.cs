using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SolidEdgeCommunity.Extensions;

public static partial class SheetExtensions
{
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool CloseClipboard();

    [LibraryImport("user32.dll")]
    [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static partial IntPtr GetClipboardData(uint format);

    [LibraryImport("user32.dll")]
    private static partial IntPtr GetClipboardOwner();

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool IsClipboardFormatAvailable(uint format);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool OpenClipboard(IntPtr hWndNewOwner);

    [LibraryImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool DeleteEnhMetaFile(IntPtr hemf);

    [LibraryImport("gdi32.dll")]
    private static partial uint GetEnhMetaFileBits(IntPtr hemf, uint cbBuffer, [Out] byte[] lpbBuffer);

    private const uint _cf_ENHMETAFILE = 14;

    public static System.Drawing.Imaging.Metafile GetEnhancedMetafile(this SolidEdgeDraft.Sheet sheet)
    {
        try
        {
            // Copy the sheet as an EMF to the windows clipboard.
            sheet.CopyEMFToClipboard();

            if (OpenClipboard(IntPtr.Zero))
            {
                if (IsClipboardFormatAvailable(_cf_ENHMETAFILE))
                {
                    // Get the handle to the EMF.
                    IntPtr henhmetafile = GetClipboardData(_cf_ENHMETAFILE);

                    return new System.Drawing.Imaging.Metafile(henhmetafile, true);
                }
                else
                {
                    throw new Exception("CF_ENHMETAFILE is not available in clipboard.");
                }
            }
            else
            {
                throw new Exception("Error opening clipboard.");
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            CloseClipboard();
        }
    }

    public static void SaveAsEnhancedMetafile(this SolidEdgeDraft.Sheet sheet, string filename)
    {
        try
        {
            // Copy the sheet as an EMF to the windows clipboard.
            sheet.CopyEMFToClipboard();

            if (OpenClipboard(IntPtr.Zero))
            {
                if (IsClipboardFormatAvailable(_cf_ENHMETAFILE))
                {
                    // Get the handle to the EMF.
                    IntPtr hEMF = GetClipboardData(_cf_ENHMETAFILE);

                    // Query the size of the EMF.
                    uint len = GetEnhMetaFileBits(hEMF, 0, null);
                    byte[] rawBytes = new byte[len];

                    // Get all of the bytes of the EMF.
                    GetEnhMetaFileBits(hEMF, len, rawBytes);

                    // Write all of the bytes to a file.
                    File.WriteAllBytes(filename, rawBytes);

                    // Delete the EMF handle.
                    DeleteEnhMetaFile(hEMF);
                }
                else
                {
                    throw new Exception("CF_ENHMETAFILE is not available in clipboard.");
                }
            }
            else
            {
                throw new Exception("Error opening clipboard.");
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            CloseClipboard();
        }
    }
}