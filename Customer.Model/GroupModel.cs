using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class GroupModel
    {
        public int GroupId { get; set; }
        public string Group_Name { get; set; }
        public bool IsActive { get; set; }
        public int? Group_Created_By { get; set; }
        public DateTime? Group_DOE { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
    }
}
