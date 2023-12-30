using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.MaterialRates")]
    public class MaterialRates
    {
        [Key]
        public int MaterialRateId { get; set; }
        public int MaterialId { get; set; }
        public decimal Rate { get; set; }
        public decimal OldRate { get; set; }
        public int? CFY_Id { get; set; }
        public int? MaterialRate_Created_By { get; set; }
        public DateTime? WEF { get; set; }
        public DateTime? MaterialRate_DOE { get; set; }
        [ForeignKey("MaterialRate_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("CFY_Id")]
        public FinancialYear financialYear { get; set; }
    }
}
