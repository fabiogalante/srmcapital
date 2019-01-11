﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SrmCapita.CoreServices.ViewModel
{
    public class ClientesViewModel
    {

        public int Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public decimal LimiteCompra { get; set; }

        public DateTime DataCadastro { get; set; }


    }
}
