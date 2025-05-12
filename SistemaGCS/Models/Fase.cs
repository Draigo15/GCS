namespace SistemaGCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Fase")]
    public partial class Fase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fase()
        {
            Elemento_Configuracion = new HashSet<Elemento_Configuracion>();
        }

        [Key]
        public int Id_fase { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public int Id_metodologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Elemento_Configuracion> Elemento_Configuracion { get; set; }

        public virtual Metodologia Metodologia { get; set; }

        //Metodo Listar
        public List<Fase> Listar()
        {
            var fase = new List<Fase>();
            try
            {
                using (var db = new ModelGCS())
                {
                    fase = db.Fase.Include("Metodologia").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fase;

        }
        //Metodo Obtener
        public Fase Obtener(int id)
        {
            var fase = new Fase();
            try
            {
                using (var db = new ModelGCS())
                {
                    fase = db.Fase.Include("Metodologia").Where(x => x.Id_fase == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fase;
        }
        //Metodo Buscar
        public List<Fase> Buscar(string criterio)
        {
            var fase = new List<Fase>();

            try
            {
                using (var db = new ModelGCS())
                {
                    fase = db.Fase.Include("Metodologia").Where(x => x.Nombre.Contains(criterio) ||
                                x.Metodologia.Nombre.Contains(criterio))
                                .ToList();

                }
            }
            catch (Exception)
            {
                throw;
            }

            return fase;

        }
        //Metodo Guardar
        public void Guardar()
        {
            try
            {
                using (var db = new ModelGCS())
                {
                    if (this.Id_fase > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //listarid
        public List<Fase> ListarPorMetodologia(int id_metodologia)
        {
            var fases = new List<Fase>();
            try
            {
                using (var db = new ModelGCS())
                {
                    fases = db.Fase
                        .Include("Metodologia")
                        .Where(x => x.Id_metodologia == id_metodologia)
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return fases;
        }


        //otro metodo

        public Fase Listaridnuevo(int id)
        {
            var sc = new Fase();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Fase.Include("Metodologia").Where(x => x.Id_fase == id)
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
