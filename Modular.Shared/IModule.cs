using System.Collections.Generic;

namespace Modular.Shared;

public interface IModule
{
    string ModuleName { get; }

    string Icon {get;}

    List<NavMenuItem> NavMenuItems {get;}
}

public record NavMenuItem (string DisplayName, string Url, string Icon);