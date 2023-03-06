namespace TimeReport.Contract;

using MediatR;

public record ReadPeopleQuery() : IRequest<IEnumerable<PersonResponse>>;
public record ReadPersonQuery(int Id) : IRequest<PersonFullResponse>;
public record ReadCustomersQuery() : IRequest<IEnumerable<CustomerResponse>>;
public record ReadCustomerQuery(int Id) : IRequest<CustomerFullResponse>;
public record ReadWorkloadsQuery() : IRequest<IEnumerable<WorkloadResponse>>;
public record ReadWorkloadsByPersonQuery(int PersonId) : IRequest<IEnumerable<WorkloadFullResponse>>;
public record ReadWorkloadsByCustomerQuery(int CustomerId) : IRequest<IEnumerable<WorkloadFullResponse>>;
public record ReadWorkloadQuery(int Id) : IRequest<WorkloadFullResponse>;
