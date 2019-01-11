using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SrmCapita.App.Model;

namespace SrmCapita.App.Dados
{
    public class ClientesDados
    {
        private readonly string _uri = ConfigurationManager.AppSettings["Uri"];

        public async Task<List<Clientes>> ObterClietes()
        {
            var jsonList = new List<Clientes>();


            var uri = $"{_uri}/api/cliente/obterclientes";

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        
                        var clienteJsonString = await response.Content.ReadAsStringAsync();
                        jsonList = JsonConvert.DeserializeObject<Clientes[]>(clienteJsonString).ToList();
                        
                    }
                }
            }

            return jsonList;
        }
    }
}
