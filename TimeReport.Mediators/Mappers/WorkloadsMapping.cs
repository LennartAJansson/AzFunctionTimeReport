namespace TimeReport.Mediators.Mappers;

using AutoMapper;

using TimeReport.Contract;
using TimeReport.Model;

public class WorkloadsMapping : Profile
{
    public WorkloadsMapping()
    {
        //Commands:
        CreateMap<CreateWorkloadCommand, Workload>();
        CreateMap<UpdateWorkloadCommand, Workload>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WorkloadId));

        //Queries:
        CreateMap<Workload, WorkloadResponse>()
            .ForCtorParam("WorkloadId", opt => opt.MapFrom(src => src.Id));
        CreateMap<Workload, WorkloadFullResponse>()
            .ForCtorParam("WorkloadId", opt => opt.MapFrom(src => src.Id));

    }
}
