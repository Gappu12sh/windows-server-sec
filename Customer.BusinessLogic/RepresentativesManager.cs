using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.BusinessLogic.Utilities;
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

        //public ResponseResults CreateRepresentative(RepresentativesModel model)
        //{
        //    var currentUser = GetCurrentUser();
        //    var res = IsUserAllowed(Constants.Controllers.Representatives, currentUser.CurrentUserId, Constants.ControllersMethod.Add);
        //    if (!res)
        //    {
        //        return new ResponseResults(ErrorCodes.InValidRequest);
        //    }
        //    var rep = _unitOfWork.Representatives.FindAll().Where(x => x.Rep_Name.ToUpper().Trim() == model.Rep_Name.ToUpper().Trim() && x.IsActive).ToList();
        //    if (rep.Count > 0)
        //    {
        //        return new ResponseResults(ErrorCodes.RecordAlreadyExists);
        //    }
        //    var mapEntity = new Representatives()
        //    {
        //        Rep_Name = Common.ToTitleCase(model.Rep_Name),
        //        Rep_DOE = DateTime.Now,
        //        Rep_Created_By = currentUser.CurrentUserId
        //    };
        //    _unitOfWork.Representatives.Insert(mapEntity);
        //    _unitOfWork.Save();
        //    mapEntity.Rep_Unique_ID = mapEntity.Rep_ID;
        //    _unitOfWork.Save();
        //    return new ResponseResults();
        //}





        public ResponseResults CreateRepresentative(RepresentativesModel model)
        {
            var currentUser = GetCurrentUser();

            // Check if the user is allowed to create a representative
            var res = IsUserAllowed(Constants.Controllers.Representatives, currentUser.CurrentUserId, Constants.ControllersMethod.Add);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }

            // Check if a representative with the same name and Rep_Code already exists (to ensure uniqueness)
            var rep = _unitOfWork.Representatives.FindAll()
                .Where(x => x.Rep_Name.ToUpper().Trim() == model.Rep_Name.ToUpper().Trim()
                            && x.Rep_Code.ToUpper().Trim() == model.Rep_Code.ToUpper().Trim() // Check Rep_Code for uniqueness
                            && x.IsActive)
                .ToList();

            if (rep.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }

            // Map the incoming model to the entity
            var mapEntity = new Representatives()
            {
                Rep_Name = Common.ToTitleCase(model.Rep_Name),  // Format the name properly
                Rep_Code = model.Rep_Code,                      // Set Rep_Code
                Rep_DOE = DateTime.Now,                          // Date of Entry (now)
                Rep_Created_By = currentUser.CurrentUserId      // Set the current user as the creator
            };

            // Insert the representative into the database
            _unitOfWork.Representatives.Insert(mapEntity);
            _unitOfWork.Save();

            // Optionally set the unique ID for the representative
            mapEntity.Rep_Unique_ID = mapEntity.Rep_ID;  // Rep_Unique_ID is set to the Rep_ID (auto-generated)
            _unitOfWork.Save();

            return new ResponseResults();  // Return a success result
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

        //public ResponseResults UpdateRepresentative(RepresentativesModel model)
        //{
        //    var currentUser = GetCurrentUser();
        //    var res = IsUserAllowed(Constants.Controllers.Representatives, currentUser.CurrentUserId, Constants.ControllersMethod.Edit);
        //    if (!res)
        //    {
        //        return new ResponseResults(ErrorCodes.InValidRequest);
        //    }
        //    var rep = _unitOfWork.Representatives.FindAll().Where(x => x.Rep_Name.ToUpper().Trim() == model.Rep_Name.ToUpper().Trim()
        //   && x.Rep_ID != model.Rep_ID && x.IsActive).ToList();
        //    if (rep.Count > 0)
        //    {
        //        return new ResponseResults(ErrorCodes.RecordAlreadyExists);
        //    }
        //    var entity = _unitOfWork.Representatives.FindAll().FirstOrDefault(x => x.Rep_ID == model.Rep_ID);
        //    entity.Rep_Name = Common.ToTitleCase(model.Rep_Name);
        //    entity.Rep_DOU = DateTime.Now;
        //    entity.Rep_Updated_By = currentUser.CurrentUserId;
        //    _unitOfWork.Save();
        //    return new ResponseResults();
        //}





        public ResponseResults UpdateRepresentative(RepresentativesModel model)
        {
            var currentUser = GetCurrentUser();

            // Check if the user has permission to edit representatives
            var res = IsUserAllowed(Constants.Controllers.Representatives, currentUser.CurrentUserId, Constants.ControllersMethod.Edit);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }

            // Check if a representative with the same Rep_Name and Rep_Code already exists (excluding the current one being updated)
            var rep = _unitOfWork.Representatives.FindAll()
                .Where(x => x.Rep_Name.ToUpper().Trim() == model.Rep_Name.ToUpper().Trim()
                            && x.Rep_Code.ToUpper().Trim() == model.Rep_Code.ToUpper().Trim()  // Ensure Rep_Code is unique
                            && x.Rep_ID != model.Rep_ID && x.IsActive)  // Exclude the current representative
                .ToList();

            if (rep.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);  // Rep_Name and Rep_Code combination already exists
            }

            // Retrieve the representative entity to be updated
            var entity = _unitOfWork.Representatives.FindAll()
                .FirstOrDefault(x => x.Rep_ID == model.Rep_ID);

            if (entity != null)
            {
                // Update the fields
                entity.Rep_Name = Common.ToTitleCase(model.Rep_Name);  // Format the name properly
                entity.Rep_Code = model.Rep_Code;                      // Update Rep_Code
                entity.Rep_DOU = DateTime.Now;                          // Set Rep_DOU to current DateTime (Date of Update)
                entity.Rep_Updated_By = currentUser.CurrentUserId;     // Set the current user as the updater

                // Save the changes
                _unitOfWork.Save();
            }

            return new ResponseResults();  // Return a success result
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
            repEntity.Rep_DOU = DateTime.Now;
            repEntity.Rep_Updated_By = currentUser.CurrentUserId;
            _unitOfWork.Representatives.SoftDelete(repEntity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

       
    }
}
