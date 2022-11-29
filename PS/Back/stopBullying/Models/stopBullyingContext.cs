using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace stopBullying.Models
{
    public partial class stopBullyingContext : DbContext
    {
        public stopBullyingContext()
        {
        }

        public stopBullyingContext(DbContextOptions<stopBullyingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Denuncia> Denuncias { get; set; }
        public virtual DbSet<Direccion> Direccions { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<NexoAlumno> NexoAlumnos { get; set; }
        public virtual DbSet<Prioridad> Prioridads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("User ID=mchayle; Password=12345; Server=localhost; Database=stopBullying; Integrated Security=true; Pooling=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Spanish_Argentina.1252");

            modelBuilder.Entity<Denuncia>(entity =>
            {
                entity.HasKey(e => e.IdDenuncia)
                    .HasName("idDenuncia");

                entity.HasIndex(e => e.IdEstado, "fki_I");

                entity.HasIndex(e => e.IdPrioridad, "fki_idPrioridad");

                entity.Property(e => e.IdDenuncia)
                    .HasColumnName("idDenuncia")
                    .HasDefaultValueSql("nextval('\"Denuncias_id_denuncia_seq\"'::regclass)");

                entity.Property(e => e.Contacto).HasColumnName("contacto");

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.Emergencia).HasColumnName("emergencia");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdDirector).HasColumnName("idDirector");

                entity.Property(e => e.IdEstado)
                    .HasColumnName("idEstado")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.IdNexo).HasColumnName("idNexo");

                entity.Property(e => e.IdPrioridad)
                    .HasColumnName("idPrioridad")
                    .HasDefaultValueSql("2");

                entity.Property(e => e.Imagen).HasColumnName("imagen");

                entity.Property(e => e.NombreAgresor).HasColumnName("nombreAgresor");

                entity.Property(e => e.NombreDenunciante).HasColumnName("nombreDenunciante");

                entity.Property(e => e.NombreObservador).HasColumnName("nombreObservador");

                entity.Property(e => e.Notas).HasColumnName("notas");

                entity.HasOne(d => d.IdDirectorNavigation)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.IdDirector)
                    .HasConstraintName("idDirector");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("idEstado");

                entity.HasOne(d => d.IdNexoNavigation)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.IdNexo)
                    .HasConstraintName("idNexo");

                entity.HasOne(d => d.IdPrioridadNavigation)
                    .WithMany(p => p.Denuncia)
                    .HasForeignKey(d => d.IdPrioridad)
                    .HasConstraintName("idPrioridad");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDirector)
                    .HasName("Direccion_pkey");

                entity.ToTable("Direccion");

                entity.Property(e => e.IdDirector)
                    .HasColumnName("idDirector")
                    .HasDefaultValueSql("nextval('\"Direccion_id_director_seq\"'::regclass)");

                entity.Property(e => e.Direccion1).HasColumnName("direccion");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Mail).HasColumnName("mail");

                entity.Property(e => e.NombreDirector).HasColumnName("nombreDirector");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.TelDirector).HasColumnName("telDirector");

                entity.Property(e => e.Usuario).HasColumnName("usuario");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("Estado_pkey");

                entity.ToTable("Estado");

                entity.Property(e => e.IdEstado)
                    .ValueGeneratedNever()
                    .HasColumnName("idEstado");

                entity.Property(e => e.Estado1).HasColumnName("estado");
            });

            modelBuilder.Entity<NexoAlumno>(entity =>
            {
                entity.HasKey(e => e.IdNexo)
                    .HasName("Nexo_alumnos_pkey");

                entity.Property(e => e.IdNexo)
                    .HasColumnName("idNexo")
                    .HasDefaultValueSql("nextval('\"Nexo_alumnos_id_nexo_seq\"'::regclass)");

                entity.Property(e => e.Direccion).HasColumnName("direccion");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Mail).HasColumnName("mail");

                entity.Property(e => e.NombreNexo).HasColumnName("nombreNexo");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.TelNexo).HasColumnName("telNexo");

                entity.Property(e => e.Usuario).HasColumnName("usuario");
            });

            modelBuilder.Entity<Prioridad>(entity =>
            {
                entity.HasKey(e => e.IdPrioridad)
                    .HasName("Prioridad_pkey");

                entity.ToTable("Prioridad");

                entity.Property(e => e.IdPrioridad)
                    .ValueGeneratedNever()
                    .HasColumnName("idPrioridad");

                entity.Property(e => e.Prioridad1).HasColumnName("prioridad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
