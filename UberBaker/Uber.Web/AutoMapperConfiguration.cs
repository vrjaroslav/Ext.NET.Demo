using AutoMapper;
using Uber.Web.AutoMapperProfiles;

namespace Uber.Web
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<UberBakerAutoMapperProfile>());
        }
    }
}