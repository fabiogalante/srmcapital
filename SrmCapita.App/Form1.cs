using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SrmCapita.App.Dados;
using SrmCapita.App.Model;

namespace SrmCapita.App
{
    public partial class Form1 : Form
    {
        private readonly string _uri = ConfigurationManager.AppSettings["Uri"];


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ObterClientes();
        }

       

        //=================================métodos para acessar a Web API ------------------------------------------------------

        private async void ObterClientes()
        {
            var uri = $"{_uri}/api/cliente/obterclientes";


            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        dgvDados.DataSource = JsonConvert.DeserializeObject<Clientes[]>(jsonString).ToList();
                    }
                    else
                    {
                        MessageBox.Show($"Não foi possível obter os clientes : {response.StatusCode}");
                    }
                }
            }
        }

        private async void GetProdutoById(int codProduto)
        {
            var uri = $"{_uri}/api/cliente/obterclientes";

            using (var client = new HttpClient())
            {
                BindingSource bsDados = new BindingSource();
                //URI = txtURI.Text + "/" + codProduto.ToString();

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                    bsDados.DataSource = JsonConvert.DeserializeObject<Clientes>(ProdutoJsonString);
                    dgvDados.DataSource = bsDados;
                }
                else
                {
                    MessageBox.Show("Falha ao obter o produto : " + response.StatusCode);
                }
            }
        }

        private async void AddProduto()
        {
            var uri = $"{_uri}/api/cliente/obterclientes";
            Clientes clientes = new Clientes();
            //Produto prod = new Produto();
            ////prod.Id = codProduto;
            //prod.Nome = "NoteBook Lenovo";
            //prod.Categoria = "Notebooks";
            //prod.Preco = 1200.00M;

            using (var client = new HttpClient())
            {
                var serializedProduto = JsonConvert.SerializeObject(clientes);
                var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(uri, content);
            }
            ObterClientes();
        }

        private async void UpdateProduto(int codProduto)
        {
            var uri = $"{_uri}/api/cliente/obterclientes";
            Clientes clientes = new Clientes();
            //prod.Id = codProduto;
            //prod.Nome = "NoteBook Apple";
            //prod.Categoria = "Notebooks";
            //prod.Preco = 9900.00M; // atualizando o preço do produto

            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(uri + "/" + clientes.Id, clientes);
                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Produto atualizado");
                }
                else
                {
                    MessageBox.Show("Falha ao atualizar o produto : " + responseMessage.StatusCode);
                }
            }
            ObterClientes();
        }


        private async void DeleteProduto(int codProduto)
        {
            var uri = $"{_uri}/api/cliente/obterclientes";
            int ProdutoID = codProduto;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage responseMessage = await client.DeleteAsync(String.Format("{0}/{1}", uri, ProdutoID));
                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Produto excluído com sucesso");
                }
                else
                {
                    MessageBox.Show("Falha ao excluir o produto  : " + responseMessage.StatusCode);
                }
            }
            ObterClientes();
        }
    }
}
