using System;
using System.Runtime.InteropServices;


namespace SolidEdgeCommunity
{
    internal class Marshal
    {
        public static object GetActiveObject(string progId) => GetActiveObject(progId, true);
        public static object GetActiveObject(string progId, bool throwOnError = true)
        {
            if (progId == null)
                throw new ArgumentNullException(nameof(progId));

            var hr = CLSIDFromProgIDEx(progId, out var clsid);
            if (hr < 0)
            {
                if (throwOnError)
                    System.Runtime.InteropServices.Marshal.ThrowExceptionForHR(hr);

                return null;
            }

            hr = GetActiveObject(clsid, IntPtr.Zero, out var obj);
            if (hr < 0)
            {
                if (throwOnError)
                    System.Runtime.InteropServices.Marshal.ThrowExceptionForHR(hr);

                return null;
            }
            return obj;
        }
        [DllImport("ole32")]
        private static extern int CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] string lpszProgID, out Guid lpclsid);

        [DllImport("oleaut32")]
        private static extern int GetActiveObject([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, IntPtr pvReserved, [MarshalAs(UnmanagedType.IUnknown)] out object ppunk);
    }
}
