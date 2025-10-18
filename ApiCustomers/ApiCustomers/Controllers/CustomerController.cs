using ApiCustomers.Interfaces;
using ApiCustomers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCustomers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service) { _service = service; }

        /// <summary>
        /// Get All.
        /// </summary>
        /// <returns>Retornar todos os clientes em memória.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>
        /// Get By Id.
        /// </summary>
        /// <param name="id">Identificador do cliente em memória.</param>
        /// <returns>Retorna o objeto em memória 200 ou 404 not found caso não seja encontrado.</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var c = await _service.GetByIdAsync(id);
            if (c == null) return NotFound();
            return Ok(c);
        }

        /// <summary>
        /// Cria um cliente em memória.
        /// </summary>
        /// <param name="customer">Objeto cliente.</param>
        /// <returns>Retorna o objeto criado 200 ou 400 badrequest.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, error) = await _service.CreateAsync(customer);
            if (!success) return BadRequest(new { message = error });

            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Atualizar cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <param name="customer">Objeto do cliente.</param>
        /// <returns>Retorna 201 no content ou 400 badrequests.</returns>
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, error) = await _service.UpdateAsync(id, customer);
            if (!success) return BadRequest(new { message = error });

            return NoContent();
        }

        /// <summary>
        /// Deletar cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente.</param>
        /// <returns>Retorna 200 ok ou 404 not foud.</returns>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Gerar erro teste para Middleware.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("erro-teste")]
        public IActionResult GerarErro()
        {
            throw new Exception("Erro de teste!");
        }
    }
}
