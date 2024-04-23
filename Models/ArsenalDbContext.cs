using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HomeTeamWebSite.Models
{
    public partial class ArsenalDbContext : DbContext
    {
        public ArsenalDbContext()
            : base("name=ArsenalDbContext")
        {
        }

        public virtual DbSet<Biglietti> Biglietti { get; set; }
        public virtual DbSet<DettaglioVendita> DettaglioVendita { get; set; }
        public virtual DbSet<Ordini> Ordini { get; set; }
        public virtual DbSet<Partite> Partite { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoPosto> TipoPosto { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DettaglioVendita>()
                .Property(e => e.PrezzoUnitario)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Ordini>()
                .HasMany(e => e.DettaglioVendita)
                .WithRequired(e => e.Ordini)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Partite>()
                .HasMany(e => e.Biglietti)
                .WithRequired(e => e.Partite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TipoPosto>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TipoPosto>()
                .HasMany(e => e.Biglietti)
                .WithRequired(e => e.TipoPosto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Ordini)
                .WithRequired(e => e.Utenti)
                .WillCascadeOnDelete(false);
        }
    }
}
