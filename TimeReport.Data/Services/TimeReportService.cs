namespace TimeReport.Data.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TimeReport.Data.Interfaces;
using TimeReport.Model;

public sealed class TimeReportService : ITimeReportService
{
    private readonly ILogger<TimeReportService> logger;
    private readonly ITimeReportContext context;

    public TimeReportService(ILogger<TimeReportService> logger, ITimeReportContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    #region People
    public async Task<Person> CreatePersonAsync(Person person)
    {
        _ = context.Add(person);
        _ = await context.SaveChangesAsync();

        return person;
    }

    public Task<IEnumerable<Person>> ReadPeople()
    {
        return Task.FromResult(context.People.AsEnumerable());
    }

    public Task<Person?> ReadPerson(int id)
    {
        Person? person = context.People
            .Include("Workloads")
            .FirstOrDefault(p => p.Id == id);

        return Task.FromResult(person);
    }

    public async Task<Person> UpdatePerson(Person person)
    {
        _ = context.Update(person);
        _ = await context.SaveChangesAsync();

        return person;
    }

    public async Task<Person?> DeletePerson(int id)
    {
        Person? person = context.People.FirstOrDefault(p => p.Id == id);

        if (person is not null)
        {
            _ = context.Remove(person);
            _ = await context.SaveChangesAsync();
        }

        return person;
    }

    #endregion

    #region Customers
    public async Task<Customer> CreateCustomer(Customer customer)
    {
        _ = context.Add(customer);
        _ = await context.SaveChangesAsync();

        return customer;
    }

    public Task<IEnumerable<Customer>> ReadCustomers()
    {
        return Task.FromResult(context.Customers.AsEnumerable());
    }

    public Task<Customer?> ReadCustomer(int id)
    {
        Customer? customer = context.Customers
            .Include("Workloads")
            .FirstOrDefault(p => p.Id == id);

        return Task.FromResult(customer);
    }

    public async Task<Customer> UpdateCustomer(Customer customer)
    {
        _ = context.Update(customer);
        _ = await context.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer?> DeleteCustomer(int id)
    {
        Customer? customer = context.Customers.FirstOrDefault(p => p.Id == id);

        if (customer is not null)
        {
            _ = context.Remove(customer);
            _ = await context.SaveChangesAsync();
        }

        return customer;
    }

    #endregion

    #region Workloads
    public async Task<Workload> CreateWorkload(Workload workload)
    {
        _ = context.Add(workload);
        _ = await context.SaveChangesAsync();

        return workload;
    }

    public Task<IEnumerable<Workload>> ReadWorkloads()
    {
        return Task.FromResult(context.Workloads.AsEnumerable());
    }

    public Task<Workload?> ReadWorkload(int id)
    {
        Workload? workload = context.Workloads
            .Include("Person")
            .Include("Customer")
            .FirstOrDefault(p => p.Id == id);

        return Task.FromResult(workload);
    }

    public Task<IEnumerable<Workload>> ReadWorkloadsByPerson(int personId)
    {
        IEnumerable<Workload> workloads = context.Workloads
            .Include("Person")
            .Include("Customer")
            .Where(w => w.PersonId == personId)
            .AsEnumerable();

        return Task.FromResult(workloads);
    }

    public Task<IEnumerable<Workload>> ReadWorkloadsByCustomer(int customerId)
    {
        IEnumerable<Workload> workloads = context.Workloads
            .Include("Person")
            .Include("Customer")
            .Where(w => w.CustomerId == customerId)
            .AsEnumerable();

        return Task.FromResult(workloads);
    }

    public async Task<Workload> UpdateWorkload(Workload workload)
    {
        _ = context.Update(workload);
        _ = await context.SaveChangesAsync();

        return workload;
    }

    public async Task<Workload?> DeleteWorkload(int id)
    {
        Workload? workload = context.Workloads.FirstOrDefault(p => p.Id == id);

        if (workload is not null)
        {
            _ = context.Remove(workload);
            _ = await context.SaveChangesAsync();
        }

        return workload;
    }

    public Task<Person?> CreatePerson(Person person)
    {
        throw new NotImplementedException();
    }

    #endregion
}