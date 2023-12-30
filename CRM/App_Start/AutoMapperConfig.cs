using AutoMapper;
using CRM;
using Customer.BusinessLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[assembly: PreApplicationStartMethod(typeof(AutoMapperConfig), "RegisterMappings")]

namespace CRM
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(map => map.AddProfile<AutoMapperProfile>());
        }
    }
}