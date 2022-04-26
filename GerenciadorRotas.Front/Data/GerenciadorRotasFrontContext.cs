using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciadorRotas.Front.Models;

namespace GerenciadorRotas.Front.Data
{
    public class GerenciadorRotasFrontContext : DbContext
    {
        public GerenciadorRotasFrontContext (DbContextOptions<GerenciadorRotasFrontContext> options)
            : base(options)
        {
        }

        //public DbSet<GerenciadorRotas.Front.Models.Cidade> Cidade { get; set; }

        //public DbSet<GerenciadorRotas.Front.Models.Pessoa> Pessoa { get; set; }
    }
}
