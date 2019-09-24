using AutoMapper;
using Oil.Api.ViewModels;
using Oil.Domain.Entity.Entities;

namespace Oil.Api.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyViewModel.CompanyView>().ReverseMap();
        }
    }
}
