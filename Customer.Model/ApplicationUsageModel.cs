using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class ApplicationUsageModel
    {
        public int ApplicationUsage_Id { get; set; }
        public string ApplicationUsage_Name { get; set; }
        public bool IsActive { get; set; }
        public int? ApplicationUsage_Created_By { get; set; }
        public int? ApplicationUsage_Updated_By { get; set; }
        public DateTime? ApplicationUsage_DOE { get; set; }
        public DateTime? ApplicationUsage_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
    }
}
