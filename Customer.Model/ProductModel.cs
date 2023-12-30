using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class ProductModel
    {
        public int Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Price { get; set; }
        public DateTime Product_UpdateOn { get; set; }
        public int FinancialYear_Id { get; set; }
        public string Product_Group { get; set; }
        public bool IsActive { get; set; }
        public int? Product_Created_By { get; set; }
        public int? Product_Updated_By { get; set; }
        public DateTime? Product_DOE { get; set; }
        public DateTime? Product_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public FinancialYearModel ProductFinancialYear { get; set; }
        public List<ProductApplicationUsageModel> ProductApplicationUsages { get; set; } = new List<ProductApplicationUsageModel>();
    }
}
