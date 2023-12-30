using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IRepresentatives
    {
        ResponseResults CreateRepresentative(RepresentativesModel model);
        ResponseResults UpdateRepresentative(RepresentativesModel model);
        ResponseResults<List<RepresentativesModel>> GetRepresentatives();
        ResponseResults<RepresentativesModel> GetRepresentativeById(int Id);
        ResponseResults DeleteRepresentative(int id);
    }
}
