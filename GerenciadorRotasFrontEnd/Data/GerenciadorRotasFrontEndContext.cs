using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciadorRotasFrontEnd.Models;

namespace GerenciadorRotasFrontEnd.Data
{
    public class GerenciadorRotasFrontEndContext : DbContext
    {
        public GerenciadorRotasFrontEndContext (DbContextOptions<GerenciadorRotasFrontEndContext> options)
            : base(options)
        {
        }

        public DbSet<GerenciadorRotasFrontEnd.Models.Cidade> Cidade { get; set; }

        public DbSet<GerenciadorRotasFrontEnd.Models.Pessoa> Pessoa { get; set; }

        public DbSet<GerenciadorRotasFrontEnd.Models.Equipe> Equipe { get; set; }

        public DbSet<GerenciadorRotasFrontEnd.Models.Usuario> Usuario { get; set; }
    }
}
