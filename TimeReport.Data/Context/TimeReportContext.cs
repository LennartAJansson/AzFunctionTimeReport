namespace TimeReport.Data.Context;
using Microsoft.EntityFrameworkCore;

using TimeReport.Data.Interfaces;
using TimeReport.Model;

public sealed class TimeReportContext : DbContext, ITimeReportContext
{
    public DbSet<Person> People => Set<Person>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Workload> Workloads => Set<Workload>();

    public TimeReportContext(DbContextOptions<TimeReportContext> options)
        : base(options)
    { }
    //OnConfiguring
    //OnModelCreating

}
