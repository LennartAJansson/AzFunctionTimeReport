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
        Workload workload = mapper.Map<Workload>(request);
        workload = await service.CreateWorkload(workload);
        WorkloadResponse response = mapper.Map<WorkloadResponse>(workload);

        return response;
    }

    public async Task<WorkloadResponse> Handle(UpdateWorkloadCommand request, CancellationToken cancellationToken)
    {
        Workload? workload = mapper.Map<Workload>(request);
        workload = await service.UpdateWorkload(workload);
        WorkloadResponse response = mapper.Map<WorkloadResponse>(workload);

        return response;
    }

    public async Task<WorkloadResponse> Handle(DeleteWorkloadCommand request, CancellationToken cancellationToken)
    {
        Workload? workload = await service.DeleteWorkload(request.WorkloadId);
        WorkloadResponse response = mapper.Map<WorkloadResponse>(workload);

        return response;
    }

    public async Task<IEnumerable<WorkloadResponse>> Handle(ReadWorkloadsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Workload> workloads = await service.ReadWorkloads();
        IEnumerable<WorkloadResponse> response = workloads.Select(mapper.Map<WorkloadResponse>);

        return response;
    }

    public async Task<IEnumerable<WorkloadFullResponse>> Handle(ReadWorkloadsByPersonQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Workload> workloads = await service.ReadWorkloadsByPerson(request.PersonId);
        IEnumerable<WorkloadFullResponse> response = workloads.Select(mapper.Map<WorkloadFullResponse>);

        return response;
    }

    public async Task<IEnumerable<WorkloadFullResponse>> Handle(ReadWorkloadsByCustomerQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Workload> workloads = await service.ReadWorkloadsByCustomer(request.CustomerId);
        IEnumerable<WorkloadFullResponse> response = workloads.Select(mapper.Map<WorkloadFullResponse>);

        return response;
    }

    public async Task<WorkloadFullResponse> Handle(ReadWorkloadQuery request, CancellationToken cancellationToken)
    {
        Workload? workload = await service.ReadWorkload(request.WorkloadId);
        WorkloadFullResponse response = mapper.Map<WorkloadFullResponse>(workload);

        return response;
    }
}
