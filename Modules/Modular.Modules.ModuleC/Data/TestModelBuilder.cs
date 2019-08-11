using System;
using Microsoft.EntityFrameworkCore;
using Modular.Core;
using Modular.Core.Data;
using Modular.Modules.ModuleC.Models;

namespace Modular.Modules.ModuleC.Data
{
    public class TestModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestModels>();
        }
    }
}
