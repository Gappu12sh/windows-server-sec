using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.ProductApplicationUsage")]
    public class ProductApplicationUsage
    {
        [Key]
        public int ProductApplicationUsage_Id { get; set; }
        public int Product_Id { get; set; }
        public int ApplicationUsage_Id { get; set; }
        public string Product_Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? FinancialYear_DOE { get; set; }

        [ForeignKey("ApplicationUsage_Id")]
        public ApplicationUsage ApplicationUsage { get; set; }
    }
}
