using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IFinancialYear
    {
        ResponseResults CreateFinancialYear(FinancialYearModel model);
        ResponseResults UpdateFinancialYear(FinancialYearModel model);
        ResponseResults<List<FinancialYearModel>> GetFinancialYear();
        ResponseResults<FinancialYearModel> GetFinancialYearById(int Id);
        ResponseResults DeleteFinancialYear(int id);
    }
}
