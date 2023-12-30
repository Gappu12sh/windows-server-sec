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
    public class DepartmentManager : BaseManager, IDepartment
    {
        public DepartmentManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults CreateDepartment(DepartmentModel department)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Department, currentUser.CurrentUserId,Constants.ControllersMethod.Add);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var departmentEntity = _unitOfWork.DepartmentMaster.FindAll().Where(x => x.Department_Name.ToUpper().Trim() == department.Department_Name.ToUpper().Trim() && x.IsActive).ToList();
            if (departmentEntity.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            //var entity = Mapper.Map<Department>(department);
            var entity = new Department
            {
                Department_Name = department.Department_Name.Trim(),
                Department_DOE = DateTime.Now,
                Department_Created_By = currentUser.CurrentUserId
            };
            _unitOfWork.DepartmentMaster.Insert(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteDepartment(int id)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Department, currentUser.CurrentUserId, Constants.ControllersMethod.Delete);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            var entity = _unitOfWork.DepartmentMaster.Get(id);
            _unitOfWork.DepartmentMaster.SoftDelete(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults<DepartmentModel> GetDepartmentById(int Id)
        {
            var entityDepartment = _unitOfWork.DepartmentMaster.FindAll()
                .Include(x => x.UserDetailsCreatedBy).Include(x => x.UserDetailsUpdatedBy).FirstOrDefault(x => x.Department_ID == Id);
            var mapModel = Mapper.Map<DepartmentModel>(entityDepartment);

            return new ResponseResults<DepartmentModel>(mapModel);
        }

        public ResponseResults<List<DepartmentModel>> GetDepartments()
        {
            var entityDepartment = _unitOfWork.DepartmentMaster.FindAll()
                .Include(x => x.UserDetailsCreatedBy).Include(x => x.UserDetailsUpdatedBy).Where(x => x.IsActive)
                .ToList();
            var mapModel = Mapper.Map<List<DepartmentModel>>(entityDepartment);

            return new ResponseResults<List<DepartmentModel>>(mapModel);
        }

        public ResponseResults UpdateDepartment(DepartmentModel department)
        {
            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Department, currentUser.CurrentUserId, Constants.ControllersMethod.Edit);
            if (!res)
            {
                return new ResponseResults<DepartmentModel>(ErrorCodes.InValidRequest);
            }
            var departmentEntity = _unitOfWork.DepartmentMaster.FindAll().Where(x => x.Department_Name.ToUpper().Trim() == department.Department_Name.ToUpper().Trim()
            && x.Department_ID != department.Department_ID && x.IsActive).ToList();
            if (departmentEntity.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var entityDepartment = _unitOfWork.DepartmentMaster.Get(department.Department_ID);
            entityDepartment.Department_Name = department.Department_Name.Trim();
            entityDepartment.IsActive = true;
            entityDepartment.Department_Updated_By = currentUser.CurrentUserId;
            entityDepartment.Department_DOU = DateTime.Now;

            _unitOfWork.Save();

            return new ResponseResults();
        }
    }
}
