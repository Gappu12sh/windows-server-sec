using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.TermCondition")]
    public class TermCondition
    {
        [Key]
        public int TermCondition_Id { get; set; }
        public string Term { get; set; }
    }
}
