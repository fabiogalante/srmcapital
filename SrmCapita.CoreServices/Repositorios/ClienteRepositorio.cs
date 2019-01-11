using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SrmCapita.CoreServices.Data;
using SrmCapita.CoreServices.Interfaces;
using SrmCapita.CoreServices.Model;

namespace SrmCapita.CoreServices.Repository
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
       
    }
}
