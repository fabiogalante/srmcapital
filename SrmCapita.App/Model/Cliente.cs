using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrmCapita.App.Model
{
    public class Cliente
    {

        public int Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public decimal LimiteCompra { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
