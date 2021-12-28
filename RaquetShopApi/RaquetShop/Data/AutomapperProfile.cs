using AutoMapper;
using RaquetShop.Data.Entities;
using RaquetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaquetShop.Data
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile() 
        {
            this.CreateMap<BrandModel, BrandEntity>()
                //.ForMember(tm => tm.Name, te => te.MapFrom(m => m.Name))
                .ReverseMap();

            this.CreateMap<RaquetModel, RaquetEntity>()
                .ForMember(ent => ent.Brand, mod => mod.MapFrom(modSrc => new BrandEntity() { id = modSrc.BrandId }))
                .ReverseMap()
                .ForMember(mod => mod.BrandId, ent => ent.MapFrom(entSrc => entSrc.Brand.id));
        }
    }
}
