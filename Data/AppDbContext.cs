using EasytransitCaisse.Models;
using Microsoft.EntityFrameworkCore;

namespace EasytransitCaisse.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Caisse> Caisses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<JourneeCaisse> JourneesCaisses { get; set; }
        public DbSet<OperationCaisse> OperationsCaisses { get; set; }
        public DbSet<Facture> Factures { get; set; }

        public DbSet<LigneFacture> LignesFactures { get; set; }
    }
}