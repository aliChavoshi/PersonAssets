using AutoMapper;
using PersonAssets.Data;
using PersonAssets.Data.Entity;
using PersonAssets.Models.Car;
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
        CreateMap<CreateCarViewModel, Car>().ReverseMap();
        CreateMap<Car, CarViewModel>()
            .ForMember(x => x.ModifiedBy,
                c => c.MapFrom(car => car.ModifyUser.Email))
            .ForMember(x => x.CreatedBy,
                c => c.MapFrom(v => v.CreateUser.Email))
            .ReverseMap();
    }
}