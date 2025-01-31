using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartEnergyAPI.Models;

public partial class PlataformaEnergeticaContext : DbContext
{
    public PlataformaEnergeticaContext()
    {
    }

    public PlataformaEnergeticaContext(DbContextOptions<PlataformaEnergeticaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alerta> Alertas { get; set; }

    public virtual DbSet<Dispositivo> Dispositivos { get; set; }

    public virtual DbSet<MetasAhorro> MetasAhorros { get; set; }

    public virtual DbSet<Recomendacione> Recomendaciones { get; set; }

    public virtual DbSet<RegistroConsumo> RegistroConsumos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VistaAlertasPendiente> VistaAlertasPendientes { get; set; }

    public virtual DbSet<VistaConsumoMensual> VistaConsumoMensuals { get; set; }

    public virtual DbSet<VistaMetasAlcanzada> VistaMetasAlcanzadas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ALENDESKTOP\\DEVELOPERSQL;Initial Catalog=PlataformaEnergetica;trusted_connection=yes;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alerta>(entity =>
        {
            entity.HasKey(e => e.AlertaId).HasName("PK__Alertas__D9EF47E509D5D563");

            entity.HasIndex(e => e.UsuarioId, "idx_UsuarioID_Alertas");

            entity.Property(e => e.AlertaId).HasColumnName("AlertaID");
            entity.Property(e => e.FechaAlerta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Mensaje).HasMaxLength(500);
            entity.Property(e => e.Resuelta).HasDefaultValue(false);
            entity.Property(e => e.TipoAlerta).HasMaxLength(100);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Alerta)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Alertas__Usuario__5535A963");
        });

        modelBuilder.Entity<Dispositivo>(entity =>
        {
            entity.HasKey(e => e.DispositivoId).HasName("PK__Disposit__724C2641D1E3E567");

            entity.Property(e => e.DispositivoId).HasColumnName("DispositivoID");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValue("Activo");
            entity.Property(e => e.FechaInstalacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Tipo).HasMaxLength(50);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Dispositi__Usuar__3E52440B");
        });

        modelBuilder.Entity<MetasAhorro>(entity =>
        {
            entity.HasKey(e => e.MetaId).HasName("PK__MetasAho__60EE57F875D13BA1");

            entity.ToTable("MetasAhorro");

            entity.HasIndex(e => e.UsuarioId, "idx_UsuarioID_MetasAhorro");

            entity.Property(e => e.MetaId).HasColumnName("MetaID");
            entity.Property(e => e.Alcanzado).HasDefaultValue(false);
            entity.Property(e => e.ConsumoActual)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.MetaConsumo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.MetasAhorros)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__MetasAhor__Usuar__5070F446");
        });

        modelBuilder.Entity<Recomendacione>(entity =>
        {
            entity.HasKey(e => e.RecomendacionId).HasName("PK__Recomend__104CC1EECADE6389");

            entity.Property(e => e.RecomendacionId).HasColumnName("RecomendacionID");
            entity.Property(e => e.AccionSugerida).HasMaxLength(255);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaRecomendacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TipoRecomendacion).HasMaxLength(255);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Recomendaciones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Recomenda__Usuar__4BAC3F29");
        });

        modelBuilder.Entity<RegistroConsumo>(entity =>
        {
            entity.HasKey(e => e.ConsumoId).HasName("PK__Registro__206D9CC64A374396");

            entity.ToTable("RegistroConsumo");

            entity.HasIndex(e => e.FechaRegistro, "idx_FechaRegistro");

            entity.HasIndex(e => e.UsuarioId, "idx_UsuarioID");

            entity.Property(e => e.ConsumoId).HasColumnName("ConsumoID");
            entity.Property(e => e.ConsumoKwh).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DispositivoId).HasColumnName("DispositivoID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Dispositivo).WithMany(p => p.RegistroConsumos)
                .HasForeignKey(d => d.DispositivoId)
                .HasConstraintName("FK__RegistroC__Dispo__47DBAE45");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RegistroConsumos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__RegistroC__Usuar__46E78A0C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7986D7731F1");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105340737C8B2").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TipoUsuario).HasMaxLength(20);
        });

        modelBuilder.Entity<VistaAlertasPendiente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaAlertasPendientes");

            entity.Property(e => e.FechaAlerta).HasColumnType("datetime");
            entity.Property(e => e.Mensaje).HasMaxLength(500);
            entity.Property(e => e.NombreUsuario).HasMaxLength(100);
            entity.Property(e => e.TipoAlerta).HasMaxLength(100);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<VistaConsumoMensual>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaConsumoMensual");

            entity.Property(e => e.ConsumoUltimos30Dias).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.Dispositivo).HasMaxLength(100);
            entity.Property(e => e.NombreUsuario).HasMaxLength(100);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
        });

        modelBuilder.Entity<VistaMetasAlcanzada>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VistaMetasAlcanzadas");

            entity.Property(e => e.ConsumoActual).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EstadoMeta)
                .HasMaxLength(17)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.MetaConsumo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NombreUsuario).HasMaxLength(100);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
