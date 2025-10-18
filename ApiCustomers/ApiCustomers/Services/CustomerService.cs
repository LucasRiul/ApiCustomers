using ApiCustomers.Interfaces;
using ApiCustomers.Models;
using ApiCustomers.Repositories.Interfaces;

namespace ApiCustomers.Services
{
    public class CustomerService : ICustomerService
    {
        protected readonly ICustomerRepository _customerRepository;
        protected readonly IViaCepClient _viaCepClient;

        public CustomerService (ICustomerRepository customerRepository,
                IViaCepClient viaCepClient)
        {
            _customerRepository = customerRepository;
            _viaCepClient = viaCepClient;
        }
        public async Task<(bool Success, string ErrorMessage)> CreateAsync(Customer customer)
        {
            try
            {
                // Validação adicional já feita no controller (ModelState)
                var address = await _viaCepClient.GetAddressAsync(customer.Cep);
                if (address == null)
                    return (false, "Erro: CEP inválido ou erro ao consultar ViaCEP");

                customer.Street = address.logradouro;
                customer.City = address.localidade;
                customer.State = address.uf;

                await _customerRepository.AddAsync(customer);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Erro: {ex.Message} - {ex.InnerException?.Message}");
            }

        }

        public async Task<bool> DeleteAsync(long id)
        {
            if (!await _customerRepository.ExistsAsync(id)) return false;
            await _customerRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetByIdAsync(long id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateAsync(long id, Customer customer)
        {
            try
            {
                if (!await _customerRepository.ExistsAsync(id))
                    return (false, "Erro: Cliente não encontrado");

                var address = await _viaCepClient.GetAddressAsync(customer.Cep);
                if (address == null)
                    return (false, "Erro: CEP inválido ou erro ao consultar ViaCEP");

                customer.Id = id;
                customer.Street = address.logradouro;
                customer.City = address.localidade;
                customer.State = address.uf;

                await _customerRepository.UpdateAsync(customer);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Erro: {ex.Message} - {ex.InnerException?.Message}");
            }
        }
    }
}
