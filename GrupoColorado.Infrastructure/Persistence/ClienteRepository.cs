using GrupoColorado.Application.Interfaces;
using GrupoColorado.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrupoColorado.Infrastructure.Persistence
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> ListarTodosAsync()
        {
            return await _context.Clientes.Include(c => c.Telefones).ToListAsync();
        }

        public async Task<Cliente> ObterPorIdAsync(int id)
        {
            return await _context.Clientes.Include(c => c.Telefones)
                                          .FirstOrDefaultAsync(c => c.CodigoCliente == id);
        }

        public async Task CriarAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            var clienteExistente = await _context.Clientes
                .Include(c => c.Telefones)
                .FirstOrDefaultAsync(c => c.CodigoCliente == cliente.CodigoCliente);

            if (clienteExistente == null)
                return;

            // Atualiza os dados principais do cliente
            _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);

            // Remove os telefones antigos
            _context.Telefones.RemoveRange(clienteExistente.Telefones);

            // Adiciona os telefones novos
            clienteExistente.Telefones = cliente.Telefones;

            await _context.SaveChangesAsync();
        }


        public async Task DeletarAsync(int id)
        {
            var cliente = await ObterPorIdAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
