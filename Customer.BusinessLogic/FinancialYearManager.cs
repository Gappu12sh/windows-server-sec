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
    public class FinancialYearManager : BaseManager, IFinancialYear
    {
        private readonly IUnitOfWork _unitOfWork;
        public FinancialYearManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults CreateFinancialYear(FinancialYearModel model)
        {
            var financialYear = _unitOfWork.FinancialYear.FindAll().Where(x => x.Financial_Year == model.Financial_Year && x.IsActive).ToList();
            if (financialYear.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var currentUser = GetCurrentUser();
            var entity = new FinancialYear
            {
                Financial_Year=model.Financial_Year,
                FinancialYear_DOE = DateTime.Now,
                FinancialYear_Created_By = currentUser.CurrentUserId
            };
            _unitOfWork.FinancialYear.Insert(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteFinancialYear(int id)
        {
            var entity = _unitOfWork.FinancialYear.Get(id);
            _unitOfWork.FinancialYear.SoftDelete(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults<List<FinancialYearModel>> GetFinancialYear()
        {
            var entityFinancialYear = _unitOfWork.FinancialYear.FindAll()
               .Include(x => x.UserDetailsCreatedBy).Include(x => x.UserDetailsUpdatedBy).Where(x=>x.IsActive)
               .ToList();
            var mapModel = Mapper.Map<List<FinancialYearModel>>(entityFinancialYear);

            return new ResponseResults<List<FinancialYearModel>>(mapModel);
        }

        public ResponseResults<FinancialYearModel> GetFinancialYearById(int Id)
        {
            var entityFinancialYear = _unitOfWork.FinancialYear.FindAll()
                 .Include(x => x.UserDetailsCreatedBy).Include(x => x.UserDetailsUpdatedBy).FirstOrDefault(x => x.FinancialYear_Id == Id);
            var mapModel = Mapper.Map<FinancialYearModel>(entityFinancialYear);

            return new ResponseResults<FinancialYearModel>(mapModel);
        }

        public ResponseResults UpdateFinancialYear(FinancialYearModel model)
        {
            var entityFinancialYear = _unitOfWork.FinancialYear.FindAll().Where(x => x.Financial_Year == model.Financial_Year && x.IsActive).ToList();
            if (entityFinancialYear.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var currentUser = GetCurrentUser();
            var financialYear = _unitOfWork.FinancialYear.Get(model.FinancialYear_Id);
            financialYear.Financial_Year = model.Financial_Year;
            financialYear.IsActive = true;
            financialYear.FinancialYear_Updated_By = currentUser.CurrentUserId;
            financialYear.FinancialYear_DOU = DateTime.Now;

            _unitOfWork.Save();

            return new ResponseResults();
        }
    }
}
