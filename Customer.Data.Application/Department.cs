using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.DepartmentMaster")]
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public bool IsActive { get; set; }
        public int? Department_Created_By { get; set; }
        public int? Department_Updated_By { get; set; }
        public DateTime? Department_DOE { get; set; }
        public DateTime? Department_DOU { get; set; }

        [ForeignKey("Department_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("Department_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
    }
}
