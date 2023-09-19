using Microsoft.EntityFrameworkCore;
using MVC_ComponentesCodeFirst.Models.Componentes;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using MVC_ComponentesCodeFirst.Models.Pedidos;

namespace MVC_ComponentesCodeFirst.Data;

public class ComponenteContext : DbContext
{
    public ComponenteContext(DbContextOptions<ComponenteContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new DbInitializer(modelBuilder).Seed();

        modelBuilder.Entity<Ordenador>()
            .HasMany(e => e.Componentes)
            .WithOne(e => e.Ordenador)
            .HasForeignKey(e => e.OrdenadorId)
            .IsRequired(false);
    }

    public DbSet<Componente> Componentes => Set<Componente>();

    public DbSet<Ordenador> Ordenadores => Set<Ordenador>();

    public DbSet<Pedido> Pedidos => Set<Pedido>();
}
