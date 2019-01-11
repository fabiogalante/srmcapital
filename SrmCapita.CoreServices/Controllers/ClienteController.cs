using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SrmCapita.CoreServices.Interfaces;
using SrmCapita.CoreServices.Model;

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

        [HttpPost]
        [Route("CadastrarCliente")]
        public async Task<IActionResult> CadastrarCliente([FromBody] Cliente cliente)
        {
            return await _clienteServico.AdicionarCliente(cliente);
        }


        [HttpGet]
        [Route("ObterCliente/{clienteId}")]
        public async Task<IActionResult> ObterCliente(int clienteId)
        {
            return await _clienteServico.ObterCliente(clienteId);
        }

        [HttpDelete("{clienteId}/ExcluirCliente")]
        public async Task<IActionResult> Excluir(int clienteId)
        {
            return await _clienteServico.ExcluirCliente(clienteId);
        }



        [HttpPost]
        [Route("UploadFile")]
        public IActionResult UploadFile(byte[] rowVersion)
        {
            //
            return new NotFoundResult();
        }

        
    }
}