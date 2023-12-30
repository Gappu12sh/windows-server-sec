using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.Group")]
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string Group_Name { get; set; }
        public bool IsActive { get; set; }
        public int? Group_Created_By { get; set; }
        public DateTime? Group_DOE { get; set; }

        [ForeignKey("Group_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
    }
}
