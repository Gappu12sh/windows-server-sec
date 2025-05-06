using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{   
    [Table("app.Representatives")]
    public class Representatives
    {
        [Key]
        public int Rep_ID { get; set; }
        public int Rep_Unique_ID { get; set; }
        public string Rep_Name { get; set; }
        public bool IsActive { get; set; }
        public int? Rep_Created_By { get; set; }
        public int? Rep_Updated_By { get; set; }
        public DateTime? Rep_DOE { get; set; }       
        public DateTime? Rep_DOU { get; set; }
        public string Rep_Code { get; set; }

        [ForeignKey("Rep_Created_By")]
        public UserDetails UserDetailsDOE { get; set; }
        [ForeignKey("Rep_Updated_By")]
        public UserDetails UserDetailsDOU { get; set; }
    }
}
