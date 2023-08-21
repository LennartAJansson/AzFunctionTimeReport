namespace TimeReport.Contract;

//Responses without relationship
public record PersonResponse(int PersonId, string? Name);
public record CustomerResponse(int CustomerId, string? Name);
public record WorkloadResponse(int WorkloadId, int PersonId, int CustomerId, DateTime Start, DateTime? Stop);

//Responses with relationship
public record PersonFullResponse(int PersonId, string? Name, IEnumerable<WorkloadResponse> Workloads);
public record CustomerFullResponse(int CustomerId, string? Name, IEnumerable<WorkloadResponse> Workloads);
public record WorkloadFullResponse(int WorkloadId, int PersonId, int CustomerId, DateTime Start, DateTime? Stop, PersonResponse Person, CustomerResponse Customer);