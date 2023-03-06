namespace TimeReport.Model;

public sealed class Person : Entity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }

    //Browsing properties:
    public ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
}
