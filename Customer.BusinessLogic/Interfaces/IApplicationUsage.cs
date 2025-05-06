using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IApplicationUsage
    {
        ResponseResults CreateApplicationUsage(ApplicationUsageModel model);
        ResponseResults UpdateApplicationUsage(ApplicationUsageModel model);
        ResponseResults<List<ApplicationUsageModel>> GetApplicationUsage();
        ResponseResults<List<ApplicationUsageModel>> GetApplicationUsageInactive();
        ResponseResults<ApplicationUsageModel> GetApplicationUsageById(int Id);
        ResponseResults DeleteApplicationUsage(int id);
    }
}
