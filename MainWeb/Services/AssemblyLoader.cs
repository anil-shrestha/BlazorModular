using System.Reflection;
using System.Runtime.Loader;

namespace MainWeb.Services;

public class AssemblyLoader : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver _resolver;

    public AssemblyLoader(string assemblyPath) : base(isCollectible: true)
    {
        _resolver = new AssemblyDependencyResolver(assemblyPath);
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        if (assemblyPath != null)
        {
            return LoadFromAssemblyPath(assemblyPath);
        }

        return null;
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        string? libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

        if (libraryPath != null)
        {
            return LoadUnmanagedDllFromPath(libraryPath);
        }

        return IntPtr.Zero;
    }
}