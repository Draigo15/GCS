namespace SistemaGCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Solicitud")]
    public partial class Solicitud
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solicitud()
        {
            Solicitud_Cambios = new HashSet<Solicitud_Cambios>();
        }

        [Key]
        public int Id_solicitud { get; set; }

        [StringLength(50)]
        public string Requerimiento { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud_Cambios> Solicitud_Cambios { get; set; }

        // listar solicitud
        public List<Solicitud> Listar()
        {
            var sc = new List<Solicitud>();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Solicitud.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sc;

        }

        // obtener solicitud
        public Solicitud Obtener(int id)
        {
            var sc = new Solicitud();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Solicitud.Where(x => x.Id_solicitud == id)
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
        public List<Solicitud> Buscar(string criterio)
        {
            var sc = new List<Solicitud>();

            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Solicitud.Where(x => x.Requerimiento.Contains(criterio) ||
                                x.Descripcion.Contains(criterio)).ToList();

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
                    if (this.Id_solicitud > 0)
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
