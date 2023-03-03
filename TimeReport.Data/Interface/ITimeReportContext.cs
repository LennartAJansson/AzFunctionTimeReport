﻿namespace TimeReport.Data.Interface;
using Microsoft.EntityFrameworkCore;
using TimeReport.Model;

public interface ITimeReportContext : IDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Person> People { get; }
    DbSet<Workload> Workloads { get; }
}
