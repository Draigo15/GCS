namespace SistemaGCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Elemento_Configuracion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Elemento_Configuracion()
        {
            Miembro_Elemento = new HashSet<Miembro_Elemento>();
        }

        [Key]
        public int Id_elementoconfiguracion { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(50, ErrorMessage = "El código no puede superar los 50 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La nomenclatura es obligatoria.")]
        [StringLength(50, ErrorMessage = "La nomenclatura no puede superar los 50 caracteres.")]
        public string Nomenclatura { get; set; }

        [StringLength(1)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Debe asignar una fase.")]
        public int Id_fase { get; set; }

        public virtual Fase Fase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Miembro_Elemento> Miembro_Elemento { get; set; }


        //Metodo Listar
        public List<Elemento_Configuracion> Listar()
        {
            var elementoConfiguracion = new List<Elemento_Configuracion>();
            try
            {
                using (var db = new ModelGCS())
                {
                    elementoConfiguracion = db.Elemento_Configuracion
                        .Include("Fase.Metodologia")
                        .Where(x => x.Estado == "A")
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return elementoConfiguracion;
        }

        //Metodo Obtener
        public Elemento_Configuracion Obtener(int id)
        {
            var elementoConfiguracion = new Elemento_Configuracion();
            try
            {
                using (var db = new ModelGCS())
                {
                    elementoConfiguracion = db.Elemento_Configuracion.Include("Fase").Where(x => x.Id_elementoconfiguracion == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return elementoConfiguracion;
        }
        //Metodo Buscar
        public List<Elemento_Configuracion> Buscar(string criterio)
        {
            var elementoConfiguracion = new List<Elemento_Configuracion>();

            try
            {
                using (var db = new ModelGCS())
                {
                    elementoConfiguracion = db.Elemento_Configuracion.Include("Fase").Where(x => x.Nombre.Contains(criterio) ||
                                x.Fase.Nombre.Contains(criterio))
                                .ToList();

                }
            }
            catch (Exception)
            {
                throw;
            }

            return elementoConfiguracion;

        }
        //Metodo Guardar
        public void Guardar()
        {
            try
            {
                using (var db = new ModelGCS())
                {
                    // Si es nuevo y no se asignó estado, marcar como activo
                    if (this.Id_elementoconfiguracion == 0 && string.IsNullOrEmpty(this.Estado))
                    {
                        this.Estado = "A";
                    }

                    if (this.Id_elementoconfiguracion > 0)
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
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.ToString());
                throw;
            }
        }
    

        //listarid
        public List<Elemento_Configuracion> Listarid(int id)
        {
            var miembro = new List<Elemento_Configuracion>();
            try
            {
                using (var db = new ModelGCS())
                {
                    miembro = db.Elemento_Configuracion
                        .Include("Fase.Metodologia")
                        .Where(x => x.Id_fase == id && x.Estado == "A")
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return miembro;
        }
    }
}
