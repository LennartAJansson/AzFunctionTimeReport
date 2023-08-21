namespace TimeReport.Mediators.Mediators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TimeReport.Contract;
using TimeReport.Data.Interfaces;
using TimeReport.Model;

public class PeopleMediator :
    IRequestHandler<CreatePersonCommand, PersonResponse>,
    IRequestHandler<UpdatePersonCommand, PersonResponse>,
    IRequestHandler<DeletePersonCommand, PersonResponse>,
    IRequestHandler<ReadPeopleQuery, IEnumerable<PersonResponse>>,
    IRequestHandler<ReadPersonQuery, PersonFullResponse>
{
    private readonly ITimeReportService service;
    private readonly IMapper mapper;

    public PeopleMediator(ITimeReportService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }
    public async Task<PersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = mapper.Map<Person>(request);
        person = await service.CreatePerson(person);
        PersonResponse response = mapper.Map<PersonResponse>(person);

        return response;
    }

    public async Task<PersonResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        Person? person = mapper.Map<Person>(request);
        person = await service.UpdatePerson(person);
        PersonResponse response = mapper.Map<PersonResponse>(person);

        return response;
    }

    public async Task<PersonResponse> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        Person? person = await service.DeletePerson(request.PersonId);
        PersonResponse response = mapper.Map<PersonResponse>(person);

        return response;
    }

    public async Task<IEnumerable<PersonResponse>> Handle(ReadPeopleQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Person> people = await service.ReadPeople();
        IEnumerable<PersonResponse> response = people.Select(mapper.Map<PersonResponse>);

        return response;
    }

    public async Task<PersonFullResponse> Handle(ReadPersonQuery request, CancellationToken cancellationToken)
    {
        Person? person = await service.ReadPerson(request.PersonId);
        PersonFullResponse response = mapper.Map<PersonFullResponse>(person);

        return response;
    }
}
