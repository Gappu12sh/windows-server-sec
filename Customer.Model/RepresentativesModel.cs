using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class RepresentativesModel
    {
        public int Rep_ID { get; set; }
        public int Rep_Unique_ID { get; set; }
        public string Rep_Name { get; set; }
        public bool IsActive { get; set; }
        public int Rep_Created_By { get; set; }
        public int Rep_Updated_By { get; set; }
        public DateTime Rep_DOE { get; set; }
        public DateTime Rep_DOU { get; set; }
        public UserDetailsModel UserDetailsDOE { get; set; }
        public UserDetailsModel UserDetailsDOU { get; set; }
    }

    public class ViewParty
    {
        public string PartyName { get; set; }
        public string RepresentativeName { get; set; }
        public AdditionalPartyAddressModel Address { get; set; }
    }
}
