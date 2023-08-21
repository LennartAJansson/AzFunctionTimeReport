namespace TimeReport.Mediators.Mappers;

using AutoMapper;

using TimeReport.Contract;
using TimeReport.Model;

public class PeopleMapping : Profile
{
    public PeopleMapping()
    {
        //Commands:
        CreateMap<CreatePersonCommand, Person>();
        CreateMap<UpdatePersonCommand, Person>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId));

        //Queries:
        CreateMap<Person, PersonResponse>()
            .ForCtorParam("PersonId", opt => opt.MapFrom(src => src.Id));
        CreateMap<Person, PersonFullResponse>()
            .ForCtorParam("PersonId", opt => opt.MapFrom(src => src.Id));
    }
}
