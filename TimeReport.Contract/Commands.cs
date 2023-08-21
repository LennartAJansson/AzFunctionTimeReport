namespace TimeReport.Contract;

using MediatR;

//public class CreatePersonCommand
//{
//    public string? Name { get; set; }
//    public string? Email { get; set; }
//}

public record CreatePersonCommand(string? Name): IRequest<PersonResponse>; 
public record UpdatePersonCommand(int PersonId, string? Name) : IRequest<PersonResponse>;
public record DeletePersonCommand(int PersonId): IRequest<PersonResponse>;

public record CreateCustomerCommand(string? Name) : IRequest<CustomerResponse>;
public record UpdateCustomerCommand(int CustomerId, string? Name) : IRequest<CustomerResponse>;
public record DeleteCustomerCommand(int CustomerId) : IRequest<CustomerResponse>;

public record CreateWorkloadCommand(int PersonId, int CustomerId, DateTime Start, DateTime? Stop) : IRequest<WorkloadResponse>;
public record UpdateWorkloadCommand(int WorkloadId, int PersonId, int CustomerId, DateTime Start, DateTime? Stop) : IRequest<WorkloadResponse>;
public record DeleteWorkloadCommand(int WorkloadId) : IRequest<WorkloadResponse>;
