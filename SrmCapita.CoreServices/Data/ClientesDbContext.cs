using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SrmCapita.CoreServices.Model;

namespace SrmCapita.CoreServices.Data
{
    public class ClientesDbContext : DbContext
    {
        public ClientesDbContext(DbContextOptions<ClientesDbContext> options) : base(options) { }

        public virtual DbSet<Cliente> Clientes { get; set; }
    }
}
