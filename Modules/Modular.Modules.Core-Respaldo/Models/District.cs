﻿using Modular.Core.Models;
using Modular.Module.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Modular.Modules.Core.Models
{
    public class District : EntityBase
    {
        public District() { }

        public District(long id)
        {
            Id = id;
        }

        public long StateOrProvinceId { get; set; }

        public StateOrProvince StateOrProvince { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string Name { get; set; }

        [StringLength(450)]
        public string Type { get; set; }

        public string Location { get; set; }
    }
}
