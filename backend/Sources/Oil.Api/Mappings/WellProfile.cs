using AutoMapper;
using Oil.Api.ViewModels;
using Oil.Domain.Entity.Entities;

namespace Oil.Api.Mappings
{
    public class WellProfile: Profile
    {
        public WellProfile()
        {
            CreateMap<Well, WellViewModel.WellView>()
                .ForMember(d => d.Company, s => s.MapFrom(x => x.Company))
                .ForMember(d => d.Shop, s => s.MapFrom(x => x.Shop))
                .ForMember(d => d.Field, s => s.MapFrom(x => x.Field))
                .ForMember(d => d.WellType, s => s.MapFrom(x => x.WellType))
                .ForMember(d => d.FieldC, s => s.MapFrom(x=>x.Field.Name + "("+x.Field.Company.ShortName + ")"))
                .ForMember(d => d.ShopC, s => s.MapFrom(x=>x.Shop.Name + "("+x.Shop.Company.ShortName + ")"))

                .ReverseMap();

            CreateMap<WellType, WellTypeViewModel.WellTypeView>().ReverseMap();
        }
    }
}
