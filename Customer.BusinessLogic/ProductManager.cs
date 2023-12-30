using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Customer.Model;
using Customer.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic
{
    public class ProductManager :BaseManager, IProduct
    {       
        public ProductManager(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public ResponseResults CreateProduct(ProductModel model)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Product, currentUser.CurrentUserId, Constants.ControllersMethod.Add);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var mapEntity = Mapper.Map<Product>(model);
            mapEntity.Product_Name = char.ToUpper(model.Product_Name.Trim().First()) + model.Product_Name.Trim().Substring(1).ToLower();
            mapEntity.FinancialYear_Id = _unitOfWork.FinancialYear.FindAll().FirstOrDefault(x => x.IsActive).FinancialYear_Id;
            mapEntity.Product_DOE = DateTime.Now;
            mapEntity.Product_Created_By = currentUser.CurrentUserId;
            mapEntity.ProductApplicationUsages.ToList().ForEach(x=>x.IsActive = true);
            _unitOfWork.Product.Insert(mapEntity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteProduct(int id)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Product, currentUser.CurrentUserId, Constants.ControllersMethod.Delete);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var entity = _unitOfWork.Product.Get(id);
            _unitOfWork.Product.SoftDelete(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults<List<ProductModel>> GetProduct()
        {
            var result = _unitOfWork.Product.FindAll()
                .Include(x => x.ProductFinancialYear)
                .Include(x => x.UserDetailsCreatedBy)
                .Include(x => x.UserDetailsUpdatedBy).Where(x => x.IsActive)
                .ToList();
            var mapModel = Mapper.Map<List<ProductModel>>(result.OrderByDescending(x=>x.Product_Name).Reverse());
            return new ResponseResults<List<ProductModel>>(mapModel);
        }

        public ResponseResults<ProductModel> GetProductById(int Id)
        {
            var result = _unitOfWork.Product.FindAll()
                .Include(x=>x.ProductApplicationUsages)
                .Include(x => x.ProductApplicationUsages.Select(y=>y.ApplicationUsage))
                .Include(x => x.ProductFinancialYear)
                .Include(x=>x.UserDetailsCreatedBy)
                .Include(x=>x.UserDetailsUpdatedBy)
                .FirstOrDefault(x => x.Product_Id == Id);
            var mapModel = Mapper.Map<ProductModel>(result);
            return new ResponseResults<ProductModel>(mapModel);
        }

        public ResponseResults UpdateProduct(ProductModel model)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Product, currentUser.CurrentUserId, Constants.ControllersMethod.Edit);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var entity = _unitOfWork.Product.FindAll()
                .Include(x => x.ProductFinancialYear)
                .Include(x=>x.ProductApplicationUsages)
                .FirstOrDefault(x => x.Product_Id == model.Product_Id);
            if (entity.ProductApplicationUsages.Any())
            {
                _unitOfWork.ProductApplicationUsage.DeleteWhere<ProductApplicationUsage>(x => x.Product_Id == model.Product_Id);
                _unitOfWork.Save();
            }
            List<ProductApplicationUsage> productAppUsage = new List<ProductApplicationUsage>();
            entity.Product_Name = char.ToUpper(model.Product_Name.Trim().First()) + model.Product_Name.Trim().Substring(1).ToLower();
            entity.Product_Price = model.Product_Price;
            entity.Product_UpdateOn = model.Product_UpdateOn;
            entity.FinancialYear_Id = _unitOfWork.FinancialYear.FindAll().FirstOrDefault(x => x.IsActive).FinancialYear_Id;
            entity.Product_DOU = DateTime.Now;
            entity.Product_Updated_By = currentUser.CurrentUserId;

            foreach (var item in model.ProductApplicationUsages)
            {
                ProductApplicationUsage appUsage=new ProductApplicationUsage();
                appUsage.Product_Id = item.Product_Id;
                appUsage.ApplicationUsage_Id = item.ApplicationUsage_Id;
                appUsage.IsActive = true;
                productAppUsage.Add(appUsage);
            }
            entity.ProductApplicationUsages = productAppUsage;
            _unitOfWork.Save();
            return new ResponseResults();
        }
    }
}
