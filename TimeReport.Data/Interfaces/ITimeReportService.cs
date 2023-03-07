namespace TimeReport.Data.Interfaces;

using TimeReport.Model;

public interface ITimeReportService
{
    Task<Person?> ReadPerson(int id);
    Task<IEnumerable<Person>> ReadPeople();
    Task<Person?> CreatePerson(Person person);
    Task<Person> UpdatePerson(Person person);
    Task<Person?> DeletePerson(int id);
    Task<Customer?> ReadCustomer(int id);
    Task<IEnumerable<Customer>> ReadCustomers();
    Task<Customer> CreateCustomer(Customer customer);
    Task<Customer> UpdateCustomer(Customer customer);
    Task<Customer?> DeleteCustomer(int id);
    Task<Workload?> ReadWorkload(int id);
    Task<IEnumerable<Workload>> ReadWorkloads();
    Task<IEnumerable<Workload>> ReadWorkloadsByPerson(int personId);
    Task<IEnumerable<Workload>> ReadWorkloadsByCustomer(int customerId);
    Task<Workload> CreateWorkload(Workload workload);
    Task<Workload> UpdateWorkload(Workload workload);
    Task<Workload?> DeleteWorkload(int id);
}
