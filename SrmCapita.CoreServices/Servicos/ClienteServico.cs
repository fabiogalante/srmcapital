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
                                ClienteNome = c.Nome
                            }
                        )
                        .OrderBy(c => c.ClienteNome)
                        //.ThenBy(c => c.ClienteNome)
                    );
                }

                return new NotFoundResult();

            }
            catch
            {
                return new ConflictResult();
            }
        }
    }
}
