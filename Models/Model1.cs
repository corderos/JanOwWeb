namespace WEB.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Klient> Klient { get; set; }
        public virtual DbSet<Wydarzenie> Wydarzenie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klient>()
                .Property(e => e.Imie)
                .IsUnicode(false);

            modelBuilder.Entity<Klient>()
                .Property(e => e.Nazwisko)
                .IsUnicode(false);

            modelBuilder.Entity<Klient>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Klient>()
                .Property(e => e.Haslo)
                .IsUnicode(false);

            modelBuilder.Entity<Klient>()
                .HasMany(e => e.Wydarzenie)
                .WithMany(e => e.Klient)
                .Map(m => m.ToTable("Klient_wydarzenie", "sql11176056").MapLeftKey("IdK").MapRightKey("IdWyd"));

            modelBuilder.Entity<Wydarzenie>()
                .Property(e => e.Nazwa)
                .IsUnicode(false);

            modelBuilder.Entity<Wydarzenie>()
                .Property(e => e.Adres)
                .IsUnicode(false);

            modelBuilder.Entity<Wydarzenie>()
                .Property(e => e.Prowadzacy)
                .IsUnicode(false);

            modelBuilder.Entity<Wydarzenie>()
                .Property(e => e.Temat)
                .IsUnicode(false);

            modelBuilder.Entity<Wydarzenie>()
                .Property(e => e.Opis)
                .IsUnicode(false);
        }
    }
}
