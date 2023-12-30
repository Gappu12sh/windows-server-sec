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
    public class RepresentativesManager : BaseManager, IRepresentatives
    {
        public RepresentativesManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults CreateRepresentative(RepresentativesModel model)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Representatives, currentUser.CurrentUserId, Constants.ControllersMethod.Add);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var rep = _unitOfWork.Representatives.FindAll().Where(x => x.Rep_Name.ToUpper().Trim() == model.Rep_Name.ToUpper().Trim() && x.IsActive).ToList();
            if (rep.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var mapEntity = new Representatives()
            {
                Rep_Name = char.ToUpper(model.Rep_Name.Trim().First()) + model.Rep_Name.Trim().Substring(1).ToLower(),
                Rep_DOE = DateTime.Now,
                Rep_Created_By = currentUser.CurrentUserId
            };
            _unitOfWork.Representatives.Insert(mapEntity);
            _unitOfWork.Save();
            mapEntity.Rep_Unique_ID = mapEntity.Rep_ID;
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults<RepresentativesModel> GetRepresentativeById(int Id)
        {
            var repEntity = _unitOfWork.Representatives.FindAll()
                .Include(x => x.UserDetailsDOE)
                .FirstOrDefault(x => x.Rep_ID == Id && x.IsActive);

            var mapRepModel = Mapper.Map<RepresentativesModel>(repEntity);
            return new ResponseResults<RepresentativesModel>(mapRepModel);
        }

        public ResponseResults<List<RepresentativesModel>> GetRepresentatives()
        {
            var repEntity = _unitOfWork.Representatives.FindAll()
                 .Include(x => x.UserDetailsDOE).Where(x => x.IsActive).ToList();

            var mapRepModel = Mapper.Map<List<RepresentativesModel>>(repEntity.OrderByDescending(x => x.Rep_Name).Reverse());
            return new ResponseResults<List<RepresentativesModel>>(mapRepModel);
        }

        public ResponseResults UpdateRepresentative(RepresentativesModel model)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Representatives, currentUser.CurrentUserId, Constants.ControllersMethod.Edit);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var rep = _unitOfWork.Representatives.FindAll().Where(x => x.Rep_Name.ToUpper().Trim() == model.Rep_Name.ToUpper().Trim()
           && x.Rep_ID != model.Rep_ID && x.IsActive).ToList();
            if (rep.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var entity = _unitOfWork.Representatives.FindAll().FirstOrDefault(x => x.Rep_ID == model.Rep_ID);
            entity.Rep_Name = char.ToUpper(model.Rep_Name.Trim().First()) + model.Rep_Name.Trim().Substring(1).ToLower();
            entity.Rep_DOU = DateTime.Now;
            entity.Rep_Updated_By = currentUser.CurrentUserId;
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteRepresentative(int id)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Representatives, currentUser.CurrentUserId, Constants.ControllersMethod.Delete);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var repEntity = _unitOfWork.Representatives.FindAll().FirstOrDefault(x => x.Rep_ID == id && x.IsActive);
            _unitOfWork.Representatives.SoftDelete(repEntity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

       
    }
}
