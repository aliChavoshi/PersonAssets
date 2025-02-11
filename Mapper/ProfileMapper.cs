﻿using AutoMapper;
using PersonAssets.Data;
using PersonAssets.Models.Person;

namespace PersonAssets.Mapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<CreatePersonViewModel, Person>().ReverseMap();
            // .ForMember(x=>x.FirstName,
            //     c=>c.Ignore())
            // .ForMember(x => x.LastName,
            //     c => c.MapFrom(v => v.LastName.Trim().ToUpper()));
        CreateMap<Person, EditPersonViewModel>().ReverseMap();
        // CreateMap<EditPersonViewModel, Person>();
    }
}