using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly CustomerApplicationContext _context;

        private IDataRepository<Department> _departmentMasterRepository;
        private IDataRepository<UserDetails> _userDetailsRepository;
        private IDataRepository<Representatives> _representativesRepository;
        private IDataRepository<LoginSession> _loginSessionRepository;
        private IDataRepository<ApplicationUsage> _applicationUsageRepository;
        private IDataRepository<FinancialYear> _financialYearRepository;
        private IDataRepository<Group> _groupRepository;
        private IDataRepository<Product> _productRepository;
        private IDataRepository<ProductApplicationUsage> _productApplicationUsageRepository;
        private IDataRepository<TermCondition> _termConditionRepository;
        private IDataRepository<PartyMaster> _partyMasterRepository;
        private IDataRepository<PartyEmail> _partyEmailRepository;
        private IDataRepository<PartyPhone> _partyPhoneRepository;
        private IDataRepository<PartyAddress> _partyAddressRepository;
        private IDataRepository<Contact> _contactRepository;
        private IDataRepository<Lookup> _lookupRepository;
        private IDataRepository<EmailList> _emailListRepository;
        private IDataRepository<PhoneList> _phoneListRepository;
        private IDataRepository<Material> _materialRepository;
        private IDataRepository<MaterialRates> _materialRatesRepository;
        private IDataRepository<QuotationMaster> _quotationMasterRepository;
        private IDataRepository<QuotationDetails> _quotationDetailsRepository;
        private IDataRepository<Module> _moduleRepository;
        private IDataRepository<UserModulePermission> _userModulePermissionRepository;
        private IDataRepository<UserPermission> _userPermissionRepository;

        public UnitOfWork(CustomerApplicationContext context)
        {
            _context = context;
            _context.Configuration.AutoDetectChangesEnabled = true;
        }

        public IDataRepository<Department> DepartmentMaster =>
            _departmentMasterRepository ?? (_departmentMasterRepository = new DataRepository<Department>(_context));

        public IDataRepository<UserDetails> UserDetails =>
            _userDetailsRepository ?? (_userDetailsRepository = new DataRepository<UserDetails>(_context));

        public IDataRepository<Representatives> Representatives =>
            _representativesRepository ?? (_representativesRepository = new DataRepository<Representatives>(_context));

        public IDataRepository<LoginSession> LoginSessions =>
            _loginSessionRepository ?? (_loginSessionRepository = new DataRepository<LoginSession>(_context));

        public IDataRepository<ApplicationUsage> ApplicationUsage =>
            _applicationUsageRepository ?? (_applicationUsageRepository = new DataRepository<ApplicationUsage>(_context));

        public IDataRepository<FinancialYear> FinancialYear =>
            _financialYearRepository ?? (_financialYearRepository = new DataRepository<FinancialYear>(_context));

        public IDataRepository<Group> Group =>
            _groupRepository ?? (_groupRepository = new DataRepository<Group>(_context));

        public IDataRepository<Product> Product =>
            _productRepository ?? (_productRepository = new DataRepository<Product>(_context));

        public IDataRepository<ProductApplicationUsage> ProductApplicationUsage =>
            _productApplicationUsageRepository ?? (_productApplicationUsageRepository = new DataRepository<ProductApplicationUsage>(_context));

        public IDataRepository<TermCondition> TermCondition =>
            _termConditionRepository ?? (_termConditionRepository = new DataRepository<TermCondition>(_context));
        public IDataRepository<PartyMaster> PartyMaster =>
           _partyMasterRepository ?? (_partyMasterRepository = new DataRepository<PartyMaster>(_context));

        public IDataRepository<PartyEmail> PartyEmail =>
            _partyEmailRepository ?? (_partyEmailRepository = new DataRepository<PartyEmail>(_context));

        public IDataRepository<PartyPhone> PartyPhone =>
            _partyPhoneRepository ?? (_partyPhoneRepository = new DataRepository<PartyPhone>(_context));

        public IDataRepository<PartyAddress> PartyAddress =>
            _partyAddressRepository ?? (_partyAddressRepository = new DataRepository<PartyAddress>(_context));

        public IDataRepository<Lookup> Lookup =>
            _lookupRepository ?? (_lookupRepository = new DataRepository<Lookup>(_context));
        public IDataRepository<Contact> Contact =>
          _contactRepository ?? (_contactRepository = new DataRepository<Contact>(_context));
        public IDataRepository<EmailList> EmailList =>
           _emailListRepository ?? (_emailListRepository = new DataRepository<EmailList>(_context));
        public IDataRepository<PhoneList> PhoneList =>
           _phoneListRepository ?? (_phoneListRepository = new DataRepository<PhoneList>(_context));

        public IDataRepository<Material> Material =>
          _materialRepository ?? (_materialRepository = new DataRepository<Material>(_context));
        public IDataRepository<MaterialRates> MaterialRates =>
           _materialRatesRepository ?? (_materialRatesRepository = new DataRepository<MaterialRates>(_context));
        public IDataRepository<QuotationMaster> QuotationMaster =>
            _quotationMasterRepository ?? (_quotationMasterRepository = new DataRepository<QuotationMaster>(_context));

        public IDataRepository<QuotationDetails> QuotationDetails =>
            _quotationDetailsRepository ?? (_quotationDetailsRepository = new DataRepository<QuotationDetails>(_context));
        public IDataRepository<Module> Modules =>
            _moduleRepository ?? (_moduleRepository = new DataRepository<Module>(_context));
        public IDataRepository<UserModulePermission> UserModulePermission =>
            _userModulePermissionRepository ?? (_userModulePermissionRepository = new DataRepository<UserModulePermission>(_context));
        public IDataRepository<UserPermission> UserPermission =>
            _userPermissionRepository ?? (_userPermissionRepository = new DataRepository<UserPermission>(_context));

        #region IDisposable Methods
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable Methods

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var stringBuilder = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    stringBuilder.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        stringBuilder.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        stringBuilder.AppendLine();
                    }
                }
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + stringBuilder, ex);
            }
        }
    }
}
