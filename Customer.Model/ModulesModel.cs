using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class ModulesModel
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleUrl { get; set; }
        public bool IsActive { get; set; }
        public List<LookupModel> ModuleActions { get; set; } = new List<LookupModel>();
    }
}
