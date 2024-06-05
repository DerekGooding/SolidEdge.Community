using System;

namespace SolidEdgeCommunity.Extensions
{
    /// <summary>
    /// SolidEdgeFileProperties.PropertySets extensions.
    /// </summary>
    public static class PropertySetsExtensions
    {
        ///// <summary>
        ///// Returns all property sets as an IEnumerable.
        ///// </summary>
        ///// <param name="propertySets"></param>
        //public static IEnumerable<SolidEdgeFileProperties.Properties> AsEnumerable(this SolidEdgeFileProperties.PropertySets propertySets)
        //{
        //    List<SolidEdgeFileProperties.Properties> list = new List<SolidEdgeFileProperties.Properties>();

        //    foreach (var properties in propertySets)
        //    {
        //        list.Add((SolidEdgeFileProperties.Properties)properties);
        //    }

        //    return list.AsEnumerable();
        //}

        /// <summary>
        /// Closes an open document with the option to save changes.
        /// </summary>
        /// <param name="propertySets"></param>
        /// <param name="saveChanges"></param>
        public static void Close(this SolidEdgeFileProperties.PropertySets propertySets, bool saveChanges)
        {
            if (saveChanges)
            {
                propertySets.Save();
            }

            propertySets.Close();
        }

        /// <summary>
        /// Adds or modifies a custom property.
        /// </summary>
        internal static SolidEdgeFileProperties.Property InternalUpdateCustomProperty(this SolidEdgeFileProperties.PropertySets propertySets, string propertyName, object propertyValue)
        {
            // Get a reference to the custom property set.
            SolidEdgeFileProperties.Properties customPropertySet = (SolidEdgeFileProperties.Properties)propertySets["Custom"];

            try
            {
                // Attempt to get the custom property.
                SolidEdgeFileProperties.Property property = (SolidEdgeFileProperties.Property)customPropertySet[propertyName];

                // If we get here, the custom property exists so update the value.
                ((SolidEdgeFileProperties.Property)null).Value = propertyValue;
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                // If we get here, the custom property does not exist so add it.
                customPropertySet.Add(propertyName, propertyValue);
            }

            return null;
        }

        /// <summary>
        /// Adds or modifies a custom property.
        /// </summary>
        public static SolidEdgeFileProperties.Property UpdateCustomProperty(this SolidEdgeFileProperties.PropertySets propertySets, string propertyName, bool propertyValue)
        {
            return propertySets.InternalUpdateCustomProperty(propertyName, propertyValue);
        }

        /// <summary>
        /// Adds or modifies a custom property.
        /// </summary>
        public static SolidEdgeFileProperties.Property UpdateCustomProperty(this SolidEdgeFileProperties.PropertySets propertySets, string propertyName, DateTime propertyValue)
        {
            return propertySets.InternalUpdateCustomProperty(propertyName, propertyValue);
        }

        /// <summary>
        /// Adds or modifies a custom property.
        /// </summary>
        public static SolidEdgeFileProperties.Property UpdateCustomProperty(this SolidEdgeFileProperties.PropertySets propertySets, string propertyName, double propertyValue)
        {
            return propertySets.InternalUpdateCustomProperty(propertyName, propertyValue);
        }

        /// <summary>
        /// Adds or modifies a custom property.
        /// </summary>
        public static SolidEdgeFileProperties.Property UpdateCustomProperty(this SolidEdgeFileProperties.PropertySets propertySets, string propertyName, int propertyValue)
        {
            return propertySets.InternalUpdateCustomProperty(propertyName, propertyValue);
        }

        /// <summary>
        /// Adds or modifies a custom property.
        /// </summary>
        public static SolidEdgeFileProperties.Property UpdateCustomProperty(this SolidEdgeFileProperties.PropertySets propertySets, string propertyName, string propertyValue)
        {
            return propertySets.InternalUpdateCustomProperty(propertyName, propertyValue);
        }
    }
}