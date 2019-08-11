using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Modular.Core.Domain.Models;

namespace Modular.Modules.ModuleC.Models
{
    public class TestModels : Entity
    {

        public new long Id { get; set; }

        public string Text { get; set; }

    }
}
