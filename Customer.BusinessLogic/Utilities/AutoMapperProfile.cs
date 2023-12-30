using AutoMapper;
using Customer.Data.Application;
using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DepartmentModel, Department>()
                 .ForMember(dest => dest.Department_ID, opt => opt.MapFrom(src => src.Department_ID))
             .ForMember(dest => dest.Department_Name, opt => opt.MapFrom(src => src.Department_Name))
             .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
             .ForMember(dest => dest.Department_Created_By, opt => opt.MapFrom(src => src.Department_Created_By))
             .ForMember(dest => dest.Department_Updated_By, opt => opt.MapFrom(src => src.Department_Updated_By))
             .ForMember(dest => dest.Department_DOE, opt => opt.MapFrom(src => src.Department_DOE))
             .ForMember(dest => dest.Department_DOU, opt => opt.MapFrom(src => src.Department_DOU))
             .ForMember(dest => dest.UserDetailsCreatedBy, opt => opt.Ignore())
             .ForMember(dest => dest.UserDetailsUpdatedBy, opt => opt.Ignore());

            CreateMap<Contact, ContactModel>()
                .ForMember(dest => dest.emailListModels, opt => opt.MapFrom(src => src.EmailList))
                .ForMember(dest => dest.phoneListModels, opt => opt.MapFrom(src => src.PhoneList));

            CreateMap<PartyAddress, PartyAddressModel>()
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.Address_Line2==""? 
                src.Address_Line1 + "," + src.City.Description + "," + src.State.Description + "," + src.Country.Description : 
                src.Address_Line1 + ","+ src.Address_Line2+","+ src.City.Description + "," + src.State.Description + "," + src.Country.Description));

            CreateMap<ApplicationUsageModel, ApplicationUsage>()
                .ForMember(dest => dest.ApplicationUsage_Name, opt => opt.MapFrom(src => src.ApplicationUsage_Name.Trim()));

            CreateMap<Data.Application.UserModulePermission, UserModulePermissionModel>()
                .ForMember(dest => dest.userPermissionModels, opt => opt.MapFrom(src => src.Permissions));
            CreateMap<UserModulePermissionModel, UserModulePermission>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.userPermissionModels));
            CreateMap<UserPermissionModel, Data.Application.UserPermission>();
        }
    }
}
