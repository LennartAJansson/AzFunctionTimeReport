namespace TimeReport.Data.Context;

using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

using TimeReport.Data.Configuration;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        //modelBuilder
        //    .ApplyConfiguration(new PersonConfiguration())
        //    .ApplyConfiguration(new CustomerConfiguration())
        //    .ApplyConfiguration(new WorkloadConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder
        //    .UseLoggerFactory(this.GetService<ILoggerFactory>());
    }

    internal void UpdateDb()
    {
        if(Database.GetPendingMigrations().Any())
        {
            Database.Migrate();
        }
    }

    //TODO: Add OnConfiguring if needed (create loggers etc)
    //TODO: Add your own override of SaveChanges if needed (add audit fields etc)
    //TODO: If needed, add a method to update and seed the database

}
