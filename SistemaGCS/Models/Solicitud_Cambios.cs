namespace SistemaGCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Solicitud_Cambios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solicitud_Cambios()
        {
            Proyecto = new HashSet<Proyecto>();
        }

        [Key]
        public int Id_solicitud_cambios { get; set; }

        [StringLength(50)]
        public string Fecha { get; set; }

        [StringLength(50)]
        public string Objetivo { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [StringLength(50)]
        public string Respuesta { get; set; }

        public int Id_solicitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyecto> Proyecto { get; set; }

        public virtual Solicitud Solicitud { get; set; }


        // listar solicitud
        public List<Solicitud_Cambios> Listar()
        {
            var sc = new List<Solicitud_Cambios>();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Solicitud_Cambios.Include("Solicitud").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sc;

        }

        // listar solicitud
        public List<Solicitud_Cambios> ListarRespuesta()
        {
            var sc = new List<Solicitud_Cambios>();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Solicitud_Cambios.Include("Solicitud").Where(x => x.Respuesta == "Positiva").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sc;

        }




        // obtener solicitud
        public Solicitud_Cambios Obtener(int id)
        {
            var sc = new Solicitud_Cambios();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Solicitud_Cambios.Include("Solicitud").Where(x => x.Id_solicitud_cambios == id)
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
        public List<Solicitud_Cambios> Buscar(string criterio)
        {
            var sc = new List<Solicitud_Cambios>();

            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Solicitud_Cambios.Include("Solicitud").Where(x => x.Objetivo.Contains(criterio) ||
                                x.Fecha.Contains(criterio) || x.Objetivo.Contains(criterio) || x.Respuesta.Contains(criterio) ||
                                x.Solicitud.Requerimiento.Contains(criterio)).ToList();

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
                    if (this.Id_solicitud_cambios > 0)
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
    }
}
