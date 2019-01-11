using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SrmCapita.CoreServices.Model;

namespace SrmCapita.CoreServices.Interfaces
{
    public interface IClienteServico
    {
        Task<IActionResult> ObterClientes();
        Task<IActionResult> AdicionarCliente(Cliente cliente);
        Task<IActionResult> ObterCliente(int? clienteId);
        Task<IActionResult> ExcluirCliente(int? clienteId);
    }
}
