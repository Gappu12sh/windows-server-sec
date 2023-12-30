using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class MaterialRatesModel
    {
        public int MaterialRateId { get; set; }
        public int MaterialId { get; set; }
        public decimal Rate { get; set; }
        public decimal OldRate { get; set; }
        public int? CFY_Id { get; set; }
        public int? MaterialRate_Created_By { get; set; }
        public DateTime? WEF { get; set; }
        public DateTime? MaterialRate_DOE { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public FinancialYearModel financialYear { get; set; }
    }
}
