using ApiCustomers.Data;
using ApiCustomers.Interfaces;
using ApiCustomers.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiCustomers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        //Injetar o context para acesso a dados
        public CustomerRepository(CustomerDbContext context) { _context = context; }

        /// <summary>
        /// Busca todos os clientes
        /// </summary>
        /// <returns>Lista de todos os Clientes</returns>
        public async Task<List<Customer>> GetAllAsync() =>
            await _context.Customers.AsNoTracking().ToListAsync();

        /// <summary>
        /// Busca o cliente pelo Id
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Retorna o cliente caso encontre, se não é null.</returns>
        public async Task<Customer> GetByIdAsync(long id) 
        {
            return await _context.Customers.FindAsync(id);
        }

        /// <summary>
        /// Adiciona um cliente
        /// </summary>
        /// <param name="customer">Objeto inteiro cliente</param>
        /// <returns>Retorno do save changes</returns>
        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza um cliente
        /// </summary>
        /// <param name="customer">Objeto inteiro atualizado cliente</param>
        /// <returns>Retorno do save changes</returns>
        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete  um cliente
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Retorno do save changes</returns>
        public async Task DeleteAsync(long id)
        {
            var c = await _context.Customers.FindAsync(id);
            if (c != null)
            {
                _context.Customers.Remove(c);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retorna se o cliente existe
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Retorna um booleano</returns>
        public async Task<bool> ExistsAsync(long id) =>
            await _context.Customers.AnyAsync(c => c.Id == id);

    }
}
