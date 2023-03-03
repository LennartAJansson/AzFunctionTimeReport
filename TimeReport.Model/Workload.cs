namespace TimeReport.Model;

public sealed class Workload : Entity
{
    public int PersonId { get; set; }
    public int CustomerId { get; set; }
    public DateTime Start { get; set; }
    public DateTime? Stop { get; set; }

    //Browsing properties:
    public Person? Person { get; set; }
    public Customer? Customer { get; set; }
}