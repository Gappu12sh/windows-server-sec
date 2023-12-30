using AutoMapper;
using Customer.Data.Application;
using Customer.Model;
using Customer.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Customer.BusinessLogic
{
    public class BaseManager
    {
        protected IUnitOfWork _unitOfWork;

        public BaseManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public CurrentUser GetCurrentUser()
        {
            int loggedInUser = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            //var userId = _unitOfWork.UserDetails.FindAll().FirstOrDefault(x => x.UserID == loggedInUser).UserID;
            var currentUser = new CurrentUser
            {
                //CurrentUserId = userId,
                CurrentUserId = loggedInUser,
            };
            return currentUser;
        }
        public bool IsUserAllowed(string controllerName, int userId, string methodType)
        {
            var entity = new UserModulePermission();
            entity = _unitOfWork.UserModulePermission.FindAll()
                .Include(x => x.module)
                .Include(x => x.Permissions)
                .Include(x => x.Permissions.Select(y => y.Actions))
                .FirstOrDefault(x => x.UserId == userId && x.module.ModuleName == controllerName && x.IsActive);
            var checkPermission = new List<Data.Application.UserPermission>();
            switch (methodType)
            {
                case "Add":
                    checkPermission = entity?.Permissions?.Where(x => x.Actions.Code == Constants.LookupCodes.Add && x.IsActive).ToList();
                    if (!entity.IsModule || !checkPermission.Any())
                    {
                        return false;
                    }
                    break;
                case "Edit":
                    checkPermission = entity?.Permissions?.Where(x => x.Actions.Code == Constants.LookupCodes.Edit && x.IsActive).ToList();
                    if (!entity.IsModule || !checkPermission.Any())
                    {
                        return false;
                    }
                    break;
                case "Delete":
                    checkPermission = entity?.Permissions?.Where(x => x.Actions.Code == Constants.LookupCodes.Delete && x.IsActive).ToList();
                    if (!entity.IsModule || !checkPermission.Any())
                    {
                        return false;
                    }
                    break;
                case "View":
                    checkPermission = entity?.Permissions?.Where(x => x.Actions.Code == Constants.LookupCodes.View && x.IsActive).ToList();
                    if (!entity.IsModule || !checkPermission.Any())
                    {
                        return false;
                    }
                    break;
                case "ViewParty":
                    checkPermission = entity?.Permissions?.Where(x => x.Actions.Code == Constants.LookupCodes.ViewParty && x.IsActive).ToList();
                    if (!entity.IsModule || !checkPermission.Any())
                    {
                        return false;
                    }
                    break;
                case "AddRate":
                    checkPermission = entity?.Permissions?.Where(x => x.Actions.Code == Constants.LookupCodes.AddRate && x.IsActive).ToList();
                    if (!entity.IsModule || !checkPermission.Any())
                    {
                        return false;
                    }
                    break;
                case "AddAddress":
                    checkPermission = entity?.Permissions?.Where(x => x.Actions.Code == Constants.LookupCodes.AddAddress && x.IsActive).ToList();
                    if (!entity.IsModule || !checkPermission.Any())
                    {
                        return false;
                    }
                    break;
            }     
            return true;
        }
    }
}
