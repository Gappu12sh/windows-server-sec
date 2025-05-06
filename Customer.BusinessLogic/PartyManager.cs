using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Customer.Model;
using Customer.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic
{
    public class PartyManager : BaseManager, IParty
    {

        public PartyManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults CreateAddressByParty(AdditionalPartyAddressModel model)
        {
            var currentUser = GetCurrentUser();
            PartyAddress partyAddress = new PartyAddress();
            List<PartyEmail> emails = new List<PartyEmail>();
            List<PartyPhone> phones = new List<PartyPhone>();

            foreach (var item in model.PartyEmails)
            {
                PartyEmail partyEmail = new PartyEmail();
                partyEmail.Party_Id = model.Party_Id;
                partyEmail.Email_Type = item.Email_Type;
                partyEmail.Email = item.Email;
                partyEmail.IsActive = true;
                partyEmail.PartyEmail_Created_By = currentUser.CurrentUserId;
                partyEmail.PartyEmail_DOE = DateTime.Now;
                emails.Add(partyEmail);
            }
            foreach (var item in model.PartyPhone)
            {
                PartyPhone partyPhone = new PartyPhone();
                partyPhone.Party_Id = model.Party_Id;
                partyPhone.Phone_Type = item.Phone_Type;
                partyPhone.Phone_Number = item.Phone_Number;
                partyPhone.IsActive = true;
                partyPhone.PartyPhone_Created_By = currentUser.CurrentUserId;
                partyPhone.PartyPhone_DOE = DateTime.Now;
                phones.Add(partyPhone);
            }

            partyAddress.Party_Id = model.Party_Id;
            partyAddress.Contact_Email = model.Contact_Email;
            partyAddress.Contact_Phone = model.Contact_Phone;
            partyAddress.Address_Type = model.Address_Type;
            partyAddress.Address_Line1 = model.Address_Line1;
            partyAddress.Address_Line2 = model.Address_Line2;
            partyAddress.CountryId = model.CountryId;
            partyAddress.StateId = model.StateId;
            partyAddress.CityId = model.CityId;
            partyAddress.GST = model.GST;
            partyAddress.Supplier_Address = model.Supplier_Address;
            partyAddress.Contact_Remark = model.Contact_Remark;
            partyAddress.Pincode = model.Pincode;
            partyAddress.Representative_Id = model.Representative_Id;
            partyAddress.PartyAddress_Created_By = currentUser.CurrentUserId;
            partyAddress.PartyAddress_DOE = DateTime.Now;
            partyAddress.IsActive = true;
            partyAddress.Zone = model.Zone;

            //foreach (var item in emails)
            //{
            //    _unitOfWork.PartyEmail.Insert(item);
            //}
            //foreach (var item in phones)
            //{
            //    _unitOfWork.PartyPhone.Insert(item);
            //}
            _unitOfWork.PartyAddress.Insert(partyAddress);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults CreateContactByParty(ContactModel model)
        {
            var currentUser = GetCurrentUser();
            Contact contactObj = new Contact();
            List<EmailList> emailLists = new List<EmailList>();
            List<PhoneList> phoneLists = new List<PhoneList>();

            contactObj.Party_Id = model.Party_Id;
            contactObj.ContactTitle = model.ContactTitle;
            contactObj.ContactPersonName = model.ContactPersonName;
            contactObj.Designation = model.Designation;
            contactObj.DateOfBirth = model?.DateOfBirth;
            contactObj.DateOfAnniversary = model?.DateOfAnniversary;
            contactObj.IsActive = true;
            contactObj.Contact_Created_By = currentUser.CurrentUserId;
            contactObj.Contact_DOE = DateTime.Now;

            foreach (var email in model.emailListModels)
            {
                EmailList emailList = new EmailList();
                emailList.Email = email.Email;
                emailList.Email_Type = email.Email_Type;
                emailList.IsActive = true;
                emailList.EmailList_Created_By = currentUser.CurrentUserId;
                emailList.EmailList_DOE = DateTime.Now;
                emailLists.Add(emailList);
            }
            foreach (var phn in model.phoneListModels)
            {
                PhoneList phoneList = new PhoneList();
                phoneList.PhoneNumber = phn.PhoneNumber;
                phoneList.Phone_Type = phn.Phone_Type;
                phoneList.IsActive = true;
                phoneList.PhoneList_Created_By = currentUser.CurrentUserId;
                phoneList.PhoneList_DOE = DateTime.Now;
                phoneLists.Add(phoneList);
            }
            contactObj.EmailList = emailLists;
            contactObj.PhoneList = phoneLists;

            _unitOfWork.Contact.Insert(contactObj);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults CreateParty(PartyMasterModel model)
        {

            var currentUser = GetCurrentUser();
            var res = IsUserAllowed(Constants.Controllers.Contact, currentUser.CurrentUserId, Constants.ControllersMethod.Add);
            if (!res)
            {
                return new ResponseResults(ErrorCodes.InValidRequest);
            }
            PartyMaster partyMaster = new PartyMaster();
            List<PartyEmail> emails = new List<PartyEmail>();
            List<PartyPhone> phones = new List<PartyPhone>();
            List<PartyAddress> addresses = new List<PartyAddress>();

            partyMaster.Party_Name = model.Party_Name;
            partyMaster.Manufacturer_Type = model.Manufacturer_Type;
            partyMaster.Party_Created_By = currentUser.CurrentUserId;
            partyMaster.Party_DOE = DateTime.Now;
            foreach (var email in model.PartyEmails)
            {
                PartyEmail partyEmail = new PartyEmail();
                partyEmail.Email_Type = email.Email_Type;
                partyEmail.Email = email.Email;
                partyEmail.IsActive = true;
                partyEmail.PartyEmail_Created_By = currentUser.CurrentUserId;
                partyEmail.PartyEmail_DOE = DateTime.Now;
                emails.Add(partyEmail);
            }
            foreach (var phn in model.PartyPhone)
            {
                PartyPhone partyPhone = new PartyPhone();
                partyPhone.Phone_Type = phn.Phone_Type;
                partyPhone.Phone_Number = phn.Phone_Number;
                partyPhone.IsActive = true;
                partyPhone.PartyPhone_Created_By = currentUser.CurrentUserId;
                partyPhone.PartyPhone_DOE = DateTime.Now;
                phones.Add(partyPhone);
            }
            if (model.PartyAddress != null)
            {
                foreach (var item in model.PartyAddress)
                {
                    List<Contact> contactList = new List<Contact>();
                    PartyAddress partyAddress = new PartyAddress();
                    partyAddress.Contact_Email = item.Contact_Email;
                    partyAddress.Contact_Phone = item.Contact_Phone;
                    partyAddress.Address_Type = item.Address_Type;
                    partyAddress.Address_Line1 = item.Address_Line1;
                    partyAddress.Address_Line2 = item.Address_Line2;
                    partyAddress.CountryId = item.CountryId;
                    partyAddress.StateId = item.StateId;
                    partyAddress.CityId = item.CityId;
                    partyAddress.GST = item.GST;
                    partyAddress.Supplier_Address = item.Supplier_Address;
                    partyAddress.Contact_Remark = item.Contact_Remark;
                    partyAddress.Pincode = item.Pincode;
                    partyAddress.Representative_Id = item.Representative_Id;
                    partyAddress.PartyAddress_Created_By = currentUser.CurrentUserId;
                    partyAddress.PartyAddress_DOE = DateTime.Now;
                    partyAddress.IsActive = true;
                    partyAddress.Zone = item.Zone;
                    foreach (var cont in item.Contact)
                    {
                        List<EmailList> emailLists = new List<EmailList>();
                        List<PhoneList> phoneLists = new List<PhoneList>();
                        Contact contactObj = new Contact();
                        contactObj.ContactTitle = cont.ContactTitle;
                        contactObj.ContactPersonName = cont.ContactPersonName;
                        contactObj.Designation = cont.Designation;
                        contactObj.DateOfBirth = cont?.DateOfBirth;
                        contactObj.DateOfAnniversary = cont?.DateOfAnniversary;
                        contactObj.IsActive = true;
                        contactObj.Contact_Created_By = currentUser.CurrentUserId;
                        contactObj.Contact_DOE = DateTime.Now;
                        foreach (var email in cont.emailListModels)
                        {
                            EmailList emailList = new EmailList();
                            emailList.Email = email.Email;
                            emailList.Email_Type = email.Email_Type;
                            emailList.IsActive = true;
                            emailList.EmailList_Created_By = currentUser.CurrentUserId;
                            emailList.EmailList_DOE = DateTime.Now;
                            emailLists.Add(emailList);
                        }
                        foreach (var phn in cont.phoneListModels)
                        {
                            PhoneList phoneList = new PhoneList();
                            phoneList.PhoneNumber = phn.PhoneNumber;
                            phoneList.Phone_Type = phn.Phone_Type;
                            phoneList.IsActive = true;
                            phoneList.PhoneList_Created_By = currentUser.CurrentUserId;
                            phoneList.PhoneList_DOE = DateTime.Now;
                            phoneLists.Add(phoneList);
                        }
                        contactObj.EmailList = emailLists;
                        contactObj.PhoneList = phoneLists;
                        contactList.Add(contactObj);
                    }
                    partyAddress.Contacts = contactList;
                    addresses.Add(partyAddress);
                }
            }
            partyMaster.PartyEmails = emails;
            partyMaster.PartyPhone = phones;
            partyMaster.PartyAddress = addresses;
            //partyMaster.Contact = contactList;
            _unitOfWork.PartyMaster.Insert(partyMaster);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeleteParty(int id)
        {
            var currentUser = GetCurrentUser();
            var entity = _unitOfWork.PartyMaster.FindAll()
                .Include(x => x.PartyEmails)
                .Include(x => x.PartyPhone)
                .Include(x => x.PartyAddress)
                .Include(x => x.PartyAddress.Select(y => y.Contacts))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList)))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList)))
                .FirstOrDefault(x => x.Party_Id == id);
            entity.IsActive = false;
            entity.Party_DOU = DateTime.Now;
            entity.Party_Updated_By = currentUser.CurrentUserId;
            entity.PartyEmails?.ToList()?.ForEach(email =>
            {
                email.IsActive = false; email.PartyEmail_DOU = DateTime.Now;
                email.PartyEmail_Updated_By = currentUser.CurrentUserId;
            });
            entity.PartyPhone?.ToList()?.ForEach(phn =>
            {
                phn.IsActive = false; phn.PartyPhone_DOU = DateTime.Now;
                phn.PartyPhone_Updated_By = currentUser.CurrentUserId;
            });
            entity.PartyAddress.ToList().ForEach(add =>
            {
                add.IsActive = false; add.PartyAddress_DOU = DateTime.Now;
                add.PartyAddress_Updated_By = currentUser.CurrentUserId;
            });
            var contacts = entity.PartyAddress.SelectMany(x => x.Contacts).ToList();
            contacts?.ToList()?.ForEach(contact =>
            {
                contact.IsActive = false; contact.Contact_DOU = DateTime.Now;
                contact.Contact_Updated_By = currentUser.CurrentUserId;
            });
            var extraEmails = contacts?.SelectMany(x => x.EmailList).ToList();
            extraEmails?.ForEach(x =>
            {
                x.IsActive = false; x.EmailList_DOU = DateTime.Now;
                x.EmailList_Updated_By = currentUser.CurrentUserId;
            });
            var extraPhone = contacts?.SelectMany(x => x.PhoneList).ToList();
            extraPhone?.ForEach(x =>
            {
                x.IsActive = false; x.PhoneList_DOU = DateTime.Now;
                x.PhoneList_Updated_By = currentUser.CurrentUserId;
            });
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults<List<PartyMasterModel>> GetParty()
        {
            var party = _unitOfWork.PartyMaster.FindAll()
                 .Include(x => x.UserDetailsCreatedBy)
                 .Include(x => x.UserDetailsUpdatedBy)
                 .Include(x => x.PartyAddress)
                 .Include(x => x.PartyAddress.Select(y=>y.Representatives))
                 .Include(x => x.PartyAddress.Select(y => y.AddressType))
                 .Include(x => x.PartyAddress.Select(y => y.Country))
                 .Include(x => x.PartyAddress.Select(y => y.State))
                 .Include(x => x.PartyAddress.Select(y => y.City))
                 .Include(x => x.PartyAddress.Select(y => y.UserDetailsCreatedBy))
                 .Include(x => x.PartyAddress.Select(y => y.UserDetailsUpdatedBy))
                 .Include(x => x.PartyEmails)
                 .Include(x => x.PartyEmails.Select(z => z.EmailType))
                 .Include(x => x.PartyEmails.Select(z => z.UserDetailsCreatedBy))
                 .Include(x => x.PartyEmails.Select(z => z.UserDetailsUpdatedBy))
                 .Include(x => x.PartyPhone)
                 .Include(x => x.PartyPhone.Select(z => z.PhoneType))
                 .Include(x => x.PartyPhone.Select(z => z.UserDetailsCreatedBy))
                 .Include(x => x.PartyPhone.Select(z => z.UserDetailsUpdatedBy))
                 .Include(x => x.PartyAddress.Select(y => y.Contacts))
                 .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.UserDetailsCreatedBy)))
                 .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.UserDetailsUpdatedBy)))
                 .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList)))
                 .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList.Select(t => t.PhoneType))))
                 .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList)))
                 .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList.Select(t => t.EmailType))))
                 .Where(x => x.IsActive)
                 .ToList();
            foreach (var item in party)
            {

                item.PartyAddress = item.PartyAddress.Where(x => x.IsActive).ToList();
                //item.PartyAddress.Contact = item.PartyAddress.Select(x => x.Contacts.Where(y => y.IsActive)).ToList();
                foreach (var item1 in item.PartyAddress)
                {
                    item1.Contacts = item1.Contacts.Where(x => x.IsActive).ToList();
                    foreach (var phn in item1.Contacts)
                    {
                        phn.PhoneList = phn.PhoneList.Where(x => x.IsActive).ToList();
                    }
                    foreach (var emails in item1.Contacts)
                    {
                        emails.EmailList = emails.EmailList.Where(x => x.IsActive).ToList();
                    }
                }

            }
            var mapModel = Mapper.Map<List<PartyMasterModel>>(party.OrderByDescending(x => x.Party_Name).Reverse());

            return new ResponseResults<List<PartyMasterModel>>(mapModel);
        }

        public ResponseResults<PartyMasterModel> GetPartyById(int Id)
        {
            var party = _unitOfWork.PartyMaster.FindAll()
                  .Include(x => x.UserDetailsCreatedBy)
                  .Include(x => x.UserDetailsUpdatedBy)
                  .Include(x => x.PartyAddress)
                  .Include(x => x.PartyAddress.Select(y => y.City))
                  .Include(x => x.PartyAddress.Select(y => y.State))
                  .Include(x => x.PartyAddress.Select(y => y.Country))
                  .Include(x => x.PartyAddress.Select(y => y.AddressType))
                  .Include(x => x.PartyAddress.Select(y => y.UserDetailsCreatedBy))
                  .Include(x => x.PartyAddress.Select(y => y.UserDetailsUpdatedBy))
                  .Include(x => x.PartyEmails)
                  .Include(x => x.PartyEmails.Select(z => z.EmailType))
                  .Include(x => x.PartyEmails.Select(z => z.UserDetailsCreatedBy))
                  .Include(x => x.PartyEmails.Select(z => z.UserDetailsUpdatedBy))
                  .Include(x => x.PartyPhone)
                  .Include(x => x.PartyPhone.Select(z => z.PhoneType))
                  .Include(x => x.PartyPhone.Select(z => z.UserDetailsCreatedBy))
                  .Include(x => x.PartyPhone.Select(z => z.UserDetailsUpdatedBy))
                  .Include(x => x.PartyAddress.Select(y => y.Contacts))
                  .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.UserDetailsCreatedBy)))
                  .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.UserDetailsUpdatedBy)))
                  .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList)))
                  .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList.Select(t => t.PhoneType))))
                  .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList)))
                  .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList.Select(t => t.EmailType))))
                  .FirstOrDefault(x => x.IsActive && x.Party_Id == Id);

            party.PartyEmails = party.PartyEmails.Where(x => x.IsActive).ToList();
            party.PartyPhone = party.PartyPhone.Where(x => x.IsActive).ToList();
            party.PartyAddress = party.PartyAddress.Where(x => x.IsActive).ToList();
            //party.Contact = party.Contact.Where(x => x.IsActive).ToList();
            foreach (var item in party.PartyAddress)
            {
                item.Contacts = item.Contacts.Where(x => x.IsActive).ToList();
                foreach (var phn in item.Contacts)
                {
                    phn.PhoneList = phn.PhoneList.Where(x => x.IsActive).ToList();
                }
                foreach (var emails in item.Contacts)
                {
                    emails.EmailList = emails.EmailList.Where(x => x.IsActive).ToList();
                }
            }

            var mapModel = Mapper.Map<PartyMasterModel>(party);
            return new ResponseResults<PartyMasterModel>(mapModel);
        }

        public ResponseResults UpdateContact(ContactModel model)
        {
            var currentUser = GetCurrentUser();
            var updatedResults = new ResponseResults();
            var entityContact = _unitOfWork.Contact.FindAll()
                .Include(x => x.EmailList)
                .Include(x => x.PhoneList)
                .FirstOrDefault(x => x.Contact_Id == model.Contact_Id);
            entityContact.Party_Id = model.Party_Id;
            entityContact.ContactTitle = model.ContactTitle;
            entityContact.ContactPersonName = model.ContactPersonName;
            entityContact.Designation = model.Designation;
            entityContact.DateOfBirth = model?.DateOfBirth;
            entityContact.DateOfAnniversary = model?.DateOfAnniversary;
            entityContact.IsActive = true;
            entityContact.Contact_Updated_By = currentUser.CurrentUserId;
            entityContact.Contact_DOU = DateTime.Now;

            //updatedResults = UpdateEmailsList(entityContact.EmailList.ToList(), model.emailListModels);
            if (updatedResults.HasError())
            {
                return new ResponseResults(ErrorCodes.UpdateRecordFailed);
            }
            //updatedResults = UpdatePhonesList(entityContact.PhoneList.ToList(), model.phoneListModels);
            if (updatedResults.HasError())
            {
                return new ResponseResults(ErrorCodes.UpdateRecordFailed);
            }
            _unitOfWork.Save();

            return new ResponseResults();
        }

        public ResponseResults UpdateParty(PartyMasterModel model)
        {
            var currentUser = GetCurrentUser();
            var entity = _unitOfWork.PartyMaster.FindAll()
                .Include(x => x.PartyEmails)
                .Include(x => x.PartyPhone)
                .Include(x => x.PartyAddress)
                .Include(x => x.PartyAddress.Select(y => y.Contacts))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList)))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList)))
                .FirstOrDefault(x => x.Party_Id == model.Party_Id);

            var updatedResults = new ResponseResults();
            entity.Party_Name = model.Party_Name;
            entity.Manufacturer_Type = model.Manufacturer_Type;
            entity.Party_DOU = DateTime.Now;
            entity.Party_Updated_By = currentUser.CurrentUserId;

            PartyMaster partyMaster = new PartyMaster();
            List<PartyEmail> emails = new List<PartyEmail>();
            List<PartyPhone> phones = new List<PartyPhone>();
            List<PartyAddress> addresses = new List<PartyAddress>();

            updatedResults = UpdateEmails(entity, model.PartyEmails);
            if (updatedResults.HasError())
            {
                return new ResponseResults(ErrorCodes.UpdateRecordFailed);
            }
            updatedResults = UpdatePhones(entity, model.PartyPhone);
            if (updatedResults.HasError())
            {
                return new ResponseResults(ErrorCodes.UpdateRecordFailed);
            }
            updatedResults = UpdateAddress(entity, model.PartyAddress);
            if (updatedResults.HasError())
            {
                return new ResponseResults(ErrorCodes.UpdateRecordFailed);
            }
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults UpdateEmails(PartyMaster master, List<PartyEmailModel> modelEmails)
        {
            var currentUser = GetCurrentUser();
            master.PartyEmails?.ToList()?.ForEach(email => { email.IsActive = false; });
            foreach (PartyEmailModel Item in modelEmails)
            {
                if (Item.PartyEmail_Id != 0)
                {
                    var emailEntity = _unitOfWork.PartyEmail.FindAll().FirstOrDefault(x => x.PartyEmail_Id == Item.PartyEmail_Id);

                    emailEntity.Email_Type = Item.Email_Type;
                    emailEntity.Email = Item.Email;
                    emailEntity.IsActive = true;
                    emailEntity.PartyEmail_Updated_By = currentUser.CurrentUserId;
                    emailEntity.PartyEmail_DOU = DateTime.Now;
                }
                else
                {
                    var partyEmail = new PartyEmail
                    {
                        Party_Id = Item.Party_Id,
                        Email_Type = Item.Email_Type,
                        Email = Item.Email,
                        IsActive = true,
                        PartyEmail_Created_By = currentUser.CurrentUserId,
                        PartyEmail_DOE = DateTime.Now,
                    };
                    master.PartyEmails.Add(partyEmail);

                }
            }
            return new ResponseResults();
        }
        public ResponseResults UpdatePhones(PartyMaster master, List<PartyPhoneModel> modelPhones)
        {
            var currentUser = GetCurrentUser();
            master.PartyPhone?.ToList()?.ForEach(phn => { phn.IsActive = false; });
            foreach (PartyPhoneModel Item in modelPhones)
            {
                if (Item.PartyPhone_Id != 0)
                {
                    var emailEntity = _unitOfWork.PartyPhone.FindAll().FirstOrDefault(x => x.PartyPhone_Id == Item.PartyPhone_Id);

                    emailEntity.Phone_Type = Item.Phone_Type;
                    emailEntity.Phone_Number = Item.Phone_Number;
                    emailEntity.IsActive = true;
                    emailEntity.PartyPhone_Updated_By = currentUser.CurrentUserId;
                    emailEntity.PartyPhone_DOU = DateTime.Now;
                }
                else
                {
                    var partyPhone = new PartyPhone
                    {
                        Party_Id = Item.Party_Id,
                        Phone_Type = Item.Phone_Type,
                        Phone_Number = Item.Phone_Number,
                        IsActive = true,
                        PartyPhone_Created_By = currentUser.CurrentUserId,
                        PartyPhone_DOE = DateTime.Now,
                    };
                    master.PartyPhone.Add(partyPhone);
                }
            }
            return new ResponseResults();
        }
        public ResponseResults UpdateAddress(PartyMaster master, List<PartyAddressModel> modelAddress)
        {
            var currentUser = GetCurrentUser();
            master.PartyAddress.ToList().ForEach(add => { add.IsActive = false; });
            var updatedResults = new ResponseResults();

            modelAddress?.ForEach(addr =>
            {
                if (addr.PartyAddress_Id != 0)
                {
                    var entityAdd = _unitOfWork.PartyAddress.FindAll().FirstOrDefault(x => x.PartyAddress_Id == addr.PartyAddress_Id);
                    entityAdd.Contact_Email = addr.Contact_Email;
                    entityAdd.Contact_Phone = addr.Contact_Phone;
                    entityAdd.Address_Type = addr.Address_Type;
                    entityAdd.Address_Line1 = addr.Address_Line1;
                    entityAdd.Address_Line2 = addr.Address_Line2;
                    entityAdd.CountryId = addr.CountryId;
                    entityAdd.StateId = addr.StateId;
                    entityAdd.CityId = addr.CityId;
                    entityAdd.GST = addr.GST;
                    entityAdd.Supplier_Address = addr.Supplier_Address;
                    entityAdd.Contact_Remark = addr.Contact_Remark;
                    entityAdd.Pincode = addr.Pincode;
                    entityAdd.Representative_Id = addr.Representative_Id;
                    entityAdd.PartyAddress_Updated_By = currentUser.CurrentUserId;
                    entityAdd.PartyAddress_DOU = DateTime.Now;
                    entityAdd.IsActive = true;
                    entityAdd.Zone = addr.Zone;
                    updateAddressContact(entityAdd, addr.Contact);
                }
                else
                {
                    var partyAddress = new PartyAddress
                    {
                        Party_Id = addr.Party_Id,
                        Contact_Email = addr.Contact_Email,
                        Contact_Phone = addr.Contact_Phone,
                        Address_Type = addr.Address_Type,
                        Address_Line1 = addr.Address_Line1,
                        Address_Line2 = addr.Address_Line2,
                        CountryId = addr.CountryId,
                        StateId = addr.StateId,
                        CityId = addr.CityId,
                        GST = addr.GST,
                        Supplier_Address = addr.Supplier_Address,
                        Contact_Remark = addr.Contact_Remark,
                        Pincode = addr.Pincode,
                        Representative_Id = addr.Representative_Id,
                        PartyAddress_Created_By = currentUser.CurrentUserId,
                        PartyAddress_DOE = DateTime.Now,
                        IsActive = true,
                        Zone = addr.Zone,
                    };
                    updateAddressContact(partyAddress, addr.Contact);
                    master.PartyAddress.Add(partyAddress);
                }
            });
            return new ResponseResults();
        }
        public ResponseResults updateAddressContact(PartyAddress address, List<ContactModel> contactsModel)
        {
            var currentUser = GetCurrentUser();
            if (address.PartyAddress_Id != 0)
            {
                address.Contacts?.ToList()?.ForEach(contact => { contact.IsActive = false; });
            }
            contactsModel?.ForEach(contact =>
            {
                if (contact.Contact_Id != 0)
                {
                    var contactObj = _unitOfWork.Contact.FindAll().FirstOrDefault(x => x.Contact_Id == contact.Contact_Id);
                    contactObj.ContactTitle = contact.ContactTitle;
                    contactObj.ContactPersonName = contact.ContactPersonName;
                    contactObj.Designation = contact.Designation;
                    contactObj.DateOfBirth = contact?.DateOfBirth;
                    contactObj.DateOfAnniversary = contact?.DateOfAnniversary;
                    contactObj.IsActive = true;
                    contactObj.Contact_Updated_By = currentUser.CurrentUserId;
                    contactObj.Contact_DOU = DateTime.Now;
                    UpdateEmailsList(contactObj, contact.emailListModels);
                    UpdatePhonesList(contactObj, contact.phoneListModels);
                }
                else
                {
                    var contactObj = new Contact
                    {
                        PartyAddress_Id = contact.PartyAddress_Id,
                        ContactTitle = contact.ContactTitle,
                        ContactPersonName = contact.ContactPersonName,
                        Designation = contact.Designation,
                        DateOfBirth = contact?.DateOfBirth,
                        DateOfAnniversary = contact?.DateOfAnniversary,
                        IsActive = true,
                        Contact_Created_By = currentUser.CurrentUserId,
                        Contact_DOE = DateTime.Now,
                    };
                    UpdateEmailsList(contactObj, contact.emailListModels);
                    UpdatePhonesList(contactObj, contact.phoneListModels);
                    address.Contacts.Add(contactObj);
                };

            });
            return new ResponseResults();
        }
        public ResponseResults UpdateEmailsList(Contact contact, List<EmailListModel> modelEmails)
        {
            var currentUser = GetCurrentUser();
            if (contact.Contact_Id != 0)
            {
                contact.EmailList?.ToList()?.ForEach(e => { e.IsActive = false; });
            }
            foreach (EmailListModel Item in modelEmails)
            {
                if (Item.EmailListId != 0)
                {
                    var emailEntity = _unitOfWork.EmailList.FindAll().FirstOrDefault(x => x.EmailListId == Item.EmailListId);
                    emailEntity.Email_Type = Item.Email_Type;
                    emailEntity.Email = Item.Email;
                    emailEntity.IsActive = true;
                    emailEntity.EmailList_Updated_By = currentUser.CurrentUserId;
                    emailEntity.EmailList_DOU = DateTime.Now;
                }
                else
                {
                    var partyEmail = new EmailList
                    {
                        ContactId = Item.ContactId,
                        Email_Type = Item.Email_Type,
                        Email = Item.Email,
                        IsActive = true,
                        EmailList_Created_By = currentUser.CurrentUserId,
                        EmailList_DOE = DateTime.Now,
                    };
                    contact.EmailList.Add(partyEmail);
                }
            }
            return new ResponseResults();
        }
        public ResponseResults UpdatePhonesList(Contact contact, List<PhoneListModel> modelPhones)
        {
            var currentUser = GetCurrentUser();
            if (contact.Contact_Id != 0)
            {
                contact.PhoneList?.ToList()?.ForEach(e => { e.IsActive = false; });
            }
            foreach (PhoneListModel Item in modelPhones)
            {
                if (Item.PhoneListId != 0)
                {
                    var emailEntity = _unitOfWork.PhoneList.FindAll().FirstOrDefault(x => x.PhoneListId == Item.PhoneListId);
                    emailEntity.Phone_Type = Item.Phone_Type;
                    emailEntity.PhoneNumber = Item.PhoneNumber;
                    emailEntity.IsActive = true;
                    emailEntity.PhoneList_Updated_By = currentUser.CurrentUserId;
                    emailEntity.PhoneList_DOU = DateTime.Now;
                }
                else
                {
                    var partyPhone = new PhoneList
                    {
                        ContactId = Item.ContactId,
                        Phone_Type = Item.Phone_Type,
                        PhoneNumber = Item.PhoneNumber,
                        IsActive = true,
                        PhoneList_Created_By = currentUser.CurrentUserId,
                        PhoneList_DOE = DateTime.Now,
                    };
                    contact.PhoneList.Add(partyPhone);
                }
            }
            return new ResponseResults();
        }
        //public ResponseResults<List<PartyMasterModel>> GetPartyByRepId(int Id)
        //{
        //    var party = _unitOfWork.PartyMaster.FindAll()
        //        .Include(x => x.PartyAddress)
        //        .Include(x => x.PartyAddress.Select(y => y.Representatives))
        //        .Include(x => x.PartyAddress.Select(y => y.AddressType))
        //        .Include(x => x.PartyAddress.Select(y => y.Contacts))
        //        .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.UserDetailsCreatedBy)))
        //        .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.UserDetailsUpdatedBy)))
        //        .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList)))
        //        .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList.Select(t => t.PhoneType))))
        //        .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList)))
        //        .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList.Select(t => t.EmailType))))
        //        .Where(x => x.IsActive).ToList();

        //    var partyAddress = party.SelectMany(x => x.PartyAddress.Where(y => y.Representative_Id == Id).ToList()).ToList();
        //    if (!partyAddress.Any())
        //    {
        //       return new ResponseResults<List<PartyMasterModel>>(new List<PartyMasterModel>());
        //    }

        //    party.ForEach(x =>
        //    {
        //        x.PartyAddress = x.PartyAddress.Where(y => y.Representative_Id == Id).ToList();
        //    });

        //    party.ForEach(x =>
        //    {
        //        x.PartyAddress = x.PartyAddress.Where(y => y.IsActive).ToList();
        //        x.PartyAddress.ToList().ForEach(y =>
        //        {
        //            y.Contacts = y.Contacts.Where(z => z.IsActive).ToList();
        //            y.Contacts.ToList().ForEach(z =>
        //            {
        //                z.PhoneList = z.PhoneList.Where(w => w.IsActive).ToList();
        //                z.EmailList = z.EmailList.Where(r => r.IsActive).ToList();
        //            });
        //        });

        //    });
        //    //party.address.ForEach(x =>
        //    //{
        //    //    x.Contacts = x.Contacts.Where(q => q.IsActive).ToList();
        //    //    x.Contacts.ToList().ForEach(y =>
        //    //    {
        //    //        y.PhoneList = y.PhoneList.Where(z => z.IsActive).ToList();
        //    //        y.EmailList = y.EmailList.Where(z => z.IsActive).ToList();
        //    //    });
        //    //});
        //    //foreach (var item in address)
        //    //{
        //    //    item.Contacts = item.Contacts.Where(x => x.IsActive).ToList();
        //    //    foreach (var phn in item.Contacts)
        //    //    {
        //    //        phn.PhoneList = phn.PhoneList.Where(x => x.IsActive).ToList();
        //    //    }
        //    //    foreach (var emails in item.Contacts)
        //    //    {
        //    //        emails.EmailList = emails.EmailList.Where(x => x.IsActive).ToList();
        //    //    }
        //    //}


        //    var mapModel = Mapper.Map<List<PartyMasterModel>>(party.OrderByDescending(x => x.Party_Name).Reverse());
        //    return new ResponseResults<List<PartyMasterModel>>(mapModel);
        //}

        public ResponseResults<List<PartyMasterModel>> GetPartyByRepId(int Id)
        {
            var party = _unitOfWork.PartyMaster.FindAll()
                .Include(x => x.PartyAddress)
                .Include(x => x.PartyAddress.Select(y => y.Representatives))
                .Include(x => x.PartyAddress.Select(y => y.AddressType))
                .Include(x => x.PartyAddress.Select(y => y.City)) 
                .Include(x => x.PartyAddress.Select(y => y.State)) 
                .Include(x => x.PartyAddress.Select(y => y.Country)) 
                .Include(x => x.PartyAddress.Select(y => y.Contacts))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.UserDetailsCreatedBy)))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.UserDetailsUpdatedBy)))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList)))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.PhoneList.Select(t => t.PhoneType))))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList)))
                .Include(x => x.PartyAddress.Select(y => y.Contacts.Select(z => z.EmailList.Select(t => t.EmailType))))
                .Where(x => x.IsActive).ToList();

            var partyAddress = party.SelectMany(x => x.PartyAddress.Where(y => y.Representative_Id == Id).ToList()).ToList();
            if (!partyAddress.Any())
            {
                return new ResponseResults<List<PartyMasterModel>>(new List<PartyMasterModel>());
            }

            party.ForEach(x =>
            {
                x.PartyAddress = x.PartyAddress.Where(y => y.Representative_Id == Id).ToList();
            });

            party.ForEach(x =>
            {
                x.PartyAddress = x.PartyAddress.Where(y => y.IsActive).ToList();
                x.PartyAddress.ToList().ForEach(y =>
                {
                    y.Contacts = y.Contacts.Where(z => z.IsActive).ToList();
                    y.Contacts.ToList().ForEach(z =>
                    {
                        z.PhoneList = z.PhoneList.Where(w => w.IsActive).ToList();
                        z.EmailList = z.EmailList.Where(r => r.IsActive).ToList();
                    });
                });
            });

            var mapModel = Mapper.Map<List<PartyMasterModel>>(party.OrderByDescending(x => x.Party_Name).Reverse());
            return new ResponseResults<List<PartyMasterModel>>(mapModel);
        }

    }
}
