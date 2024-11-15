using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PROYECTOISW.Models;

public partial class ProyectoiswContext : DbContext
{
    public ProyectoiswContext()
    {
    }

    public ProyectoiswContext(DbContextOptions<ProyectoiswContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Favorito> Favoritos { get; set; }

    public virtual DbSet<Imagene> Imagenes { get; set; }

    public virtual DbSet<Propiedade> Propiedades { get; set; }

    public virtual DbSet<Reseña> Reseñas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PROYECTOISW;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Creacion");
            entity.Property(e => e.IdCitas).HasColumnName("Id_Citas");
            entity.Property(e => e.IdPropiedad).HasColumnName("Id_Propiedad");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

            entity.HasOne(d => d.IdPropiedadNavigation).WithMany()
                .HasForeignKey(d => d.IdPropiedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__Id_Propie__6EF57B66");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Citas__Id_Usuari__6FE99F9F");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IdFavorito).HasColumnName("Id_Favorito");
            entity.Property(e => e.IdPropiedad).HasColumnName("Id_Propiedad");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

            entity.HasOne(d => d.IdPropiedadNavigation).WithMany()
                .HasForeignKey(d => d.IdPropiedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favoritos__Id_Pr__5070F446");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favoritos__Id_Pr__4F7CD00D");
        });

        modelBuilder.Entity<Imagene>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IdFoto).HasColumnName("Id_Foto");
            entity.Property(e => e.IdPropiedad).HasColumnName("Id_Propiedad");

            entity.HasOne(d => d.IdPropiedadNavigation).WithMany()
                .HasForeignKey(d => d.IdPropiedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Imagenes__Imagen__4D94879B");
        });

        modelBuilder.Entity<Propiedade>(entity =>
        {
            entity.HasKey(e => e.IdPropiedad).HasName("PK__Propieda__5D2875B3EED636A3");

            entity.Property(e => e.IdPropiedad).HasColumnName("Id_Propiedad");
            entity.Property(e => e.CondicionesEspeciales)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Condiciones_Especiales");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(600)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FechaPublicacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Publicacion");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.NumeroBaños)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Numero_Baños");
            entity.Property(e => e.NumeroHabitaciones)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Numero_Habitaciones");
            entity.Property(e => e.PrecioRenta).HasColumnName("Precio_Renta");
            entity.Property(e => e.Sevicios)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Superficie)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.TipoPropiedad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tipo_Propiedad");
            entity.Property(e => e.Titulo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Propiedades)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Propiedad__Fecha__4BAC3F29");
        });

        modelBuilder.Entity<Reseña>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Comentario)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnName("Fecha_Creacion");
            entity.Property(e => e.IdPropiedad).HasColumnName("Id_Propiedad");
            entity.Property(e => e.IdReseña).HasColumnName("Id_Reseña");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

            entity.HasOne(d => d.IdPropiedadNavigation).WithMany()
                .HasForeignKey(d => d.IdPropiedad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reseñas__Id_Prop__534D60F1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reseñas__Id_Usua__52593CB8");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__63C76BE2DEA1A1A6");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("Correo_Electronico");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("Nombre_Completo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
