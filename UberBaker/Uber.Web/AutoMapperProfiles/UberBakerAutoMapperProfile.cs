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
            CreateMap<Customer, CustomerModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => string.Format("{0} {1}", src.FirstName, src.LastName)));
            CreateMap<Country, CountryModel>();
            CreateMap<Order, OrderModel>();
            CreateMap<Product, ProductModel>();
            CreateMap<ProductType, ProductTypeModel>();
            CreateMap<Permission, PermissionModel>();

            CreateMap<UserModel, User>();
            CreateMap<UserProfileModel, UserProfile>();
            CreateMap<RoleModel, Role>();
            CreateMap<ProductModel, Product>();
            CreateMap<ProductTypeModel, ProductType>();
            CreateMap<CustomerModel, Customer>();
            CreateMap<AddressModel, Address>();
            CreateMap<CountryModel, Country>();
            CreateMap<OrderModel, Order>();
            CreateMap<PermissionModel, Permission>();
        }
    }
}