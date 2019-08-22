using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using Modular.Core.Models;

namespace Modular.Modules.Core.Models
{
    public class UserDocuments : EntityBase
    {
        public long DocumentId { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset LatestUpdatedOn { get; set; }

        [StringLength(20)]
        public string Type { get; set; }

        [StringLength(150)]
        public string Path { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("FK_UserId")]
        public long UserId { get; set; }

        public User User { get; set; }

        private List<UserDocuments> _ListDocuments { get; set; }

        [NotMapped]
        public List<UserDocuments> ListDocuments
        {
            get
            {
                return _ListDocuments.Where(x => x.UserId == UserId).ToList();
            }
            set
            {
                _ListDocuments = value;
            }
        }

    }
}