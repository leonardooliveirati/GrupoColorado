using GrupoColorado.Application.DTOs;

namespace GrupoColorado.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ListarClientesAsync();
        Task<ClienteDto> ObterClienteAsync(int id);
        Task CriarClienteAsync(ClienteDto cliente);
        Task AtualizarClienteAsync(ClienteDto cliente);
        Task DeletarClienteAsync(int id);
    }
}
