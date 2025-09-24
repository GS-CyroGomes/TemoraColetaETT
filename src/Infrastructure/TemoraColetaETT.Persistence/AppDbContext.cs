using TemoraColetaETT.Domain;
using Microsoft.EntityFrameworkCore;

namespace TemoraColetaETT.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    // Tabela para a fila offline
    public DbSet<AcaoOffline> AcoesOffline { get; set; }

    private readonly string _databasePath;

    // O path será injetado pela camada MAUI depois
    public AppDbContext(string databasePath)
    {
        _databasePath = databasePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Filename={_databasePath}");
    }
}

// Entidade para a fila offline
public class AcaoOffline
{
    public int Id { get; set; }
    public string TipoAcao { get; set; } // Ex: "CADASTRO_USUARIO"
    public string DadosJson { get; set; } // Os dados do usuário em formato JSON
    public DateTime DataCriacao { get; set; }
}