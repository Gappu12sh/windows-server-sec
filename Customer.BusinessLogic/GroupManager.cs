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
    public class GroupManager :BaseManager, IGroup
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupManager(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public ResponseResults CreateGroup(GroupModel model)
        {
            var Group = _unitOfWork.Group.FindAll().Where(x => x.Group_Name.ToUpper().Trim() == model.Group_Name.ToUpper().Trim() && x.IsActive).ToList();
            if (Group.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var currentUser = GetCurrentUser();
            var entity = new Group
            {
                Group_Name=model.Group_Name,
                Group_DOE=DateTime.Now,
                Group_Created_By=currentUser.CurrentUserId
            };
            
            _unitOfWork.Group.Insert(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteGroup(int id)
        {
            var entity = _unitOfWork.Group.Get(id);
            _unitOfWork.Group.SoftDelete(entity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults<GroupModel> GetGroupById(int Id)
        {
            var entityGroup = _unitOfWork.Group.FindAll()
                .Include(x => x.UserDetailsCreatedBy).FirstOrDefault(x => x.GroupId == Id && x.IsActive);
            var mapModel = Mapper.Map<GroupModel>(entityGroup);

            return new ResponseResults<GroupModel>(mapModel);
        }

        public ResponseResults<List<GroupModel>> GetGroup()
        {
            var entityGroup = _unitOfWork.Group.FindAll()
                .Include(x => x.UserDetailsCreatedBy).Where(x=>x.IsActive)
                .ToList();
            var mapModel = Mapper.Map<List<GroupModel>>(entityGroup);

            return new ResponseResults<List<GroupModel>>(mapModel);
        }

        public ResponseResults UpdateGroup(GroupModel model)
        {
            var entityGroup = _unitOfWork.Group.FindAll().Where(x => x.Group_Name.ToUpper().Trim() == model.Group_Name.ToUpper().Trim() && x.IsActive).ToList();
            if (entityGroup.Count > 0)
            {
                return new ResponseResults(ErrorCodes.RecordAlreadyExists);
            }
            var currentUser = GetCurrentUser();
            var Group = _unitOfWork.Group.Get(model.GroupId);
            Group.Group_Name =model.Group_Name;
            Group.IsActive = true;
            _unitOfWork.Save();
            return new ResponseResults();
        }
    }
}
