using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SistemaGCS.Models
{
    public partial class ModelGCS : DbContext
    {
        public ModelGCS()
            : base("name=ModelGCS3")
        {
        }


        public virtual DbSet<Cronograma> Cronograma { get; set; }
        public virtual DbSet<Elemento_Configuracion> Elemento_Configuracion { get; set; }
        public virtual DbSet<Fase> Fase { get; set; }
        public virtual DbSet<Metodologia> Metodologia { get; set; }
        public virtual DbSet<Miembro_Elemento> Miembro_Elemento { get; set; }
        public virtual DbSet<Miembro_Proyecto> Miembro_Proyecto { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<Solicitud_Cambios> Solicitud_Cambios { get; set; }
        public virtual DbSet<Tipo_Usuario> Tipo_Usuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cronograma>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cronograma>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Cronograma>()
                .Property(e => e.FechaInicio)
                .IsUnicode(false);

            modelBuilder.Entity<Cronograma>()
                .Property(e => e.Fechafin)
                .IsUnicode(false);

            modelBuilder.Entity<Elemento_Configuracion>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Elemento_Configuracion>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Elemento_Configuracion>()
                .Property(e => e.Nomenclatura)
                .IsUnicode(false);

            modelBuilder.Entity<Elemento_Configuracion>()
                .HasMany(e => e.Miembro_Elemento)
                .WithRequired(e => e.Elemento_Configuracion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fase>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Fase>()
                .HasMany(e => e.Elemento_Configuracion)
                .WithRequired(e => e.Fase)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Metodologia>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Metodologia>()
                .HasMany(e => e.Fase)
                .WithRequired(e => e.Metodologia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Metodologia>()
                .HasMany(e => e.Proyecto)
                .WithRequired(e => e.Metodologia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Miembro_Elemento>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Miembro_Elemento>()
                .Property(e => e.FechaInicio)
                .IsUnicode(false);

            modelBuilder.Entity<Miembro_Elemento>()
                .Property(e => e.Fechafin)
                .IsUnicode(false);

            modelBuilder.Entity<Miembro_Proyecto>()
                .HasMany(e => e.Miembro_Elemento)
                .WithRequired(e => e.Miembro_Proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.FechaInicio)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.FechaTermino)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Proyecto>()
                .HasMany(e => e.Miembro_Proyecto)
                .WithRequired(e => e.Proyecto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Miembro_Proyecto)
                .WithRequired(e => e.Rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.Requerimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .HasMany(e => e.Solicitud_Cambios)
                .WithRequired(e => e.Solicitud)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Solicitud_Cambios>()
                .Property(e => e.Fecha)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud_Cambios>()
                .Property(e => e.Objetivo)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud_Cambios>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud_Cambios>()
                .Property(e => e.Respuesta)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud_Cambios>()
                .HasMany(e => e.Proyecto)
                .WithRequired(e => e.Solicitud_Cambios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tipo_Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Tipo_Usuario>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Tipo_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Estado)
                .IsUnicode(false);
        }
    }
}


