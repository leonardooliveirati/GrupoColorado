using GrupoColorado.Application.DTOs;
using GrupoColorado.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrupoColorado.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _clienteService.ListarClientesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _clienteService.ObterClienteAsync(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDto cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clienteService.CriarClienteAsync(cliente);
            return CreatedAtAction(nameof(Get), new { id = cliente.CodigoCliente }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClienteDto cliente)
        {
            if (id != cliente.CodigoCliente)
                return BadRequest("Id não corresponde ao cliente enviado.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clienteService.AtualizarClienteAsync(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteService.DeletarClienteAsync(id);
            return NoContent();
        }
    }
}