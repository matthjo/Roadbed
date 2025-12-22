/*
 * The namespace Roadbed.Common.Extensions was removed on purpose and replaced with Roadbed so that no additional using statements are required.
 */

namespace Roadbed;

using System.Reflection;
using Roadbed.Common;

/// <summary>
/// Extensions for common Assembly operations.
/// </summary>
public static class CommonAssemblyExtension
{
    #region Public Methods

    /// <summary>
    /// Verifies that an assembly is loaded in the current AppDomain.
    /// </summary>
    /// <param name="assemblyName">Name of the assembly to verify.</param>
    /// <returns>Decision on whether or not the assembly is loaded.</returns>
    public static bool IsAssemblyLoaded(string assemblyName)
    {
        bool result = false;

        if (!string.IsNullOrEmpty(assemblyName))
        {
            // Get all assemblies currently loaded in the current AppDomain
            Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (loadedAssemblies.Length > 0)
            {
                foreach (Assembly assembly in loadedAssemblies)
                {
                    var name = assembly.GetName();
                    string? nameStr = name?.Name;

                    if (!string.IsNullOrEmpty(nameStr) &&
                        nameStr.Equals(assemblyName, StringComparison.OrdinalIgnoreCase))
                    {
                        result = true;
                        break;
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Opens an embedded resource and returns the content as text.
    /// </summary>
    /// <param name="assembly">Assembly to use to locate the embedded resource.</param>
    /// <param name="fileAndExtensionWithFullNamespace">Full namespace of the file to read.</param>
    /// <returns>Content of the embedded resource.</returns>
    /// <remarks>
    /// You need to reference the Embedded Resource by its
    /// full namespace.
    /// <code>
    /// this.GetType().Assembly.ReadTextResource(
    ///      "ProjectName.FolderName.FileName.txt");
    /// </code>
    /// </remarks>
    public static CommonEmbeddedResourceResponse ReadTextResource(this Assembly assembly, string fileAndExtensionWithFullNamespace)
    {
        return CommonAssembly.ReadTextResource(
            assembly,
            fileAndExtensionWithFullNamespace);
    }

    #endregion Public Methods
}