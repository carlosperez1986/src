﻿using System;
using System.Reflection;

namespace Modular.Core
{
    public class ModuleInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsBundledWithHost { get; set; }

        public Version Version { get; set; }

        public Assembly Assembly { get; set; }

        public string Path { get; set; }
    }
}
