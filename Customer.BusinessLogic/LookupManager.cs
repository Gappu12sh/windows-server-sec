using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Customer.Model;
using Customer.Model.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Customer.BusinessLogic
{
    public class LookupManager : BaseManager, ILookup
    {
        public LookupManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults<List<LookupModel>> GetLookup()
        {
            var entity = _unitOfWork.Lookup.FindAll().Where(x => x.IsActive);
            var mapModel = Mapper.Map<List<LookupModel>>(entity);
            return new ResponseResults<List<LookupModel>>(mapModel);
        }

        public ResponseResults<List<LookupModel>> GetLookupByCode(string code)
        {
            var entity = _unitOfWork.Lookup.FindAll().Where(x => x.Code == code && x.IsActive);
            var mapModel = Mapper.Map<List<LookupModel>>(entity);
            return new ResponseResults<List<LookupModel>>(mapModel);
        }

        public ResponseResults<List<LookupModel>> GetLookupByType(string type)
        {
            var entity = _unitOfWork.Lookup.FindAll().Where(x => x.Type == type && x.IsActive);
            var mapModel = Mapper.Map<List<LookupModel>>(entity);
            return new ResponseResults<List<LookupModel>>(mapModel);
        }

        
        //public ResponseResults CreateLookup(LookupModel model)
        //{
        //    var currentUser = GetCurrentUser();

        //    // Check permission
        //    if (!IsUserAllowed(Constants.Controllers.Contact, currentUser.CurrentUserId, Constants.ControllersMethod.Add))
        //    {
        //        return new ResponseResults(ErrorCodes.AccessTokenInvalid)
        //        {
        //            ErrorDescription = "E001"
        //        };
        //    }

        //    // Normalize the code and type for duplicate check
        //    string Normalize(string input) => input?.ToUpper().Replace(" ", "").Replace("-", "") ?? "";

        //    string normalizedCode = Normalize(model.Code);
        //    string normalizedType = Normalize(model.Type);

        //    // Check for duplicate lookup (case-insensitive, ignores spaces and hyphens)
        //    bool lookupExists = _unitOfWork.Lookup.FindAll()
        //        .Any(x => Normalize(x.Code) == normalizedCode
        //                  && Normalize(x.Type) == normalizedType
        //                  && x.IsActive);

        //    if (lookupExists)
        //    {
        //        return new ResponseResults(ErrorCodes.RecordAlreadyExists)
        //        {
        //            ErrorDescription = "E005"
        //        };
        //    }

        //    try
        //    {
        //        // Insert new lookup record
        //        _unitOfWork.Lookup.Insert(new Lookup
        //        {
        //            Code = model.Code.Trim(),
        //            Type = model.Type.Trim(),
        //            Description = model.Description?.Trim(),
        //            IsActive = true,
        //        });

        //        _unitOfWork.Save();
        //    }
        //    catch (Exception)
        //    {
        //        return new ResponseResults(ErrorCodes.CreateRecordFailed)
        //        {
        //            ErrorDescription = "E003"
        //        };
        //    }

        //    // Success case
        //    return new ResponseResults();
        //}



    }
}
