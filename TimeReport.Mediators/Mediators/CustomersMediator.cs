namespace TimeReport.Mediators.Mediators;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using TimeReport.Contract;
using TimeReport.Data.Interfaces;
using TimeReport.Model;

public class CustomersMediator :
    IRequestHandler<CreateCustomerCommand, CustomerResponse>,
    IRequestHandler<UpdateCustomerCommand, CustomerResponse>,
    IRequestHandler<DeleteCustomerCommand, CustomerResponse>,
    IRequestHandler<ReadCustomersQuery, IEnumerable<CustomerResponse>>,
    IRequestHandler<ReadCustomerQuery, CustomerFullResponse>
{
    private readonly ITimeReportService service;
    private readonly IMapper mapper;

    public CustomersMediator(ITimeReportService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }
    public async Task<CustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = mapper.Map<Customer>(request);

        customer = await service.CreateCustomer(customer);

        CustomerResponse response = mapper.Map<CustomerResponse>(customer);

        return response;
    }

    public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = mapper.Map<Customer>(request);
        //Could return null if not processed or found
        customer = await service.UpdateCustomer(customer);
        CustomerResponse response = mapper.Map<CustomerResponse>(customer);

        return response;
    }

    public async Task<CustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await service.DeleteCustomer(request.Id);
        CustomerResponse response = mapper.Map<CustomerResponse>(customer);

        return response;
    }

    public async Task<IEnumerable<CustomerResponse>> Handle(ReadCustomersQuery request, CancellationToken cancellationToken)
    {
        //Är egentligen en InternalDbSet<Customer>
        IEnumerable<Customer> customers = await service.ReadCustomers(); 

        //Är en Enumerable<Customer>
        IEnumerable<Customer> test = customers.Select(c => c);

        //Är en Enumerable<CustomerResponse>
        IEnumerable<CustomerResponse> response = customers.Select(mapper.Map<CustomerResponse>); 

        return response;
    }

    public async Task<CustomerFullResponse> Handle(ReadCustomerQuery request, CancellationToken cancellationToken)
    {
        Customer? customer = await service.ReadCustomer(request.Id);
        CustomerFullResponse response = mapper.Map<CustomerFullResponse>(customer);

        return response;
    }
}
