using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SrmCapita.App.Dados;
using SrmCapita.App.Model;

namespace SrmCapita.App
{
    public partial class FrmClientes : Form
    {
        private readonly string _uri = ConfigurationManager.AppSettings["Uri"];


        public FrmClientes()
        {
            InitializeComponent();
        }

        int _codigoCliente = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            ObterClientes();
        }

        private void btnCadastrarCliente_Click(object sender, EventArgs e)
        {
            CadastrarCliente();

            tabControl1.SelectedIndex = 2;
        }

        private void btnObterPorId_Click(object sender, EventArgs e)
        {
            InputBox();
            if (_codigoCliente != -1)
            {
                ObterClientePorId(_codigoCliente);
            }
        }


        private void btnSelecionarPlanilha_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Selecionar Planilha",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xlsx",
                Filter = "xlsx files (*.xlsx)|*.xlsx",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };




            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;

                var sr = new
                    StreamReader(openFileDialog1.OpenFile());


                FileStream stream = File.OpenRead(openFileDialog1.FileName);
                byte[] array = new byte[stream.Length];

                UploadFile(openFileDialog1.FileName);

                sr.Close();



            }

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {

        }

        private void InputBox()
        {
            
            var prompt = "Informe o código do cliente.";
            const string titulo = "SRM Capital";

            string resultado = Microsoft.VisualBasic.Interaction.InputBox(prompt, titulo, "1", 600, 350);
            
            if (resultado != "")
                _codigoCliente = Convert.ToInt32(resultado);
            else
                _codigoCliente = -1;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            InputBox();
            if (_codigoCliente != -1)
            {
                ExcluirCliente(_codigoCliente);
            }

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
                        dgvDados.DataSource = JsonConvert.DeserializeObject<Cliente[]>(jsonString).ToList();
                    }
                    else
                    {
                        MessageBox.Show($"Não foi possível obter os clientes : {response.StatusCode}");
                    }
                }
            }
        }


     

        private async void ObterClientePorId(int codProduto)
        {
            var uri = $"{_uri}/api/cliente/obtercliente/{codProduto}";

            using (var client = new HttpClient())
            {
                var bsDados = new BindingSource();
                

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonCliente = await response.Content.ReadAsStringAsync();
                    bsDados.DataSource = JsonConvert.DeserializeObject<Cliente>(jsonCliente);
                    dgvDados.DataSource = bsDados;
                }
                else
                {
                    MessageBox.Show($"Falha ao obter o cliente : {response.StatusCode}");
                }
            }
        }


        private async void UploadFile(string arquivo)
        {
            var uri = $"{_uri}/api/cliente/uploadfile";


            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    client.BaseAddress = new Uri(uri);

                    content.Add(new StreamContent(File.OpenRead(arquivo)), "foo", "xls");

                   

                    var result = client.PostAsync(uri,content).Result;
                }
            }



            //using (var client = new HttpClient())
            //{

            //    using (var content = new MultipartFormDataContent())
            //    {
            //        var byteContent = new ByteArrayContent(array);



            //       // var fileContent = new ByteArrayContent(array);
            //        //fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };

            //        content.Add(byteContent);

            //        var response = await client.PostAsync(uri, byteContent);
            //    }





            //    //using (var br = new BinaryReader(vm.File.OpenReadStream()))
            //    //    data = br.ReadBytes((int)vm.File.OpenReadStream().Length);
            //    //ByteArrayContent bytes = new ByteArrayContent(data);
            //    //MultipartFormDataContent multiContent = new MultipartFormDataContent();
            //    //multiContent.Add(bytes, "file", vm.File.FileName);
            //    //multiContent.Add(new StringContent(vm.Id.ToString()), "Id");
            //    //multiContent.Add(new StringContent(vm.Name), "Name");




            //   // var result = client.PostAsync(uri, content).Result;



            //}
            //ObterClientes();
        }

        private async void CadastrarCliente()
        {
            var uri = $"{_uri}/api/cliente/cadastrarcliente";
            Cliente cliente = new Cliente();
            cliente.Nome = txtNome.Text.Trim();
            cliente.Email = txtEmail.Text.Trim();
            cliente.LimiteCompra =  Convert.ToDecimal(txtLimiteCompra.Text);
            cliente.DataCadastro = DateTime.Now;
            cliente.Telefone = txtTelefone.Text;
            
            
           

            using (var client = new HttpClient())
            {
                var serializedCliente = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(serializedCliente, Encoding.UTF8, "application/json");
                await client.PostAsync(uri, content);
            }
            ObterClientes();
        }

       


        private async void ExcluirCliente(int clienteId)
        {
            var uri = $"{_uri}/api/cliente/{clienteId}/ExcluirCliente";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                var responseMessage = await client.DeleteAsync(uri);
                if (responseMessage.IsSuccessStatusCode)
                    MessageBox.Show("Cliente excluído com sucesso");
                else
                    MessageBox.Show("Falha ao excluir o cliente  : " + responseMessage.StatusCode);
            }

            ObterClientes();
        }

       
    }
}
