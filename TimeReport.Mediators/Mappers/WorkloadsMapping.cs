namespace TimeReport.Mediators.Mappers;

using AutoMapper;

using TimeReport.Contract;
using TimeReport.Model;

public class WorkloadsMapping : Profile
{
    public WorkloadsMapping()
    {
        CreateMap<CreateWorkloadCommand, Workload>();
        CreateMap<UpdateWorkloadCommand, Workload>();

        CreateMap<Workload, WorkloadResponse>();
        CreateMap<Workload, WorkloadFullResponse>();
    }
}
