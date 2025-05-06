using Customer.Data.Application;
using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IQuotation
    {
        ResponseResults CreateQuotation(QuotationMasterModel model);
        ResponseResults UpdateQuotation(QuotationMasterModel model);
        ResponseResults<List<QuotationMasterModel>> GetQuotationList();
        ResponseResults<QuotationMasterModel> GetQuotationById(int quotationId);
        ResponseResults<List<QuotationMasterModel>> GetQuotationListByParty(int partyId);
        ResponseResults<QuotationMasterModel> GetQuotationBySampleName(string sampleName,int partyId);

    }
}
