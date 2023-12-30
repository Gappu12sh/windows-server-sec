using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class DepartmentModel
    {
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public bool IsActive { get; set; }
        public int Department_Created_By { get; set; }
        public int Department_Updated_By { get; set; }
        public DateTime Department_DOE { get; set; }
        public DateTime Department_DOU { get; set; }
        public UserModel UserDetailsCreatedBy { get; set; }
        public UserModel UserDetailsUpdatedBy { get; set; }
    }
}
