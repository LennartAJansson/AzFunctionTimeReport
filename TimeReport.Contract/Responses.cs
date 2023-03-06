namespace TimeReport.Contract;

//Responses without relationship
public record PersonResponse(int Id, string? Name, string? Email);
public record CustomerResponse(int Id, string? Name);
public record WorkloadResponse(int Id, int PersonId, int CustomerId, DateTime Start, DateTime? Stop);

//Responses with relationship
public record PersonFullResponse(int Id, string? Name, string? Email, IEnumerable<WorkloadResponse> Workloads);
public record CustomerFullResponse(int Id, string? Name, IEnumerable<WorkloadResponse> Workloads);
public record WorkloadFullResponse(int Id, int PersonId, int CustomerId, DateTime Start, DateTime? Stop, PersonResponse Person, CustomerResponse Customer);