using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    public class CustomerApplicationContext : DbContext
    {
        public CustomerApplicationContext() : base("name=CustomerApplication")
        {
            Database.SetInitializer<CustomerApplicationContext>(null);
            Database.CommandTimeout = 300;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("app");

            //modelBuilder.Entity<UserDetails>()
            //    .HasRequired(x => x.UserDetailsCreatedBy)
            //    .WithMany();

            //modelBuilder.Entity<UserDetails>()
            //   .HasRequired(x => x.UserDetailsUpdatedBy)
            //   .WithMany();

            //modelBuilder.Entity<UserDetails>()
            //   .HasRequired(x => x.Representatives)
            //   .WithMany();

            //modelBuilder.Entity<UserDetails>()
            //    .HasRequired(x => x.UserDetailsUpdatedBy)
            //    .WithMany();

            //modelBuilder.Entity<UserDetails>()
            //    .HasRequired(x => x.Representatives)
            //    .WithMany();

            modelBuilder.Entity<UserDetails>()
              .HasOptional(x => x.UserDetailsCreatedBy)
              .WithMany()
              .HasForeignKey(x => x.User_Created_By)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserDetails>()
               .HasOptional(x => x.UserDetailsUpdatedBy)
                .WithMany()
                .HasForeignKey(x => x.User_Updated_By)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserDetails>()
               .HasOptional(x => x.Representatives)
                .WithMany()
                .HasForeignKey(x => x.Rep_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasOptional(x => x.UserDetailsCreatedBy)
                .WithMany()
                .HasForeignKey(x => x.Department_Created_By)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
               .HasOptional(x => x.UserDetailsUpdatedBy)
                .WithMany()
                .HasForeignKey(x => x.Department_Updated_By)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Module>()
                .HasMany(x => x.ModuleActions)
                .WithMany(x => x.Modules).Map(ma =>
                {
                    ma.MapLeftKey("ModuleId");
                    ma.MapRightKey("ActionId");
                    ma.ToTable("ModuleActions");
                });


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Department> DepartmentMasters { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<Representatives> Representatives { get; set; }
        public virtual DbSet<LoginSession> LoginSession { get; set; }
        public virtual DbSet<ApplicationUsage> ApplicationUsage { get; set; }
        public virtual DbSet<FinancialYear> FinancialYear { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductApplicationUsage> ProductApplicationUsage { get; set; }
        public virtual DbSet<TermCondition> TermCondition { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<PartyMaster> PartyMasters { get; set; }
        public virtual DbSet<PartyEmail> PartyEmails { get; set; }
        public virtual DbSet<PartyPhone> PartyPhones { get; set; }
        public virtual DbSet<PartyAddress> PartyAddresses { get; set; }
        public virtual DbSet<Lookup> Lookups { get; set; }
        public virtual DbSet<PhoneList> PhoneLists { get; set; }
        public virtual DbSet<EmailList> EmailLists { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MaterialRates> MaterialRates { get; set; }
        public virtual DbSet<QuotationMaster> QuotationMasters { get; set; }
        public virtual DbSet<QuotationDetails> QuotationDetails { get; set; }
        public virtual DbSet<UserModulePermission> UserModulePermissions { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
    }
}
