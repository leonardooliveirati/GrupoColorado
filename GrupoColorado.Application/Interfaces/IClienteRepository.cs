using GrupoColorado.Domain.Entities;

namespace GrupoColorado.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListarTodosAsync();
        Task<Cliente> ObterPorIdAsync(int id);
        Task CriarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task DeletarAsync(int id);
    }
}
