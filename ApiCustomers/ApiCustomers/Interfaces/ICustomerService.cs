using ApiCustomers.Models;

namespace ApiCustomers.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(long id);
        Task<(bool Success, string ErrorMessage)> CreateAsync(Customer customer);
        Task<(bool Success, string ErrorMessage)> UpdateAsync(long id, Customer customer);
        Task<bool> DeleteAsync(long id);
    }
}
