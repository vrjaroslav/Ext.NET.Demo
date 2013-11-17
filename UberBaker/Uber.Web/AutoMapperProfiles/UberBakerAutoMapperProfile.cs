using AutoMapper;
using Uber.Core;
using Uber.Web.Models;

namespace Uber.Web.AutoMapperProfiles
{
    public class UberBakerAutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserProfile, UserProfileModel>();
            CreateMap<Role, RoleModel>();
            CreateMap<Address, AddressModel>();
            CreateMap<Customer, CustomerModel>();
            CreateMap<Country, CountryModel>();
            CreateMap<Order, OrderModel>();
            CreateMap<ProductType, ProductTypeModel>();
        }
    }
}