namespace TimeReport.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

using TimeReport.Model;

public interface ITimeReportContext : IDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Person> People { get; }
    DbSet<Workload> Workloads { get; }

    //TODO: If needed, add a method to update and seed the database
}
