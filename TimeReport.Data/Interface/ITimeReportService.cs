namespace TimeReport.Data.Interface;

using TimeReport.Model;

public interface ITimeReportService
{
    Task<Person?> GetPerson(int id);
    Task<IEnumerable<Person>> GetPeople();
    Task<Person> CreatePerson(Person person);
    Task<Person> UpdatePerson(Person person);
    Task<Person?> DeletePerson(int id);
    Task<Customer?> GetCustomer(int id);
    Task<IEnumerable<Customer>> GetCustomers();
    Task<Customer> CreateCustomer(Customer customer);
    Task<Customer> UpdateCustomer(Customer customer);
    Task<Customer?> DeleteCustomer(int id);
    Task<Workload?> GetWorkload(int id);
    Task<IEnumerable<Workload>> GetWorkloads();
    Task<Workload> CreateWorkload(Workload workload);
    Task<Workload> UpdateWorkload(Workload workload);
    Task<Workload?> DeleteWorkload(int id);
}
