using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrmCapita.CoreServices.Interfaces;

namespace SrmCapita.CoreServices.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IClienteServico _clienteServico;

        public ClienteController(IClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }


        [HttpGet]
        [Route("ObterClientes")]
        public async Task<IActionResult> ObterClientes()
        {
            return await _clienteServico.ObterClientes();

        }
    }
}