using System;
using System.Linq;

namespace SolidEdgeCommunity.Extensions
{
    /// <summary>
    /// SolidEdgePart.Line3D extension methods.
    /// </summary>
    public static class Line3DExtensions
    {
        public static double[] SafeGetKeypointPosition(this SolidEdgePart.Line3D line3d, SolidEdgePart.Sketch3DKeypointType KeypointType, bool throwOnError = false)
        {
            var position = Array.CreateInstance(typeof(double), 0);

            try
            {
                // GetKeypointPosition may throw an exception so wrap in try\catch.
                line3d.GetKeypointPosition(KeypointType, ref position);
            }
            catch when (!throwOnError)
            {
            }

            return position.OfType<double>().ToArray();
        }
    }
}