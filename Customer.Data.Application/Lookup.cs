using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.Lookup")]
    public class Lookup
    {
        [Key]
        public int LookupId { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int? LookupParentId { get; set; }
        public ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}
