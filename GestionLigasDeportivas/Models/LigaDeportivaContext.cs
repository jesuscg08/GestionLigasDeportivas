using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionLigasDeportivas.Models;

public partial class LigaDeportivaContext : DbContext
{
    public LigaDeportivaContext()
    {
    }

    public LigaDeportivaContext(DbContextOptions<LigaDeportivaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AsistenciaEntrenamiento> AsistenciaEntrenamientos { get; set; }

    public virtual DbSet<EntrenadorEquipo> EntrenadorEquipos { get; set; }

    public virtual DbSet<Entrenamiento> Entrenamientos { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Estadistica> Estadisticas { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<JugadorEquipo> JugadorEquipos { get; set; }

    public virtual DbSet<Liga> Ligas { get; set; }

    public virtual DbSet<PerfilEquipo> PerfilEquipos { get; set; }

    public virtual DbSet<PerfilJugador> PerfilJugadors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=LigaDeportiva;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AsistenciaEntrenamiento>(entity =>
        {
            entity.HasKey(e => e.AsistenciaId).HasName("PK__Asistenc__72710FA531199BA3");

            entity.ToTable("AsistenciaEntrenamiento");

            entity.Property(e => e.EstadoAsistencia).HasMaxLength(50);

            entity.HasOne(d => d.Entrenamiento).WithMany(p => p.AsistenciaEntrenamientos)
                .HasForeignKey(d => d.EntrenamientoId)
                .HasConstraintName("FK__Asistenci__Entre__47DBAE45");

            entity.HasOne(d => d.Usuario).WithMany(p => p.AsistenciaEntrenamientos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Asistenci__Usuar__48CFD27E");
        });

        modelBuilder.Entity<EntrenadorEquipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Entrenad__3214EC078EAB9703");

            entity.ToTable("EntrenadorEquipo");

            entity.HasOne(d => d.Equipo).WithMany(p => p.EntrenadorEquipos)
                .HasForeignKey(d => d.EquipoId)
                .HasConstraintName("FK__Entrenado__Equip__30F848ED");

            entity.HasOne(d => d.Usuario).WithMany(p => p.EntrenadorEquipos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Entrenado__Usuar__300424B4");
        });

        modelBuilder.Entity<Entrenamiento>(entity =>
        {
            entity.HasKey(e => e.EntrenamientoId).HasName("PK__Entrenam__F9610C43E078F49A");

            entity.ToTable("Entrenamiento");

            entity.Property(e => e.Detalle).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Notificacion).HasDefaultValue(false);
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.EquipoId).HasName("PK__Equipo__DE8A0BDF70404324");

            entity.ToTable("Equipo");

            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Liga).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.LigaId)
                .HasConstraintName("FK__Equipo__LigaId__29572725");
        });

        modelBuilder.Entity<Estadistica>(entity =>
        {
            entity.HasKey(e => e.EstadisticaId).HasName("PK__Estadist__5E78B40CE9BFB27B");

            entity.ToTable("Estadistica");

            entity.HasOne(d => d.Equipo).WithMany(p => p.Estadisticas)
                .HasForeignKey(d => d.EquipoId)
                .HasConstraintName("FK__Estadisti__Equip__3A81B327");

            entity.HasOne(d => d.Evento).WithMany(p => p.Estadisticas)
                .HasForeignKey(d => d.EventoId)
                .HasConstraintName("FK__Estadisti__Event__38996AB5");

            entity.HasOne(d => d.Jugador).WithMany(p => p.Estadisticas)
                .HasForeignKey(d => d.JugadorId)
                .HasConstraintName("FK__Estadisti__Jugad__398D8EEE");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.EventoId).HasName("PK__Evento__1EEB59215F6548B8");

            entity.ToTable("Evento");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Notificacion).HasDefaultValue(false);

            entity.HasOne(d => d.EquipoLocal).WithMany(p => p.EventoEquipoLocals)
                .HasForeignKey(d => d.EquipoLocalId)
                .HasConstraintName("FK__Evento__EquipoLo__34C8D9D1");

            entity.HasOne(d => d.EquipoVisitante).WithMany(p => p.EventoEquipoVisitantes)
                .HasForeignKey(d => d.EquipoVisitanteId)
                .HasConstraintName("FK__Evento__EquipoVi__35BCFE0A");
        });

        modelBuilder.Entity<JugadorEquipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JugadorE__3214EC070F0ABF31");

            entity.ToTable("JugadorEquipo");

            entity.HasOne(d => d.Equipo).WithMany(p => p.JugadorEquipos)
                .HasForeignKey(d => d.EquipoId)
                .HasConstraintName("FK__JugadorEq__Equip__2D27B809");

            entity.HasOne(d => d.Usuario).WithMany(p => p.JugadorEquipos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__JugadorEq__Usuar__2C3393D0");
        });

        modelBuilder.Entity<Liga>(entity =>
        {
            entity.HasKey(e => e.LigaId).HasName("PK__Liga__05567B370541782A");

            entity.ToTable("Liga");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<PerfilEquipo>(entity =>
        {
            entity.HasKey(e => e.PerfilEquipoId).HasName("PK__PerfilEq__FACAE84B820DC456");

            entity.ToTable("PerfilEquipo");

            entity.HasOne(d => d.Equipo).WithMany(p => p.PerfilEquipos)
                .HasForeignKey(d => d.EquipoId)
                .HasConstraintName("FK__PerfilEqu__Equip__412EB0B6");

            entity.HasOne(d => d.Liga).WithMany(p => p.PerfilEquipos)
                .HasForeignKey(d => d.LigaId)
                .HasConstraintName("FK__PerfilEqu__LigaI__4222D4EF");
        });

        modelBuilder.Entity<PerfilJugador>(entity =>
        {
            entity.HasKey(e => e.PerfilJugadorId).HasName("PK__PerfilJu__95C99AD90EA56348");

            entity.ToTable("PerfilJugador");

            entity.HasOne(d => d.Equipo).WithMany(p => p.PerfilJugadors)
                .HasForeignKey(d => d.EquipoId)
                .HasConstraintName("FK__PerfilJug__Equip__3E52440B");

            entity.HasOne(d => d.Usuario).WithMany(p => p.PerfilJugadors)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__PerfilJug__Usuar__3D5E1FD2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B868668E54");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A197FD66880").IsUnique();

            entity.Property(e => e.Contrasena).HasMaxLength(200);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TipoUsuario).HasMaxLength(50);
            entity.Property(e => e.TokenRecuperacion).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
