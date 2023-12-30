using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.Product")]
    public class Product
    {
        [Key]
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

        [ForeignKey("Product_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("Product_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("FinancialYear_Id")]
        public FinancialYear ProductFinancialYear { get; set; }
        public virtual ICollection<ProductApplicationUsage> ProductApplicationUsages { get; set; }=new List<ProductApplicationUsage>();
    }
}
