using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SrmCapita.CoreServices.Model;

namespace SrmCapita.CoreServices.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<List<Cliente>> ObterClientes();
    }
}
