using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.BusinessLogic.Utilities;
using Customer.Data.Application;
using Customer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Customer.BusinessLogic
{
    public class UserManager : BaseManager, IUser
    {
        public UserManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults CreateUser(UserDetailsModel model)
        {

            var currentUser = GetCurrentUser();
            var user = new UserDetails()
            {
                UserID = model.UserID,
                UserName = model.UserName,
                User_Email = model.User_Email,
                User_Password = new Common().MD5Hash(model.User_Password),
                User_Created_By = currentUser.CurrentUserId,
                IsActive = model.IsActive,
                User_DOE = DateTime.Now,
                User_Type = model.User_Type,
                Rep_ID = model.Rep_ID,
                User_Dept = model.User_Dept,
            };
            _unitOfWork.UserDetails.Insert(user);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteUser(int id)
        {
            var user=_unitOfWork.UserDetails.FindAll()
                .FirstOrDefault(x=>x.UserID== id);
            if (user != null)
            {
                user.IsActive = false;
                _unitOfWork.Save();
            }
            return new ResponseResults();
        }

        public ResponseResults<UserDetailsModel> GetUserById(int Id)
        {
            var userEntity = _unitOfWork.UserDetails.FindAll()
                .Include(x => x.UserDetailsCreatedBy)
                .Include(x => x.UserDetailsUpdatedBy)
                .Include(x => x.Representatives)
                .FirstOrDefault(x => x.UserID == Id && x.IsActive);
            userEntity.User_Password = null;
            var mapModel = Mapper.Map<UserDetailsModel>(userEntity);
            return new ResponseResults<UserDetailsModel>(mapModel);
        }

        public ResponseResults<List<UserDetailsModel>> GetUsers()
        {
            var userEntity = _unitOfWork.UserDetails.FindAll()
                .Include(x => x.UserDetailsCreatedBy)
                .Include(x => x.UserDetailsUpdatedBy)
                .Include(x => x.Representatives)
                .Where(x => x.IsActive).ToList();
            userEntity.ForEach(x => x.User_Password = null);
            var mapModel = Mapper.Map<List<UserDetailsModel>>(userEntity);
            return new ResponseResults<List<UserDetailsModel>>(mapModel);
        }

        public ResponseResults UpdateUser(UserDetailsModel model)
        {
            var currentUser = GetCurrentUser();
            var entity = _unitOfWork.UserDetails.FindAll().FirstOrDefault(x => x.UserID == model.UserID && x.IsActive);
            if (entity != null)
            {
                entity.UserID = model.UserID;
                entity.UserName = model.UserName;
                entity.User_Email = model.User_Email;
                entity.User_Password = new Common().MD5Hash(model.User_Password);
                entity.User_Created_By = currentUser.CurrentUserId;
                entity.IsActive = true;
                entity.User_DOE = DateTime.Now;
                entity.User_Type = model.User_Type;
                entity.Rep_ID = model.Rep_ID==0?null :model.Rep_ID;
                _unitOfWork.Save();
            }

            return new ResponseResults();
        }
    }
}
