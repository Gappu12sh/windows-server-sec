using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Customer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic
{
    public class UserMdulePermissionManager : BaseManager, IUserModulePermission
    {
        public UserMdulePermissionManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults<List<UserModulePermissionModel>> CreatePermission(UserModulePermissionModel model)
        {
            var userId = model.UserId;
            var entity = _unitOfWork.UserModulePermission.FindAll()
                .Include(x => x.Permissions)
                .FirstOrDefault(x => x.UserId == userId && x.ModuleId == model.ModuleId && x.IsActive);
            //entity.ForEach(x => x.IsActive = false);
            if (entity != null)
            {
                entity.IsModule=model.IsModule;
                var permission = _unitOfWork.UserPermission.FindAll().Where(x => x.UserModulePermissionId == entity.UserModulePermissionId).ToList();
                permission.ForEach(x => x.IsActive = false);
                foreach (var item in model.userPermissionModels)
                {
                    var checkPerm = permission.FirstOrDefault(x => x.ActionId == item.ActionId);
                    if (checkPerm != null)
                    {
                        checkPerm.IsActive = true;

                    }
                    else
                    {
                        var userPerm = new Data.Application.UserPermission
                        {
                            UserModulePermissionId = entity.UserModulePermissionId,
                            ActionId = item.ActionId,
                            IsActive = true,
                        };
                        _unitOfWork.UserPermission.Insert(userPerm);
                    }
                }

                _unitOfWork.Save();
            }
            else
            {
                var userModulePerm = new UserModulePermission
                {
                    ModuleId = model.ModuleId,
                    UserId = model.UserId,
                    IsModule = model.IsModule,
                    Permissions = Mapper.Map<List<Data.Application.UserPermission>>(model.userPermissionModels)
                };
                _unitOfWork.UserModulePermission.Insert(userModulePerm);
                _unitOfWork.Save();
            }
            var response = GetPermissionByUserId(userId.Value);
            return response;
        }

        public ResponseResults DeletePermissions(int userId)
        {
            var entity = _unitOfWork.UserModulePermission.FindAll().Where(x => x.UserId == userId).ToList();
            entity.ForEach(x => x.IsActive = false);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults<List<ModulesModel>> GetModules()
        {
            var modules = _unitOfWork.Modules.FindAll()
                .Include(x => x.ModuleActions)
                .Where(x => x.IsActive).ToList();
            var mapModel = Mapper.Map<List<ModulesModel>>(modules);
            return new ResponseResults<List<ModulesModel>>(mapModel);
        }

        public ResponseResults<List<UserModulePermissionModel>> GetPermissionByUserId(int userId)
        {
            var entity = _unitOfWork.UserModulePermission.FindAll()
                .Include(x => x.module)
                .Include(x => x.Permissions)
                .Where(x => x.UserId == userId && x.IsActive).ToList();
            var mapModel = Mapper.Map<List<UserModulePermissionModel>>(entity);
            return new ResponseResults<List<UserModulePermissionModel>>(mapModel);
        }
        public ResponseResults<List<UserModulePermissionModel>> GetModuleByUserId(int userId)
        {
            var entity = _unitOfWork.UserModulePermission.FindAll()
                .Include(x => x.module)
                .Include(x => x.Permissions)
                .Include(x => x.Permissions.Select(y => y.Actions))
                .Where(x => x.UserId == userId && x.IsActive && x.IsModule).ToList();
            var mapModel = Mapper.Map<List<UserModulePermissionModel>>(entity);
            return new ResponseResults<List<UserModulePermissionModel>>(mapModel);
        }

        public ResponseResults<List<UserModulePermissionModel>> GetPermissions()
        {
            var permissions = _unitOfWork.UserModulePermission.FindAll()
                .Include(x => x.user)
                .Include(x => x.module)
                .Where(x => x.IsActive).ToList();
            var mapModel = Mapper.Map<List<UserModulePermissionModel>>(permissions);
            return new ResponseResults<List<UserModulePermissionModel>>(mapModel);
        }

        public ResponseResults UpdatePermission(List<UserModulePermissionModel> model)
        {
            var entity = _unitOfWork.UserModulePermission.FindAll().Where(x => x.UserId == model.FirstOrDefault().UserId).ToList();
            entity.ForEach(x => x.IsActive = false);
            foreach (var item in model)
            {
                if (item.UserModulePermissionId == 0)
                {
                    var mapEntity = Mapper.Map<UserModulePermission>(item);
                    _unitOfWork.UserModulePermission.Insert(mapEntity);

                }
                else
                {
                    var record = entity.FirstOrDefault(x => x.UserModulePermissionId == item.UserModulePermissionId);
                    record.ModuleId = item.ModuleId;
                    record.UserId = item.UserId;
                    record.IsModule = item.IsModule;
                }
                _unitOfWork.Save();
            }
            return new ResponseResults();
        }
    }
}
