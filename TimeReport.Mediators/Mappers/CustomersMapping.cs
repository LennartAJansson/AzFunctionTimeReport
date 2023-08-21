namespace TimeReport.Mediators.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using TimeReport.Contract;
using TimeReport.Model;
public class CustomersMapping : Profile
{
    public CustomersMapping()
    {
        //Commands:
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

        //Queries:
        CreateMap<Customer, CustomerResponse>()
            .ForCtorParam("CustomerId", opt => opt.MapFrom(src => src.Id));
        CreateMap<Customer, CustomerFullResponse>()
            .ForCtorParam("CustomerId", opt => opt.MapFrom(src => src.Id));
    }
}
