using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SrmCapita.CoreServices.Interfaces;
using SrmCapita.CoreServices.Model;
using SrmCapita.CoreServices.ViewModel;

namespace SrmCapita.CoreServices.Servicos
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServico(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<IActionResult> ObterClientes()
        {
            try
            {
                IEnumerable<Cliente> clientes = await _clienteRepositorio.ObterClientes();

                if (clientes != null)
                {
                    return new OkObjectResult(clientes.Select(c => new ClientesViewModel
                            {
                                Id = c.Id,
                                Nome = c.Nome,
                                Telefone = c.Telefone,
                                Email = c.Email,
                                DataCadastro = c.DataCadastro,
                                LimiteCompra = c.LimiteCompra,
                               
                            }
                        )
                        .OrderBy(c => c.Nome)
                        .ThenBy(c => c.LimiteCompra)
                    );
                }

                return new NotFoundResult();

            }
            catch
            {
                return new ConflictResult();
            }
        }

        public async Task<IActionResult> AdicionarCliente(Cliente cliente)
        {
            if (cliente == null)
                return new BadRequestResult();

            var clienteId = await _clienteRepositorio.AdicionarCliente(cliente);

            if (clienteId > 0)
                return new OkObjectResult(clienteId);

            return new StatusCodeResult(500);
        }

        public async Task<IActionResult> ObterCliente(int? clienteId)
        {
            Cliente cliente = await _clienteRepositorio.ObterCliente(clienteId);

            if (cliente != null)
            {
                return new OkObjectResult(new ClientesViewModel
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Telefone = cliente.Telefone,
                    Email = cliente.Email,
                    DataCadastro = cliente.DataCadastro,
                    LimiteCompra = cliente.LimiteCompra,

                });

            }

            return new NoContentResult();
        }

        public async Task<IActionResult> ExcluirCliente(int? clienteId)
        {
            if (clienteId == null)
                return new NoContentResult();

            var cliente = await _clienteRepositorio.ExcluirCliente(clienteId);

            return new OkObjectResult(new ClientesViewModel()
            {
                Id = cliente.Id
            });
        }
    }
}
