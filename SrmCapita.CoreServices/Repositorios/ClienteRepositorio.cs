using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SrmCapita.CoreServices.Data;
using SrmCapita.CoreServices.Interfaces;
using SrmCapita.CoreServices.Model;

namespace SrmCapita.CoreServices.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private ClientesDbContext _clientesDbContext;
        public ClienteRepositorio(ClientesDbContext clientesDbContext)
        {
            _clientesDbContext = clientesDbContext;
        }


        public async Task<List<Cliente>> ObterClientes()
        {
            return await _clientesDbContext.Clientes.ToListAsync();
        }

        public async Task<int> AdicionarCliente(Cliente cliente)
        {
            await _clientesDbContext.Clientes.AddAsync(cliente);
            await Salvar();
            return cliente.Id;
        }

        public async Task<Cliente> ObterCliente(int? clienteId)
        {
            return await _clientesDbContext.Clientes
                .Where(p => p.Id == clienteId)
                .FirstOrDefaultAsync();
        }

        public async Task<Cliente> ExcluirCliente(int? clienteId)
        {
            var cliente = await ObterCliente(clienteId);

            if (cliente != null)
            {
                _clientesDbContext.Clientes.Remove(cliente);
                await Salvar();
            }

            return cliente;
        }

        public async Task Salvar()
        {
            await _clientesDbContext.SaveChangesAsync();
        }
    }
}
