using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ECN.Models
{
    public partial class diagnosticosContext : DbContext
    {
        public diagnosticosContext()
        {
        }

        public diagnosticosContext(DbContextOptions<diagnosticosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cita> Citas { get; set; }
        public virtual DbSet<CitasDiagnostico> CitasDiagnosticos { get; set; }
        public virtual DbSet<Diagnostico> Diagnosticos { get; set; }
        public virtual DbSet<DiagnosticoSintoma> DiagnosticoSintomas { get; set; }
        public virtual DbSet<DiagnosticosTratamiento> DiagnosticosTratamientos { get; set; }
        public virtual DbSet<Enfermedade> Enfermedades { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<LaboratorioPrueba> LaboratorioPruebas { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }
        public virtual DbSet<PostmortemPrueba> PostmortemPruebas { get; set; }
        public virtual DbSet<PruebasLaboratorioResultado> PruebasLaboratorioResultados { get; set; }
        public virtual DbSet<PruebasPostmortmResultado> PruebasPostmortmResultados { get; set; }
        public virtual DbSet<Sintoma> Sintomas { get; set; }
        public virtual DbSet<Tratamiento> Tratamientos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=diagnosticos;user=root;password=user", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasKey(e => e.IdCita)
                    .HasName("PRIMARY");

                entity.ToTable("citas");

                entity.HasIndex(e => e.IdCita, "ID_Cita_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdEstado, "fk-estado_idx");

                entity.HasIndex(e => e.IdMedico, "fk-medico_idx");

                entity.HasIndex(e => e.IdPaciente, "fk-paciente_idx");

                entity.HasIndex(e => e.IdUsuario, "fk-secretaria_idx");

                entity.Property(e => e.IdCita).HasColumnName("ID_Cita");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Hora).HasColumnType("time");

                entity.Property(e => e.IdEstado).HasColumnName("ID_Estado");

                entity.Property(e => e.IdMedico).HasColumnName("ID_Medico");

                entity.Property(e => e.IdPaciente).HasColumnName("ID_Paciente");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-estado");

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.CitaIdMedicoNavigations)
                    .HasForeignKey(d => d.IdMedico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-medico");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-paciente");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.CitaIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-secretaria");
            });

            modelBuilder.Entity<CitasDiagnostico>(entity =>
            {
                entity.HasKey(e => new { e.IdCita, e.IdDiagnostico })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("citas_diagnosticos");

                entity.HasIndex(e => e.IdCita, "ID_Cita_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdDiagnostico, "fk-diagnostico_idx");

                entity.Property(e => e.IdCita).HasColumnName("ID_Cita");

                entity.Property(e => e.IdDiagnostico).HasColumnName("ID_Diagnostico");

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithOne(p => p.CitasDiagnostico)
                    .HasForeignKey<CitasDiagnostico>(d => d.IdCita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-cita");

                entity.HasOne(d => d.IdDiagnosticoNavigation)
                    .WithMany(p => p.CitasDiagnosticos)
                    .HasForeignKey(d => d.IdDiagnostico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-diagnostico");
            });

            modelBuilder.Entity<Diagnostico>(entity =>
            {
                entity.HasKey(e => e.IdDiagnostico)
                    .HasName("PRIMARY");

                entity.ToTable("diagnosticos");

                entity.HasIndex(e => e.IdDiagnostico, "ID_Diagnostico_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdEnfermedad, "fk-enfermedad_idx");

                entity.HasIndex(e => e.IdPruebaLab, "fk-pruebalab_idx");

                entity.HasIndex(e => e.IdPruebaPostMortem, "fk-pruebapost_idx");

                entity.Property(e => e.IdDiagnostico).HasColumnName("ID_Diagnostico");

                entity.Property(e => e.IdEnfermedad).HasColumnName("ID_Enfermedad");

                entity.Property(e => e.IdPruebaLab).HasColumnName("ID_PruebaLab");

                entity.Property(e => e.IdPruebaPostMortem).HasColumnName("ID_PruebaPostMortem");

                entity.HasOne(d => d.IdEnfermedadNavigation)
                    .WithMany(p => p.Diagnosticos)
                    .HasForeignKey(d => d.IdEnfermedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-enfermedad");

                entity.HasOne(d => d.IdPruebaLabNavigation)
                    .WithMany(p => p.Diagnosticos)
                    .HasForeignKey(d => d.IdPruebaLab)
                    .HasConstraintName("fk-pruebalab");

                entity.HasOne(d => d.IdPruebaPostMortemNavigation)
                    .WithMany(p => p.Diagnosticos)
                    .HasForeignKey(d => d.IdPruebaPostMortem)
                    .HasConstraintName("fk-pruebapost");
            });

            modelBuilder.Entity<DiagnosticoSintoma>(entity =>
            {
                entity.HasKey(e => new { e.IdDiagnostico, e.IdSintoma })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("diagnostico_sintomas");

                entity.HasIndex(e => e.IdSintoma, "sintoma_fk_idx");

                entity.Property(e => e.IdDiagnostico).HasColumnName("ID_Diagnostico");

                entity.Property(e => e.IdSintoma).HasColumnName("ID_Sintoma");

                entity.HasOne(d => d.IdDiagnosticoNavigation)
                    .WithMany(p => p.DiagnosticoSintomas)
                    .HasForeignKey(d => d.IdDiagnostico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("diagnostico_fk");

                entity.HasOne(d => d.IdSintomaNavigation)
                    .WithMany(p => p.DiagnosticoSintomas)
                    .HasForeignKey(d => d.IdSintoma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sintoma_fk");
            });

            modelBuilder.Entity<DiagnosticosTratamiento>(entity =>
            {
                entity.HasKey(e => new { e.IdTratamiento, e.IdDiagnostico })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("diagnosticos_tratamientos");

                entity.HasIndex(e => e.IdDiagnostico, "fk_diagnostico_idx");

                entity.Property(e => e.IdTratamiento).HasColumnName("ID_Tratamiento");

                entity.Property(e => e.IdDiagnostico).HasColumnName("ID_Diagnostico");

                entity.HasOne(d => d.IdDiagnosticoNavigation)
                    .WithMany(p => p.DiagnosticosTratamientos)
                    .HasForeignKey(d => d.IdDiagnostico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_diagnostico");

                entity.HasOne(d => d.IdTratamientoNavigation)
                    .WithMany(p => p.DiagnosticosTratamientos)
                    .HasForeignKey(d => d.IdTratamiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tratamiento");
            });

            modelBuilder.Entity<Enfermedade>(entity =>
            {
                entity.HasKey(e => e.IdEnfermedad)
                    .HasName("PRIMARY");

                entity.ToTable("enfermedades");

                entity.HasIndex(e => e.IdEnfermedad, "ID_Enfermedad_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre, "Nombre_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdEnfermedad).HasColumnName("ID_Enfermedad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PRIMARY");

                entity.ToTable("estados");

                entity.HasIndex(e => e.IdEstado, "ID_Estado_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdEstado).HasColumnName("ID_Estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<LaboratorioPrueba>(entity =>
            {
                entity.HasKey(e => e.IdPruebaLab)
                    .HasName("PRIMARY");

                entity.ToTable("laboratorio_pruebas");

                entity.HasIndex(e => e.Nombre, "Nombre_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdPruebaLab).HasColumnName("ID_PruebaLab");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente)
                    .HasName("PRIMARY");

                entity.ToTable("pacientes");

                entity.HasIndex(e => e.CorreoElectronico, "Correo_Electronico_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdPaciente, "ID_Paciente_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Telefono, "Telefono_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdPaciente).HasColumnName("ID_Paciente");

                entity.Property(e => e.CorreoElectronico)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Correo_Electronico");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Nacimiento");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<PostmortemPrueba>(entity =>
            {
                entity.HasKey(e => e.IdPruebaPost)
                    .HasName("PRIMARY");

                entity.ToTable("postmortem_pruebas");

                entity.HasIndex(e => e.IdPruebaPost, "ID_PruebaPost_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre, "Nombre_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdPruebaPost).HasColumnName("ID_PruebaPost");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PruebasLaboratorioResultado>(entity =>
            {
                entity.HasKey(e => e.IdPrueba)
                    .HasName("PRIMARY");

                entity.ToTable("pruebas_laboratorio_resultados");

                entity.HasIndex(e => e.IdPrueba, "ID_Prueba_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TipoPrueba, "fk-prueba_idx");

                entity.Property(e => e.IdPrueba).HasColumnName("ID_Prueba");

                entity.Property(e => e.DescripcionPrueba)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Descripcion_Prueba");

                entity.Property(e => e.FechaPuebra)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Puebra");

                entity.Property(e => e.Resultado)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.TipoPrueba).HasColumnName("Tipo_Prueba");

                entity.HasOne(d => d.TipoPruebaNavigation)
                    .WithMany(p => p.PruebasLaboratorioResultados)
                    .HasForeignKey(d => d.TipoPrueba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk-prueba");
            });

            modelBuilder.Entity<PruebasPostmortmResultado>(entity =>
            {
                entity.HasKey(e => e.IdResultado)
                    .HasName("PRIMARY");

                entity.ToTable("pruebas_postmortm_resultados");

                entity.HasIndex(e => e.IdResultado, "ID_Resultado_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdPrueba, "fk_prueba_idx");

                entity.Property(e => e.IdResultado).HasColumnName("ID_Resultado");

                entity.Property(e => e.DescripcionPrueba)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Descripcion_Prueba");

                entity.Property(e => e.FechaPrueba)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Fecha_Prueba");

                entity.Property(e => e.IdPrueba).HasColumnName("ID_Prueba");

                entity.Property(e => e.Resultado)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.HasOne(d => d.IdPruebaNavigation)
                    .WithMany(p => p.PruebasPostmortmResultados)
                    .HasForeignKey(d => d.IdPrueba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_prueba");
            });

            modelBuilder.Entity<Sintoma>(entity =>
            {
                entity.HasKey(e => e.IdSintoma)
                    .HasName("PRIMARY");

                entity.ToTable("sintomas");

                entity.HasIndex(e => e.IdSintoma, "ID_Sintoma_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre, "Nombre_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdSintoma).HasColumnName("ID_Sintoma");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Tratamiento>(entity =>
            {
                entity.HasKey(e => e.IdTratamiento)
                    .HasName("PRIMARY");

                entity.ToTable("tratamientos");

                entity.HasIndex(e => e.Nombre, "Descripcion_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdTratamiento, "ID_Tratamiento_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdTratamiento).HasColumnName("ID_Tratamiento");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.HasIndex(e => e.Correo, "Correo_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdUsuario, "ID_Usuario_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NombreUsuario, "NombreUsuario_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Telefono, "Telefono_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdUsuario)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Usuario");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.TipoUsuario)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Tipo_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
