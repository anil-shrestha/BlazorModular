using System.Collections.Generic;
using Modular.Shared;

namespace Modular.HrModule;

public class RegisterModule: IModule
{
    public string ModuleName  =>"Human Resource";
    public string Icon => "oi oi-person";
    public List<NavMenuItem> NavMenuItems => new List<NavMenuItem>{
        new("Home", "hr", "oi oi-home"),
        new("Add Employee", "hr/AddEmployee", "oi oi-plus"),

    };
}