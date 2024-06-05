using System;
using System.Linq;

namespace SolidEdgeCommunity.Extensions
{
    /// <summary>
    /// SolidEdgePart.Arc3D extension methods.
    /// </summary>
    public static class Arc3DExtensions
    {
        public static double[] SafeGetKeypointPosition(this SolidEdgePart.Arc3D arc3d, SolidEdgePart.Sketch3DKeypointType KeypointType, bool throwOnError = false)
        {
            var position = Array.CreateInstance(typeof(double), 0);

            try
            {
                // GetKeypointPosition may throw an exception so wrap in try\catch.
                arc3d.GetKeypointPosition(KeypointType, ref position);
            }
            catch when (!throwOnError)
            {
            }

            return position.OfType<double>().ToArray();
        }
    }
}