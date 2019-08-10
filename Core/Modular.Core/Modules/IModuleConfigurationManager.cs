using System.Collections.Generic;

namespace Modular.Core
{
    public interface IModuleConfigurationManager
    {
        IEnumerable<ModuleInfo> GetModules();
    }
}