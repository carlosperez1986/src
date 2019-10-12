using System.Collections.Generic;
using Modular.Core;
using Modular.Core.Localization;

namespace Modular.Core
{
    public static class GlobalConfiguration
    {
        public static IList<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();

        public static IList<Culture> Cultures { get; set; } = new List<Culture>();

        public static string DefaultCulture => "en-US";

        public static string WebRootPath { get; set; }

        public static string Path { get; set; }

        public static string ContentRootPath { get; set; }
    }
}
