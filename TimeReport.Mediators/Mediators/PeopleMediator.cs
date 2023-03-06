namespace TimeReport.Mediators.Mediators;
using System;
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
        var person = mapper.Map<Person>(request);
        person = await service.CreatePerson(person);
        var response = mapper.Map<PersonResponse>(person);

        return response;
    }

    public async Task<PersonResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = mapper.Map<Person>(request);
        person = await service.UpdatePerson(person);
        var response = mapper.Map<PersonResponse>(person);

        return response;
    }

    public async Task<PersonResponse> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await service.DeletePerson(request.Id);
        var response = mapper.Map<PersonResponse>(person);

        return response;
    }

    public async Task<IEnumerable<PersonResponse>> Handle(ReadPeopleQuery request, CancellationToken cancellationToken)
    {
        var people = await service.ReadPeople();
        var response = people.Select(mapper.Map<PersonResponse>);

        return response;
    }

    public async Task<PersonFullResponse> Handle(ReadPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await service.ReadPerson(request.Id);
        var response = mapper.Map<PersonFullResponse>(person);

        return response;
    }
}
