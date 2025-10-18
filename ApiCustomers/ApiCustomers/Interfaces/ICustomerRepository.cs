using ApiCustomers.Models;

namespace ApiCustomers.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(long id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}
