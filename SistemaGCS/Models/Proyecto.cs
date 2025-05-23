namespace SistemaGCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Proyecto")]
    public partial class Proyecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proyecto()
        {
            Miembro_Proyecto = new HashSet<Miembro_Proyecto>();
        }

        [Key]
        public int Id_proyecto { get; set; }
        [Required(ErrorMessage = "El campo C�digo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El C�digo no debe exceder los 50 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Nombre no debe exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Fecha de Inicio es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha inv�lido.")]
        public string FechaInicio { get; set; }

        [Required(ErrorMessage = "La Fecha de T�rmino es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de fecha inv�lido.")]
        public string FechaTermino { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Estado no debe exceder los 50 caracteres.")]
        public string Estado { get; set; }

        public int Id_metodologia { get; set; }

        public int Id_solicitud_cambios { get; set; }

        public virtual Metodologia Metodologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Miembro_Proyecto> Miembro_Proyecto { get; set; }
        public virtual ICollection<Elemento_Proyecto> Elemento_Proyecto { get; set; }


        public virtual Solicitud_Cambios Solicitud_Cambios { get; set; }

        // listar proyecto
        public List<Proyecto> Listar()
        {
            var sc = new List<Proyecto>();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Proyecto.Include("Metodologia").Include("Solicitud_Cambios").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sc;

        }
        // obtener solicitud
        public Proyecto Obtener(int id)
        {
            var sc = new Proyecto();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Proyecto.Include("Metodologia").Include("Solicitud_Cambios").Where(x => x.Id_proyecto == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sc;
        }

        // buscar solicitud
        public List<Proyecto> Buscar(string criterio)
        {
            var sc = new List<Proyecto>();

            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Proyecto.Include("Metodologia").Include("Solicitud_Cambios").Where(x => x.Nombre.Contains(criterio) ||
                                x.Codigo.Contains(criterio) ||
                                x.FechaInicio.Contains(criterio) ||
                                x.FechaTermino.Contains(criterio) ||
                                x.Estado.Contains(criterio))
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return sc;
        }
        //guardar solicitud
        public void Guardar()
        {
            try
            {
                using (var db = new ModelGCS())
                {
                    if (this.Id_proyecto > 0)
                    {
                        db.Entry(this).State = EntityState.Modified; //existe
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added; //nuevo registro
                    }
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //public Proyecto Listarid(int id)
        //{
        //    var miembro = new List<Proyecto>();
        //    try
        //    {
        //        using (var db = new ModelGCS())
        //        {
        //            miembro = db.Proyecto
        //                .Include("Metodologia")
        //                .Include("Solicitud_Cambios")
        //                .Where(x => x.Id_proyecto == id).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return miembro;
        //}


        public Proyecto Listarid(int id)
        {
            var sc = new Proyecto();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Proyecto.Include("Metodologia").Include("Solicitud_Cambios").Where(x => x.Id_proyecto == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sc;
        }
    }
}
