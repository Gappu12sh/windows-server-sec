using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model.Utilities
{
    public class Constants
    {
        public class LookupCodes
        {
            public static string EmailOfficial = "PARTY-EMAIL-OFFICIAL";
            public static string EmailPersonal = "PARTY-EMAIL-PERSONAL";
            public static string PhoneOfficial = "PARTY-PHONE-OFFICIAL";
            public static string PhonePersonal = "PARTY-PHONE-PERSONAL";
            public static string AddressOfficial = "PARTY-ADDRESS-OFFICIAL";
            public static string AddressPersonal = "PARTY-ADDRESS-PERSONAL";
            public static string Add = "MODULE-ACTION-ADD";
            public static string Edit = "MODULE-ACTION-EDIT";
            public static string View = "MODULE-ACTION-VIEW";
            public static string Delete = "MODULE-ACTION-DELETE";
            public static string AddAddress = "MODULE-ACTION-ADD-ADDRESS";
            public static string ViewParty = "MODULE-ACTION-VIEW-PARTIES";
            public static string AddRate = "MODULE-ACTION-ADD-RATE";
        }
        public class LookupType
        {
            public static string PartyEmailType = "PARTY-EMAIL-TYPE";
            public static string PartyPhoneType = "PARTY-PHONE-TYPE";
            public static string PartyAddressType = "PARTY-ADDRESS-TYPE";
        }

        public class Controllers
        {
            public static string Department = "Department";
            public static string ApplicationUsage = "ApplicationUsage";
            public static string Representatives = "Representatives";
            public static string Product = "Product";
            public static string Contact = "Contact";
            public static string Material = "Material";
            public static string Samples = "Samples";
            public static string UserDetails = "UserDetails";
            public static string UserModulePermission = "UserModulePermission";
            public static string CustomerReport = "CustomerReport";
            public static string Quotations = "Quotations";
            public static string PartyAddress = "PartyAddress";
            
        }
        public class ControllersMethod
        {
            public static string Add = "Add";
            public static string Edit = "Edit";
            public static string Delete = "Delete";
            public static string View = "View";
            public static string ViewParty = "ViewParty";
            public static string AddAddress = "AddAddress";
            public static string AddRate = "AddRate";
        }
    }
}
