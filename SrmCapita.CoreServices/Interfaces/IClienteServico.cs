using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SrmCapita.CoreServices.Interfaces
{
    public interface IClienteServico
    {
        Task<IActionResult> ObterClientes();
    }
}
