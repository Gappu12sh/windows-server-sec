using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IGroup
    {
        ResponseResults CreateGroup(GroupModel model);
        ResponseResults UpdateGroup(GroupModel model);
        ResponseResults<List<GroupModel>> GetGroup();
        ResponseResults<GroupModel> GetGroupById(int Id);
        ResponseResults DeleteGroup(int id);
    }
}
