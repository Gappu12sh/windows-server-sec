using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.ApplicationUsage")]
    public class ApplicationUsage
    {
        [Key]
        public int ApplicationUsage_Id { get; set; }
        public string ApplicationUsage_Name { get; set; }
        public bool IsActive { get; set; }
        public int? ApplicationUsage_Created_By { get; set; }
        public int? ApplicationUsage_Updated_By { get; set; }
        public DateTime? ApplicationUsage_DOE { get; set; }
        public DateTime? ApplicationUsage_DOU { get; set; }

        [ForeignKey("ApplicationUsage_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("ApplicationUsage_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
    }
}
