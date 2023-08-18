using System.Reflection;
using Modular.Shared;

namespace MainWeb.Services;

public class ModulesService
{
    public List<Assembly> ModuleAssemblies { get; } = new();
    public List<IModule> Modules { get; } = new();

    public ModulesService()
    {
        // var context = new AssemblyLoader(@$"{Environment.CurrentDirectory}\Modules\Modular.HrModule.dll");
        
        var files = Directory.GetFiles(@$"{Environment.CurrentDirectory}\Modules", "Modular.*Module.dll");
        // var files = Directory.GetFiles(@$"{Environment.CurrentDirectory}\Modules", "Modular.*.dll");
        foreach (var file in files)
        {
            //TODO: optimize assembly loading using cache and possibly singleton with reload function
            var context = new AssemblyLoader("");
            // System.Console.WriteLine(file);
            var assembly = context.LoadFromStream(new MemoryStream(File.ReadAllBytes(file)));
            if (assembly != null)
            {
                var modules = assembly.GetTypes().Where(type=>typeof(IModule).IsAssignableFrom(type)).Select(x=>Activator.CreateInstance(x) as IModule);
                if(modules == null || modules.Any() ==false) {
                    context.Unload();
                }
                Modules.AddRange(modules!);
                ModuleAssemblies.Add(assembly);
            }
        }
        // var assembly = context.LoadFromAssemblyPath(@$"{Environment.CurrentDirectory}\Modules\Modular.HrModule.dll");
        // var assembly = .LoadFromAssemblyName(new AssemblyName($"Modular.HrModule"));
        // if (assembly != null)
            // ModuleAssemblies.Add(assembly);
    }


}