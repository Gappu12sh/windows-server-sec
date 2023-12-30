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
            throw new NotImplementedException();
        }

        public ResponseResults<List<PartyMasterModel>> GetParty()
        {
            var party = _unitOfWork.PartyMaster.FindAll()
                 .Include(x => x.UserDetailsCreatedBy)
                 .Include(x => x.UserDetailsUpdatedBy)
                 .Include(x => x.PartyAddress)
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

            party.PartyAddress = party.PartyAddress.Where(x => x.IsActive).ToList();
            //party.Contact = party.Contact.Where(x => x.IsActive).ToList();
            foreach (var item in party.PartyAddress)
            {
                item.Contacts=item.Contacts.Where(x=>x.IsActive).ToList();
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

            updatedResults = UpdateEmailsList(entityContact.EmailList.ToList(), model.emailListModels);
            if (updatedResults.HasError())
            {
                return new ResponseResults(ErrorCodes.UpdateRecordFailed);
            }
            updatedResults = UpdatePhonesList(entityContact.PhoneList.ToList(), model.phoneListModels);
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
            var entity = _unitOfWork.PartyMaster.FindAll().FirstOrDefault(x => x.Party_Id == model.Party_Id);
            entity.Party_Name = model.Party_Name;
            entity.Manufacturer_Type = model.Manufacturer_Type;
            entity.Party_DOU = DateTime.Now;
            entity.Party_Updated_By = currentUser.CurrentUserId;
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults UpdatePartyAddress(AdditionalPartyAddressModel model)
        {
            var updatedResults = new ResponseResults();
            var currentUser = GetCurrentUser();
            var entityAdd = _unitOfWork.PartyAddress.FindAll().FirstOrDefault(x => x.PartyAddress_Id == model.PartyAddress_Id);
            var entityEmail = _unitOfWork.PartyEmail.FindAll().Where(x => x.Party_Id == model.Party_Id).ToList();
            var entityPhone = _unitOfWork.PartyPhone.FindAll().Where(x => x.Party_Id == model.Party_Id).ToList();

            entityAdd.Party_Id = model.Party_Id;
            entityAdd.Contact_Email = model.Contact_Email;
            entityAdd.Contact_Phone = model.Contact_Phone;
            entityAdd.Address_Type = model.Address_Type;
            entityAdd.Address_Line1 = model.Address_Line1;
            entityAdd.Address_Line2 = model.Address_Line2;
            entityAdd.CountryId = model.CountryId;
            entityAdd.StateId = model.StateId;
            entityAdd.CityId = model.CityId;
            entityAdd.GST = model.GST;
            entityAdd.Supplier_Address = model.Supplier_Address;
            entityAdd.Contact_Remark = model.Contact_Remark;
            entityAdd.Pincode = model.Pincode;
            entityAdd.Representative_Id = model.Representative_Id;
            entityAdd.PartyAddress_Updated_By = currentUser.CurrentUserId;
            entityAdd.PartyAddress_DOU = DateTime.Now;
            entityAdd.IsActive = true;
            entityAdd.Zone = model.Zone;
            updatedResults = UpdateEmails(entityEmail, model.PartyEmails);
            if (updatedResults.HasError())
            {
                return new ResponseResults(ErrorCodes.UpdateRecordFailed);
            }
            updatedResults = UpdatePhones(entityPhone, model.PartyPhone);
            if (updatedResults.HasError())
            {
                return new ResponseResults(ErrorCodes.UpdateRecordFailed);
            }
            _unitOfWork.Save();

            return new ResponseResults();
        }

        public ResponseResults UpdateEmails(List<PartyEmail> email, List<PartyEmailModel> modelEmails)
        {
            var currentUser = GetCurrentUser();
            email.Clear();
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
                    email.Add(partyEmail);
                }
            }
            return new ResponseResults();
        }
        public ResponseResults UpdatePhones(List<PartyPhone> phone, List<PartyPhoneModel> modelPhones)
        {
            var currentUser = GetCurrentUser();
            phone.Clear();
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
                    phone.Add(partyPhone);
                }
            }
            return new ResponseResults();
        }
        public ResponseResults UpdateEmailsList(List<EmailList> email, List<EmailListModel> modelEmails)
        {
            var currentUser = GetCurrentUser();
            email.Clear();
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
                    email.Add(partyEmail);
                }
            }
            return new ResponseResults();
        }
        public ResponseResults UpdatePhonesList(List<PhoneList> phone, List<PhoneListModel> modelPhones)
        {
            var currentUser = GetCurrentUser();
            phone.Clear();
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
                    phone.Add(partyPhone);
                }
            }
            return new ResponseResults();
        }

    }
}
