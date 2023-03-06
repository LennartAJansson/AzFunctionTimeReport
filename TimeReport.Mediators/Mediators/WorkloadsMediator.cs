namespace TimeReport.Mediators.Mediators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TimeReport.Contract;
using TimeReport.Data.Interfaces;
using TimeReport.Model;

public class WorkloadsMediator :
    IRequestHandler<CreateWorkloadCommand, WorkloadResponse>,
    IRequestHandler<UpdateWorkloadCommand, WorkloadResponse>,
    IRequestHandler<DeleteWorkloadCommand, WorkloadResponse>,
    IRequestHandler<ReadWorkloadsQuery, IEnumerable<WorkloadResponse>>,
    IRequestHandler<ReadWorkloadsByPersonQuery, IEnumerable<WorkloadFullResponse>>,
    IRequestHandler<ReadWorkloadsByCustomerQuery, IEnumerable<WorkloadFullResponse>>,
    IRequestHandler<ReadWorkloadQuery, WorkloadFullResponse>
{
    private readonly ITimeReportService service;
    private readonly IMapper mapper;

    public WorkloadsMediator(ITimeReportService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }
    public async Task<WorkloadResponse> Handle(CreateWorkloadCommand request, CancellationToken cancellationToken)
    {
        var workload = mapper.Map<Workload>(request);
        workload = await service.CreateWorkload(workload);
        var response = mapper.Map<WorkloadResponse>(workload);

        return response;
    }

    public async Task<WorkloadResponse> Handle(UpdateWorkloadCommand request, CancellationToken cancellationToken)
    {
        var workload = mapper.Map<Workload>(request);
        workload = await service.UpdateWorkload(workload);
        var response = mapper.Map<WorkloadResponse>(workload);

        return response;
    }

    public async Task<WorkloadResponse> Handle(DeleteWorkloadCommand request, CancellationToken cancellationToken)
    {
        var workload = await service.DeleteWorkload(request.Id);
        var response = mapper.Map<WorkloadResponse>(workload);

        return response;
    }

    public async Task<IEnumerable<WorkloadResponse>> Handle(ReadWorkloadsQuery request, CancellationToken cancellationToken)
    {
        var workloads = await service.ReadWorkloads();
        var response = workloads.Select(mapper.Map<WorkloadResponse>);

        return response;
    }

    public async Task<IEnumerable<WorkloadFullResponse>> Handle(ReadWorkloadsByPersonQuery request, CancellationToken cancellationToken)
    {
        var workloads = await service.ReadWorkloadsByPerson(request.PersonId);
        var response = workloads.Select(mapper.Map<WorkloadFullResponse>);

        return response;
    }

    public async Task<IEnumerable<WorkloadFullResponse>> Handle(ReadWorkloadsByCustomerQuery request, CancellationToken cancellationToken)
    {
        var workloads = await service.ReadWorkloadsByPerson(request.CustomerId);
        var response = workloads.Select(mapper.Map<WorkloadFullResponse>);

        return response;
    }

    public async Task<WorkloadFullResponse> Handle(ReadWorkloadQuery request, CancellationToken cancellationToken)
    {
        var workload = await service.ReadWorkload(request.Id);
        var response = mapper.Map<WorkloadFullResponse>(workload);

        return response;
    }
}
