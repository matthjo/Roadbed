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
        bool isAssemblyLoaded = false;

        if (!string.IsNullOrEmpty(assemblyName))
        {
            Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in loadedAssemblies)
            {
                var name = assembly.GetName();
                string? nameStr = name?.Name;

                if (!string.IsNullOrEmpty(nameStr) &&
                    nameStr.Equals(assemblyName, StringComparison.OrdinalIgnoreCase))
                {
                    isAssemblyLoaded = true;
                    break;
                    /*
                    Visual Studio doesn't support ignoring a single line of code from code coverage.
                    The "break;" causes the 'if' statements closing bracket (}) not to be covered.
                     */
                }
            }
        }

        return isAssemblyLoaded;
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