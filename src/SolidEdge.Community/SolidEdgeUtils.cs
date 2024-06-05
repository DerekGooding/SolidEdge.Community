using System;
using System.IO;

namespace SolidEdgeCommunity
{
    /// <summary>
    /// Helper class for interaction with Solid Edge.
    /// </summary>
    public static class SolidEdgeUtils
    {
        //[DllImport("ole32.dll")]
        //static extern int CreateBindCtx(uint reserved, out IBindCtx ppbc);

        //[DllImport("ole32.dll")]
        //static extern void GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

        //const int MK_E_UNAVAILABLE = (int)(0x800401E3 - 0x100000000);
        private const int _mk_E_UNAVAILABLE = unchecked((int)0x800401E3);

        /// <summary>
        /// Connects to a running instance of Solid Edge.
        /// </summary>
        /// <returns>
        /// An object of type SolidEdgeFramework.Application.
        /// </returns>
        public static SolidEdgeFramework.Application Connect() => Connect(startIfNotRunning: false);

        /// <summary>
        /// Connects to or starts a new instance of Solid Edge.
        /// </summary>
        /// <param name="startIfNotRunning"></param>
        /// <returns>
        /// An object of type SolidEdgeFramework.Application.
        /// </returns>
        public static SolidEdgeFramework.Application Connect(bool startIfNotRunning)
        {
            try
            {
                // Attempt to connect to a running instance of Solid Edge.
                return (SolidEdgeFramework.Application)Marshal.GetActiveObject(SolidEdgeSDK.PROGID.SolidEdge_Application);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                switch (ex.ErrorCode)
                {
                    // Solid Edge is not running.
                    case _mk_E_UNAVAILABLE:
                        if (startIfNotRunning)
                        {
                            // Start Solid Edge.
                            return Start();
                        }
                        else
                        {
                            // Rethrow exception.
                            throw;
                        }
                    default:
                        // Rethrow exception.
                        throw;
                }
            }
            catch
            {
                // Rethrow exception.
                throw;
            }
        }

        /// <summary>
        /// Connects to or starts a new instance of Solid Edge.
        /// </summary>
        /// <param name="startIfNotRunning"></param>
        /// <param name="ensureVisible"></param>
        /// <returns>
        /// An object of type SolidEdgeFramework.Application.
        /// </returns>
        public static SolidEdgeFramework.Application Connect(bool startIfNotRunning, bool ensureVisible)
        {
            SolidEdgeFramework.Application application = null;

            try
            {
                // Attempt to connect to a running instance of Solid Edge.
                application = (SolidEdgeFramework.Application)Marshal.GetActiveObject(SolidEdgeSDK.PROGID.SolidEdge_Application);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                switch (ex.ErrorCode)
                {
                    // Solid Edge is not running.
                    case _mk_E_UNAVAILABLE:
                        if (startIfNotRunning)
                        {
                            // Start Solid Edge.
                            application = Start();
                            break;
                        }
                        else
                        {
                            // Rethrow exception.
                            throw;
                        }
                    default:
                        // Rethrow exception.
                        throw;
                }
            }

            if ((application != null) && ensureVisible)
            {
                application.Visible = true;
            }

            return application;
        }

        /// <summary>
        /// Returns the path to the Solid Edge installation folder.
        /// </summary>
        /// <remarks>
        /// Typically 'C:\Program Files\Solid Edge XXX'.
        /// </remarks>
        public static string GetInstalledPath()
        {
            /* Get path to Solid Edge program directory. */
            var programDirectory = new DirectoryInfo(GetProgramFolderPath());

            /* Get path to Solid Edge installation directory. */
            var installationDirectory = programDirectory.Parent;

            return installationDirectory.FullName;
        }

        public static System.Globalization.CultureInfo GetInstalledLanguage()
        {
            var installData = new SEInstallDataLib.SEInstallData();

            try
            {
                return System.Globalization.CultureInfo.GetCultureInfo(installData.GetLanguageID());
            }
            finally
            {
                if (installData != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(installData);
                }
            }
        }

        /// <summary>
        /// Returns the path to the Solid Edge program folder.
        /// </summary>
        /// <remarks>
        /// Typically 'C:\Program Files\Solid Edge XXX\Program'.
        /// </remarks>
        public static string GetProgramFolderPath()
        {
            var installData = new SEInstallDataLib.SEInstallData();

            try
            {
                /* Get path to Solid Edge program directory. */
                return installData.GetInstalledPath();
            }
            finally
            {
                if (installData != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(installData);
                }
            }
        }

        /// <summary>
        /// Returns the path to the Solid Edge training folder.
        /// </summary>
        /// <remarks>
        /// Typically 'C:\Program Files\Solid Edge XXX\Training'.
        /// </remarks>
        public static string GetTrainingFolderPath() => new DirectoryInfo(Path.Combine(GetInstalledPath(), "Training")).FullName;

        /// <summary>
        /// Returns a Version object representing the installed version of Solid Edge.
        /// </summary>
        /// <returns></returns>
        public static Version GetVersion()
        {
            var installData = new SEInstallDataLib.SEInstallData();

            return new Version(installData.GetMajorVersion(), installData.GetMinorVersion(), installData.GetServicePackVersion(), installData.GetBuildNumber());
        }

        /// <summary>
        /// Creates and returns a new instance of Solid Edge.
        /// </summary>
        /// <returns>
        /// An object of type SolidEdgeFramework.Application.
        /// </returns>
        public static SolidEdgeFramework.Application Start()
        {
            // On a system where Solid Edge is installed, the COM ProgID will be
            // defined in registry: HKEY_CLASSES_ROOT\SolidEdge.Application
            Type t = Type.GetTypeFromProgID(progID: SolidEdgeSDK.PROGID.SolidEdge_Application, throwOnError: true);

            // Using the discovered Type, create and return a new instance of Solid Edge.
            return (SolidEdgeFramework.Application)Activator.CreateInstance(type: t);
        }
    }
}