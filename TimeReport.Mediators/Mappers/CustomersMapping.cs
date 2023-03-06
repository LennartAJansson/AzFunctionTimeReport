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
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();

        CreateMap<Customer, CustomerResponse>();
        CreateMap<Customer, CustomerFullResponse>()
            .BeforeMap((src, dest) => { })
            .AfterMap((src, dest) => { }); ;
    }
}
