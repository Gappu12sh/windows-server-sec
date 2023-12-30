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
    public class MaterialManager : BaseManager, IMaterial
    {
        public MaterialManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults AddNewRate(MaterialRatesModel model)
        {
            var entity = _unitOfWork.MaterialRates.FindAll().Where(x => x.MaterialId == model.MaterialId).OrderByDescending(x => x.MaterialRateId).ToList();
            var currentUser = GetCurrentUser();
            var mapEntity = Mapper.Map<MaterialRates>(model);
            mapEntity.OldRate = entity.FirstOrDefault().Rate;
            mapEntity.MaterialRate_DOE = DateTime.Now;
            mapEntity.MaterialRate_Created_By= currentUser.CurrentUserId;
            mapEntity.CFY_Id = _unitOfWork.FinancialYear.FindAll().FirstOrDefault(x => x.IsActive).FinancialYear_Id;
            _unitOfWork.MaterialRates.Insert(mapEntity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults CreateMaterial(MaterialModel model)
        {
            var currentUser = GetCurrentUser();
            var mapEntity = Mapper.Map<Material>(model);
            mapEntity.Material_DOE = DateTime.Now;
            mapEntity.Material_Created_By = currentUser.CurrentUserId;
            mapEntity.IsActive = true;
            mapEntity.MaterialRates.FirstOrDefault().MaterialRate_DOE= DateTime.Now;
            mapEntity.MaterialRates.FirstOrDefault().MaterialRate_Created_By = currentUser.CurrentUserId;
            mapEntity.MaterialRates.FirstOrDefault().CFY_Id = _unitOfWork.FinancialYear.FindAll().FirstOrDefault(x => x.IsActive).FinancialYear_Id;
            _unitOfWork.Material.Insert(mapEntity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteMaterial(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseResults<MaterialModel> GetMaterialById(int Id)
        {
            var entity = _unitOfWork.Material.FindAll()
                .Include(x => x.UserDetailsCreatedBy).Include(x => x.UserDetailsUpdatedBy)
                .FirstOrDefault(x => x.MaterialId == Id && x.IsActive);
            var mapModel = Mapper.Map<MaterialModel>(entity);
            return new ResponseResults<MaterialModel>(mapModel);

        }

        public ResponseResults<List<MaterialModel>> GetMaterials()
        {
            var entity = _unitOfWork.Material.FindAll()
                .Include(x=>x.MaterialRates)
                .Include(x => x.MaterialRates.Select(y=>y.financialYear))
                //.Include(x => x.MaterialRates.Select(y => y.MaterialRate_Created_By))
                .Include(x => x.UserDetailsCreatedBy).Include(x => x.UserDetailsUpdatedBy)
                .Where(x => x.IsActive).ToList();
            var mapModel = Mapper.Map<List<MaterialModel>>(entity);
            return new ResponseResults<List<MaterialModel>>(mapModel);
        }

        public ResponseResults UpdateMaterial(MaterialModel model)
        {
           var entity=_unitOfWork.Material.FindAll()
                .Include(x=>x.MaterialRates)
                .Include(x => x.UserDetailsCreatedBy)
                .Include(x => x.UserDetailsUpdatedBy)
                .FirstOrDefault(x=>x.MaterialId == model.MaterialId && x.IsActive);
            var rateId = model.MaterialRates.FirstOrDefault().MaterialRateId;
            var rateEntity = _unitOfWork.MaterialRates.FindAll().FirstOrDefault(x => x.MaterialRateId== rateId);

            var currentUser = GetCurrentUser();
            entity.ProductName = model.ProductName;
            entity.PartyName=model.PartyName;
            entity.ActualCode=model.ActualCode;
            entity.TradeDiscount=model.TradeDiscount;
            entity.PartyId= model.PartyId;
            entity.ProductId = model.ProductId;
            entity.Material_DOU = DateTime.Now;
            entity.Material_Updated_By = currentUser.CurrentUserId;
            entity.IsActive = true;
            //rateEntity.OldRate = model.MaterialRates.FirstOrDefault().OldRate;
            rateEntity.Rate = model.MaterialRates.FirstOrDefault().Rate;
            rateEntity.CFY_Id = _unitOfWork.FinancialYear.FindAll().FirstOrDefault(x => x.IsActive).FinancialYear_Id;
            _unitOfWork.Save();
            return new ResponseResults();

        }
    }
}
