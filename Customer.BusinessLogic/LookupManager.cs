using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Customer.Model;
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
    }
}
