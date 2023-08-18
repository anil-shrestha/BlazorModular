using Modular.Shared;

namespace Modular.FinanceModule;

public class RegisterModule: IModule
{
    public string ModuleName  =>"Finance";
    public string Icon => "oi oi-calculator";
    public List<NavMenuItem> NavMenuItems => new List<NavMenuItem>{
        new("Home", "fin", "oi oi-home"),

    };
}