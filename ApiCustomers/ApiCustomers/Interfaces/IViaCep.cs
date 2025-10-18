using ApiCustomers.Models;

namespace ApiCustomers.Repositories.Interfaces
{
    public interface IViaCepClient
    {
        Task<ViaCepResponse> GetAddressAsync(string cep);
    }
}
