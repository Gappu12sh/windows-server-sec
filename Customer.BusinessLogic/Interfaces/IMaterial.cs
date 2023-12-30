using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IMaterial
    {
        ResponseResults CreateMaterial(MaterialModel model);
        ResponseResults UpdateMaterial(MaterialModel model);
        ResponseResults<List<MaterialModel>> GetMaterials();
        ResponseResults<MaterialModel> GetMaterialById(int Id);
        ResponseResults DeleteMaterial(int id);
        ResponseResults AddNewRate(MaterialRatesModel model);
    }
}
