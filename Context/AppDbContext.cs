using Microsoft.EntityFrameworkCore;
using AlunosApi.Models;

namespace AlunosApi.Context
{
    public class AppDbContext : DbContext
    {
        //Este contexto deve ser declarado no Startup informando o provedor que será utilizado e a string de conexão
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
    }
}
