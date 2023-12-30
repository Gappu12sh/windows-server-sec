using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IParty
    {
        ResponseResults CreateParty(PartyMasterModel model);
        ResponseResults UpdateParty(PartyMasterModel model);
        ResponseResults<List<PartyMasterModel>> GetParty();
        ResponseResults<PartyMasterModel> GetPartyById(int Id);
        ResponseResults DeleteParty(int id);
        ResponseResults CreateAddressByParty(AdditionalPartyAddressModel model);
        ResponseResults CreateContactByParty(ContactModel model);
        ResponseResults UpdatePartyAddress(AdditionalPartyAddressModel model);
        ResponseResults UpdateContact(ContactModel model);
    }
}
