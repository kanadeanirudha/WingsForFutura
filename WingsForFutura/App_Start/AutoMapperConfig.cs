using AutoMapper;
using Coditech.DataAccessLayer.DataEntity;
using Coditech.Model;
using Coditech.Utilities.Filters;
using Coditech.ViewModel;
using System.Collections.Generic;
namespace Coditech
{
    public static class AutoMapperConfig
    {
        public static void Execute()
        {
            Mapper.CreateMap<FilterTuple, FilterDataTuple>();
            Mapper.CreateMap<UserModel, UserLoginViewModel>().ReverseMap();
            Mapper.CreateMap<UserModel, UserMaster>().ReverseMap();
            Mapper.CreateMap<UserModel, UserMasterViewModel>().ReverseMap();
            Mapper.CreateMap<AdminRoleMasterModel, AdminRoleMasterViewModel>().ReverseMap();
            Mapper.CreateMap<AdminRoleMasterModel, AdminRoleMaster>().ReverseMap();
            Mapper.CreateMap<UserModel, ChangePasswordViewModel>().ReverseMap();
            Mapper.CreateMap<UserMaster, ChangePasswordViewModel>().ReverseMap();
            Mapper.CreateMap<ClientMasterModel, ClientMasterViewModel>().ReverseMap();
            // For ClientMaster mapping
            Mapper.CreateMap<ClientMasterViewModel, UserModel>()
                .ForMember(dest => dest.FormAccessList, opt => opt.MapFrom(src => src.FormAccessList ?? new List<string>()));

            Mapper.CreateMap<UserModel, ClientMasterViewModel>()
                .ForMember(dest => dest.ClientMasterList, opt => opt.Ignore());
        }
    }
}
