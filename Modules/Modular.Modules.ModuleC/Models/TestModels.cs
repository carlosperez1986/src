using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Modular.Core.Domain.Models;
using Modular.Core.Models;

namespace Modular.Modules.ModuleC.Models
{
	public class TestModels : EntityBase
	{

		public long IdTest { get; set; }

		public string Text { get; set; }

	}
}
