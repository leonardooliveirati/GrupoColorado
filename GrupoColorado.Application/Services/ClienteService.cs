using AutoMapper;
using GrupoColorado.Application.DTOs;
using GrupoColorado.Application.Interfaces;
using GrupoColorado.Domain.Entities;

namespace GrupoColorado.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDto>> ListarClientesAsync()
        {
            var clientes = await _repository.ListarTodosAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto> ObterClienteAsync(int id)
        {
            var cliente = await _repository.ObterPorIdAsync(id);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task CriarClienteAsync(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _repository.CriarAsync(cliente);
        }

        public async Task AtualizarClienteAsync(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            await _repository.AtualizarAsync(cliente);
        }

        public async Task DeletarClienteAsync(int id)
        {
            await _repository.DeletarAsync(id);
        }
    }
}
