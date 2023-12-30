using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.Modules")]
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string ModuleUrl { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Lookup> ModuleActions { get; set;}=new List<Lookup>();
    }
}
