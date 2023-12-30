using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IDepartment
    {
        ResponseResults CreateDepartment(DepartmentModel department);
        ResponseResults UpdateDepartment(DepartmentModel department);
        ResponseResults<List<DepartmentModel>> GetDepartments();
        ResponseResults<DepartmentModel> GetDepartmentById(int Id);
        ResponseResults DeleteDepartment(int id);
    }
}
