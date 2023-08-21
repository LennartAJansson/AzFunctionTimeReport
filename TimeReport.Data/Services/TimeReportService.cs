namespace TimeReport.Data.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TimeReport.Data.Interfaces;
using TimeReport.Model;

//INFO: This is the service that will be used by the API controllers
//INFO: The queries will return related entities for single entities, but not for collections
//INFO: The methods are not optimized, but they are simple and easy to understand

//TODO: Consider handling of creating already existing entities
//TODO: Consider handling of creating related entities
//TODO: Consider handling of returning empty result sets
//TODO: Consider handling of update or delete of nonexisting entities
//TODO: Consider handling of update or delete of entities with missing relations
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
    public async Task<Person> CreatePerson(Person person)
    {
        logger.LogDebug("CreatePerson");

        _ = context.Add(person);
        _ = await context.SaveChangesAsync();

        return person;
    }

    public Task<Person?> ReadPerson(int personId)
    {
        logger.LogDebug("ReadPerson {id}", personId);

        Person? person = context.People
            .Include("Workloads")
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefault(p => p.Id == personId);

        return Task.FromResult(person);
    }

    public Task<IEnumerable<Person>> ReadPeople()
    {
        logger.LogDebug("ReadPeople");

        IEnumerable<Person> people = context.People
            .AsNoTrackingWithIdentityResolution()
            .AsEnumerable();

        return Task.FromResult(people);
    }

    public async Task<Person?> UpdatePerson(Person person)
    {
        logger.LogDebug("UpdatePerson");

        _ = context.Update(person);
        _ = await context.SaveChangesAsync();

        return person;
    }

    public async Task<Person?> DeletePerson(int personId)
    {
        logger.LogDebug("DeletePerson {id}", personId);

        Person? person = context.People
            .FirstOrDefault(p => p.Id == personId);

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
        logger.LogDebug("CreateCustomer");

        _ = context.Add(customer);
        _ = await context.SaveChangesAsync();

        return customer;
    }

    public Task<Customer?> ReadCustomer(int customerId)
    {
        logger.LogDebug("ReadCustomer {id}", customerId);

        Customer? customer = context.Customers
            .Include("Workloads")
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefault(p => p.Id == customerId);

        return Task.FromResult(customer);
    }

    public Task<IEnumerable<Customer>> ReadCustomers()
    {
        logger.LogDebug("ReadCustomers");

        IEnumerable<Customer> customers = context.Customers
            .AsNoTrackingWithIdentityResolution()
            .AsEnumerable();

        return Task.FromResult(customers);
    }

    public async Task<Customer?> UpdateCustomer(Customer customer)
    {
        logger.LogDebug("UpdateCustomer");

        _ = context.Update(customer);
        _ = await context.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer?> DeleteCustomer(int customerId)
    {
        logger.LogDebug("DeleteCustomer {id}", customerId);

        Customer? customer = context.Customers
            .FirstOrDefault(p => p.Id == customerId);

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
        logger.LogDebug("CreateWorkload");

        _ = context.Add(workload);
        _ = await context.SaveChangesAsync();

        return workload;
    }

    public Task<Workload?> ReadWorkload(int workloadId)
    {
        logger.LogDebug("ReadWorkload {id}", workloadId);

        Workload? workload = context.Workloads
            .Include("Person")
            .Include("Customer")
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == workloadId);

        return Task.FromResult(workload);
    }

    public Task<IEnumerable<Workload>> ReadWorkloads()
    {
        logger.LogDebug("ReadWorkloads");

        IEnumerable<Workload> workloads = context.Workloads
            .AsNoTracking()
            .AsEnumerable();

        return Task.FromResult(workloads);
    }

    public Task<IEnumerable<Workload>> ReadWorkloadsByPerson(int personId)
    {
        logger.LogDebug("ReadWorkloadsByPerson {id}", personId);

        IEnumerable<Workload> workloads = context.Workloads
            .Include("Person")
            .Include("Customer")
            .AsNoTrackingWithIdentityResolution()
            .Where(w => w.PersonId == personId)
            .AsEnumerable();

        return Task.FromResult(workloads);
    }

    public Task<IEnumerable<Workload>> ReadWorkloadsByCustomer(int customerId)
    {
        logger.LogDebug("ReadWorkloadsByCustomer {id}", customerId);

        IEnumerable<Workload> workloads = context.Workloads
            .Include("Person")
            .Include("Customer")
            .AsNoTrackingWithIdentityResolution()
            .Where(w => w.CustomerId == customerId)
            .AsEnumerable();

        return Task.FromResult(workloads);
    }

    public async Task<Workload?> UpdateWorkload(Workload workload)
    {
        logger.LogDebug("UpdateWorkload");

        _ = context.Update(workload);
        _ = await context.SaveChangesAsync();

        return workload;
    }

    public async Task<Workload?> DeleteWorkload(int workloadId)
    {
        logger.LogDebug("DeleteWorkload {id}", workloadId);

        Workload? workload = context.Workloads
            .FirstOrDefault(p => p.Id == workloadId);

        if (workload is not null)
        {
            _ = context.Remove(workload);
            _ = await context.SaveChangesAsync();
        }

        return workload;
    }
    #endregion
}