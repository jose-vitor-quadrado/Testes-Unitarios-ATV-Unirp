using Microsoft.EntityFrameworkCore;

namespace SistemaNotificacao.Core;

public class AppDbContext : DbContext
{
    public DbSet<LogNotificacao> LogsNotificacoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DbSistemaNotificacoes;Trusted_Connection=True;");
    }
}