using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class ProductApplicationUsageModel
    {
        public int ProductApplicationUsage_Id { get; set; }
        public int Product_Id { get; set; }
        public int ApplicationUsage_Id { get; set; }
        public string Product_Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? FinancialYear_DOE { get; set; }
        public ApplicationUsageModel ApplicationUsage { get; set; }
    }
}
