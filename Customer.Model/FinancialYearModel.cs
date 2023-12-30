using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class FinancialYearModel
    {
        public int FinancialYear_Id { get; set; }
        public string Financial_Year { get; set; }
        public bool IsActive { get; set; }
        public int? FinancialYear_Created_By { get; set; }
        public int? FinancialYear_Updated_By { get; set; }
        public DateTime? FinancialYear_DOE { get; set; }
        public DateTime? FinancialYear_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
    }
}
