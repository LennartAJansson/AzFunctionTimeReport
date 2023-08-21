namespace TimeReport.Model;

public sealed class Person : Entity
{
    public string? Name { get; set; }

    //Browsing properties:
    public ICollection<Workload> Workloads { get; set; } = new HashSet<Workload>();
}
