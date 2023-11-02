using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Entities;

namespace ModuloAPI.Context
{
    // Classe resposavel por acessar o DB
    public class AgendaContext : DbContext
    {
        // Recebe a conexão com o DB e tranfere para a classe base q inicia a conexão
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {}

        // entidade DbSet -> classe no programa e uma tabela no DB
        public DbSet<Contato> Contatos { get; set; }
    }
}