namespace TimeReport.Data.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TimeReport.Data.Context;
using TimeReport.Data.Interfaces;
using TimeReport.Model;

public sealed class TimeReportService : ITimeReportService
{
    private readonly ILogger<TimeReportService> logger;
    private readonly TimeReportContext context;

    public TimeReportService(ILogger<TimeReportService> logger, TimeReportContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    #region People
    public async Task<Person> CreatePerson(Person person)
    {
        _ = context.Add(person);
        _ = await context.SaveChangesAsync();

        return person;
    }

    public async Task<IEnumerable<Person>> ReadPeople()
    {
        return await GetAll<Person>();
    }

    public async Task<Person?> ReadPerson(int id)
    {
        return await GetById<Person>(id);
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

    public async Task<IEnumerable<Customer>> ReadCustomers()
    {
        return await GetAll<Customer>();
    }

    public Task<Customer?> ReadCustomer(int id)
    {
        var customer = context.Customers.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(customer);
        //return await GetById<Customer>(id);
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

    public async Task<IEnumerable<Workload>> ReadWorkloads()
    {
        //TODO ReadWorkloads
        return await GetAll<Workload>();
    }

    public async Task<Workload?> ReadWorkload(int id)
    {
        //TODO ReadWorkload
        return await GetById<Workload>(id);
    }

    public async Task<IEnumerable<Workload>> ReadWorkloadsByPerson(int personId)
    {
        //TODO ReadWorkloadByPerson
        return (await GetAll<Workload>()).Where(w=>w.PersonId==personId);
    }

    public async Task<IEnumerable<Workload>> ReadWorkloadsByCustomer(int customerId)
    {
        //TODO ReadWorkloadByCustomer
        return (await GetAll<Workload>()).Where(w=>w.CustomerId==customerId);
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

    #endregion

    private Task<T?> GetById<T>(int id) where T : Entity
    {
        return Task.FromResult(context.Set<T>().FirstOrDefault(p => p.Id == id));
    }

    private Task<IEnumerable<T>> GetAll<T>() where T : Entity
    {
        return Task.FromResult(context.Set<T>().AsEnumerable());
    }
}