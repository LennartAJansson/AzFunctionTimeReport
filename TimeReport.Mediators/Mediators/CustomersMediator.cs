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
        var customer = mapper.Map<Customer>(request);
        customer = await service.CreateCustomer(customer);
        var response = mapper.Map<CustomerResponse>(customer);

        return response;
    }

    public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = mapper.Map<Customer>(request);
        customer = await service.UpdateCustomer(customer);
        var response = mapper.Map<CustomerResponse>(customer);

        return response;
    }

    public async Task<CustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await service.DeleteCustomer(request.Id);
        var response = mapper.Map<CustomerResponse>(customer);

        return response;
    }

    public async Task<IEnumerable<CustomerResponse>> Handle(ReadCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await service.ReadCustomers();
        var response = customers.Select(mapper.Map<CustomerResponse>);

        return response;
    }

    public async Task<CustomerFullResponse> Handle(ReadCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await service.ReadCustomer(request.Id);
        var response = mapper.Map<CustomerFullResponse>(customer);

        return response;
    }
}
