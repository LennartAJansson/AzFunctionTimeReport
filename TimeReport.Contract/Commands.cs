namespace TimeReport.Contract;

using MediatR;

//public class CreatePersonCommand
//{
//    public string? Name { get; set; }
//    public string? Email { get; set; }
//}

public record CreatePersonCommand(string? Name, string? Email, string? Address, string? PostalCode, string? City): IRequest<PersonResponse>; 
public record UpdatePersonCommand(int Id, string? Name, string? Email, string? Address, string? PostalCode, string? City) : IRequest<PersonResponse>;
public record DeletePersonCommand(int Id): IRequest<PersonResponse>;

public record CreateCustomerCommand(string? Name) : IRequest<CustomerResponse>;
public record UpdateCustomerCommand(int Id, string? Name) : IRequest<CustomerResponse>;
public record DeleteCustomerCommand(int Id) : IRequest<CustomerResponse>;

public record CreateWorkloadCommand(int PersonId, int CustomerId, DateTime Start, DateTime? Stop) : IRequest<WorkloadResponse>;
public record UpdateWorkloadCommand(int Id, int PersonId, int CustomerId, DateTime Start, DateTime? Stop) : IRequest<WorkloadResponse>;
public record DeleteWorkloadCommand(int Id) : IRequest<WorkloadResponse>;
