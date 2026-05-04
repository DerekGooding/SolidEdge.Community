using SolidEdgeCommunity.Runtime.InteropServices.ComTypes;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace SolidEdgeCommunity.Runtime.InteropServices;

/// <summary>
/// COM object wrapper class.
/// </summary>
public static class ComObject
{
    private const int LOCALE_SYSTEM_DEFAULT = 2048;

    /// <summary>
    /// Using IDispatch, returns the ITypeInfo of the specified object.
    /// </summary>
    /// <param name="comObject"></param>
    /// <returns></returns>
    public static ITypeInfo GetITypeInfo(object comObject)
    {
        if (!System.Runtime.InteropServices.Marshal.IsComObject(comObject)) throw new InvalidComObjectException();

        if (comObject is IDispatch dispatch)
        {
            return dispatch.GetTypeInfo(0, LOCALE_SYSTEM_DEFAULT);
        }

        return null;
    }

    /// <summary>
    /// Returns a strongly typed property by name using the specified COM object.
    /// </summary>
    /// <typeparam name="T">The type of the property to return.</typeparam>
    /// <param name="comObject"></param>
    /// <param name="name">The name of the property to retrieve.</param>
    /// <returns></returns>
    public static T GetPropertyValue<T>(object comObject, string name)
    {
        if (!System.Runtime.InteropServices.Marshal.IsComObject(comObject)) throw new InvalidComObjectException();

        var type = comObject.GetType();
        var value = type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty, null, comObject, null);

        return (T)value;
    }

    /// <summary>
    /// Returns a strongly typed property by name using the specified COM object.
    /// </summary>
    /// <typeparam name="T">The type of the property to return.</typeparam>
    /// <param name="comObject"></param>
    /// <param name="name">The name of the property to retrieve.</param>
    /// <param name="defaultValue">The value to return if the property does not exist.</param>
    /// <returns></returns>
    public static T GetPropertyValue<T>(object comObject, string name, T defaultValue)
    {
        if (!System.Runtime.InteropServices.Marshal.IsComObject(comObject)) throw new InvalidComObjectException();

        var type = comObject.GetType();

        try
        {
            var value = type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty, null, comObject, null);
            return (T)value;
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Using IDispatch, determine the managed type of the specified object.
    /// </summary>
    /// <param name="comObject"></param>
    /// <returns></returns>
    public static Type GetType(object comObject)
    {
        if (!System.Runtime.InteropServices.Marshal.IsComObject(comObject)) throw new InvalidComObjectException();

        Type type = null;
        ITypeInfo typeInfo = null;
        var pTypeAttr = IntPtr.Zero;
        var typeAttr = default(TYPEATTR);

        try
        {
            if (comObject is IDispatch dispatch)
            {
                typeInfo = dispatch.GetTypeInfo(0, LOCALE_SYSTEM_DEFAULT);
                typeInfo.GetTypeAttr(out pTypeAttr);
                typeAttr = System.Runtime.InteropServices.Marshal.PtrToStructure<TYPEATTR>(pTypeAttr);

                // Type can technically be defined in any loaded assembly.
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                // Scan each assembly for a type with a matching GUID.
                foreach (var assembly in assemblies)
                {
                    type = assembly.GetTypes()
                        .Where(x => x.IsInterface)
                        .FirstOrDefault(x => x.GUID.Equals(typeAttr.guid));

                    if (type != null)
                    {
                        // Found what we're looking for so break out of the loop.
                        break;
                    }
                }
            }
        }
        finally
        {
            if (typeInfo != null)
            {
                typeInfo.ReleaseTypeAttr(pTypeAttr);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(typeInfo);
            }
        }

        return type;
    }
}