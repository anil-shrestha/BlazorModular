using System.Reflection;
using Modular.Shared;

namespace MainWeb.Services;

public interface IModulesService
{
    List<Assembly> ModuleAssemblies { get; }
    List<IModule> Modules { get; }

    void SyncModules();
}

public class ModulesService : IModulesService
{
    public List<Assembly> ModuleAssemblies { get; private set; } = new();
    public List<IModule> Modules { get; private set; } = new();

    public ModulesService()
    {
        SyncModules();
    }

    public void SyncModules()
    {
        // var context = new AssemblyLoader(@$"{Environment.CurrentDirectory}\Modules\Modular.HrModule.dll");
        ModuleAssemblies = new();
        Modules = new();
        var fileinfos = new DirectoryInfo(@$"{Environment.CurrentDirectory}\Modules").GetFiles("Modular.*Module.dll")
                        .OrderByDescending(x => x.CreationTime).ThenBy(x => x.Name);
        foreach (var fileinfo in fileinfos)
        {

            // }
            // var files = Directory.GetFiles(@$"{Environment.CurrentDirectory}\Modules", "Modular.*Module.dll");
            // // var files = Directory.GetFiles(@$"{Environment.CurrentDirectory}\Modules", "Modular.*.dll");
            // foreach (var file in files)
            // {
            var context = new AssemblyLoader("");
            System.Console.WriteLine("----------------------------------");
            System.Console.WriteLine(fileinfo.Name);
            var assembly = context.LoadFromStream(new MemoryStream(File.ReadAllBytes(fileinfo.FullName)));
            if (assembly != null)
            {
                System.Console.Write(assembly.GetName().Name);
                var modules = assembly.GetTypes().Where(type => typeof(IModule).IsAssignableFrom(type)).Select(x => Activator.CreateInstance(x) as IModule);
                if (modules == null || modules.Any() == false)
                {
                    context.Unload();
                    System.Console.WriteLine("............UnLoaded !!");

                    continue;
                }

                if (!ModuleAssemblies.Any(x => x.GetName().Name == assembly.GetName().Name))
                {
                    Modules.AddRange(modules!);
                    ModuleAssemblies.Add(assembly);
                    System.Console.WriteLine("............Loaded !!");
                }
                else
                    System.Console.WriteLine("............Skipped !!");
            }
        }
        // var assembly = context.LoadFromAssemblyPath(@$"{Environment.CurrentDirectory}\Modules\Modular.HrModule.dll");
        // var assembly = .LoadFromAssemblyName(new AssemblyName($"Modular.HrModule"));
        // if (assembly != null)
        // ModuleAssemblies.Add(assembly);
    }

}