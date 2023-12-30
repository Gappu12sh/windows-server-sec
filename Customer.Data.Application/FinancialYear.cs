using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.FinancialYear")]
    public class FinancialYear
    {
        [Key]
        public int FinancialYear_Id { get; set; }
        public string Financial_Year { get; set; }
        public bool IsActive { get; set; }
        public int? FinancialYear_Created_By { get; set; }
        public int? FinancialYear_Updated_By { get; set; }
        public DateTime? FinancialYear_DOE { get; set; }
        public DateTime? FinancialYear_DOU { get; set; }

        [ForeignKey("FinancialYear_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("FinancialYear_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
    }
}
