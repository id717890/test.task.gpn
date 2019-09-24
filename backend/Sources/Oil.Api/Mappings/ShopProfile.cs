﻿using AutoMapper;
using Oil.Api.ViewModels;
using Oil.Domain.Entity.Entities;

namespace Oil.Api.Mappings
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Shop, ShopViewModel.ShopView>().ReverseMap();

            CreateMap<Shop, ShopViewModel.ShopView>()
                .ForMember(d => d.CompanyId, s => s.MapFrom(x=>x.CompanyId))
                .ForMember(d => d.Company, s => s.MapFrom(x=>x.Company))
                .ForMember(d => d.FullName, s => s.MapFrom(x => x.Name + " (" + x.Company.ShortName + ")"))
                .ReverseMap();
        }
    }
}
