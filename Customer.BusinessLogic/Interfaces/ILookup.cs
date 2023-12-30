using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface ILookup
    {
        ResponseResults<List<LookupModel>> GetLookup();
        ResponseResults<List<LookupModel>> GetLookupByCode(string code);
        ResponseResults<List<LookupModel>> GetLookupByType(string type);
    }
}
