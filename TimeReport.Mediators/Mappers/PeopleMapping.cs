namespace TimeReport.Mediators.Mappers;

using AutoMapper;

using TimeReport.Contract;
using TimeReport.Model;

public class PeopleMapping : Profile
{
    public PeopleMapping()
    {
        CreateMap<CreatePersonCommand, Person>();
        CreateMap<UpdatePersonCommand, Person>();

        CreateMap<Person, PersonResponse>();
        CreateMap<Person, PersonFullResponse>();
    }
}
