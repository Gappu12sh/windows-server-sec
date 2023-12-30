using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IUserModulePermission
    {
        ResponseResults<List<UserModulePermissionModel>> CreatePermission(UserModulePermissionModel model);
        ResponseResults UpdatePermission(List<UserModulePermissionModel> model);
        ResponseResults<List<UserModulePermissionModel>> GetPermissionByUserId(int userId );
        ResponseResults<List<UserModulePermissionModel>> GetPermissions();
        ResponseResults<List<ModulesModel>> GetModules();
        ResponseResults DeletePermissions(int userId);
        ResponseResults<List<UserModulePermissionModel>> GetModuleByUserId(int userId);
    }
}
