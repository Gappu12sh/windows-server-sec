using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Customer.Model;
using Customer.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic
{
    public class ApplicationUsageManager : BaseManager, IApplicationUsage
    {
        public ApplicationUsageManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public ResponseResults CreateApplicationUsage(ApplicationUsageModel model)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.ApplicationUsage, currentUser.CurrentUserId, Constants.ControllersMethod.Add);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var applicationUsage = _unitOfWork.ApplicationUsage.FindAll().Where(x => x.ApplicationUsage_Name.ToUpper().Trim() == model.ApplicationUsage_Name.ToUpper().Trim() && x.IsActive).ToList();
            if (applicationUsage.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var entity = Mapper.Map<ApplicationUsage>(model);
            entity.ApplicationUsage_DOE = DateTime.Now;
            entity.ApplicationUsage_Created_By = currentUser.CurrentUserId;
            _unitOfWork.ApplicationUsage.Insert(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteApplicationUsage(int id)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.ApplicationUsage, currentUser.CurrentUserId, Constants.ControllersMethod.Delete);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var entity = _unitOfWork.ApplicationUsage.Get(id);
            _unitOfWork.ApplicationUsage.SoftDelete(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults<ApplicationUsageModel> GetApplicationUsageById(int Id)
        {
            var entityApplicationUsage = _unitOfWork.ApplicationUsage.FindAll()
                .Include(x => x.UserDetailsCreatedBy).Include(x => x.UserDetailsUpdatedBy).FirstOrDefault(x => x.ApplicationUsage_Id == Id);
            var mapModel = Mapper.Map<ApplicationUsageModel>(entityApplicationUsage);

            return new ResponseResults<ApplicationUsageModel>(mapModel);
        }

        public ResponseResults<List<ApplicationUsageModel>> GetApplicationUsage()
        {
            var entityApplicationUsage = _unitOfWork.ApplicationUsage.FindAll()
                .Include(x => x.UserDetailsCreatedBy)
                .Include(x => x.UserDetailsUpdatedBy)
                .Where(x => x.IsActive)
                .ToList();
            var mapModel = Mapper.Map<List<ApplicationUsageModel>>(entityApplicationUsage.OrderByDescending(x => x.ApplicationUsage_Name).Reverse());

            return new ResponseResults<List<ApplicationUsageModel>>(mapModel);
        }

        public ResponseResults UpdateApplicationUsage(ApplicationUsageModel model)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.ApplicationUsage, currentUser.CurrentUserId, Constants.ControllersMethod.Edit);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var entityApplicationUsage = _unitOfWork.ApplicationUsage.FindAll().Where(x => x.ApplicationUsage_Name.ToUpper().Trim() == model.ApplicationUsage_Name.ToUpper().Trim()
            && x.ApplicationUsage_Id != model.ApplicationUsage_Id && x.IsActive).ToList();
            if (entityApplicationUsage.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var applicationUsage = _unitOfWork.ApplicationUsage.Get(model.ApplicationUsage_Id);
            applicationUsage.ApplicationUsage_Name = model.ApplicationUsage_Name.Trim();
            applicationUsage.IsActive = true;
            applicationUsage.ApplicationUsage_Updated_By = model.ApplicationUsage_Updated_By;
            applicationUsage.ApplicationUsage_DOU = DateTime.Now;
            applicationUsage.ApplicationUsage_Updated_By = currentUser.CurrentUserId;
            _unitOfWork.Save();

            return new ResponseResults();
        }
    }
}
