using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IDataRepository<Department> DepartmentMaster { get; }
        IDataRepository<UserDetails> UserDetails { get; }
        IDataRepository<Representatives> Representatives { get; }
        IDataRepository<LoginSession> LoginSessions { get; }
        IDataRepository<ApplicationUsage> ApplicationUsage { get; }
        IDataRepository<FinancialYear> FinancialYear { get; }
        IDataRepository<Group> Group { get; }
        IDataRepository<Product> Product { get; }
        IDataRepository<ProductApplicationUsage> ProductApplicationUsage { get; }
        IDataRepository<TermCondition> TermCondition { get; }
        IDataRepository<PartyMaster> PartyMaster { get; }
        IDataRepository<PartyEmail> PartyEmail { get; }
        IDataRepository<PartyPhone> PartyPhone { get; }
        IDataRepository<PartyAddress> PartyAddress { get; }
        IDataRepository<Lookup> Lookup { get; }
        IDataRepository<Contact> Contact { get; }
        IDataRepository<EmailList> EmailList { get; }
        IDataRepository<PhoneList> PhoneList { get; }
        IDataRepository<Material> Material { get; }
        IDataRepository<MaterialRates> MaterialRates { get; }
        IDataRepository<QuotationMaster> QuotationMaster { get; }
        IDataRepository<QuotationDetails> QuotationDetails { get; }
        IDataRepository<UserModulePermission> UserModulePermission { get; }
        IDataRepository<Module> Modules { get; }
        IDataRepository<UserPermission> UserPermission { get; }
        IDataRepository<PurchaseOrder> PurchaseOrder { get; }
        IDataRepository<PurchaseOrderDetails> PurchaseOrderDetails { get; }
        void Save();
    }
}
