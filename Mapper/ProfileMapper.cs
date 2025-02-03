using AutoMapper;
using PersonAssets.Data;
using PersonAssets.Models.Person;

namespace PersonAssets.Mapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<CreatePersonViewModel, Person>().ReverseMap();
        CreateMap<Person, EditPersonViewModel>();
        CreateMap<EditPersonViewModel, Person>();
    }
}