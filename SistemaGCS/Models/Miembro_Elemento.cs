namespace SistemaGCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Miembro_Elemento
    {
        [Key]
        public int Id_miembroelemento { get; set; }

        public int Id_miembro { get; set; }

        public int Id_elementoconfiguracion { get; set; }

        [StringLength(50)]
        public string Url { get; set; }

        [StringLength(50)]
        public string FechaInicio { get; set; }

        [StringLength(50)]
        public string Fechafin { get; set; }

        public int? Porcentaje { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        public virtual Elemento_Configuracion Elemento_Configuracion { get; set; }

        public virtual Miembro_Proyecto Miembro_Proyecto { get; set; }

        //Metodo Listar
        public List<Miembro_Elemento> Listar()
        {
            var fase = new List<Miembro_Elemento>();
            try
            {
                using (var db = new ModelGCS())
                {
                    fase = db.Miembro_Elemento.Include("Elemento_Configuracion").Include("Miembro_Proyecto.Usuario").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return fase;

        }
        //Metodo Obtener
        public Miembro_Elemento Obtener(int id)
        {
            var fase = new Miembro_Elemento();
            try
            {
                using (var db = new ModelGCS())
                {
                    fase = db.Miembro_Elemento.Include("Elemento_Configuracion").Include("Miembro_Proyecto").Where(x => x.Id_miembroelemento == id)
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
        public List<Miembro_Elemento> Buscar(string criterio)
        {
            var fase = new List<Miembro_Elemento>();

            try
            {
                using (var db = new ModelGCS())
                {
                    fase = db.Miembro_Elemento.Include("Elemento_Configuracion").Include("Miembro_Proyecto").Where(x => x.Elemento_Configuracion.Nomenclatura.Contains(criterio) ||
                                x.Miembro_Proyecto.Id_usuario == Convert.ToInt32(criterio))
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
                    if (this.Id_miembroelemento > 0)
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
        public List<Miembro_Elemento> Listarid(int id)
        {
            var miembro = new List<Miembro_Elemento>();
            try
            {
                using (var db = new ModelGCS())
                {
                    miembro = db.Miembro_Elemento.Include("Elemento_Configuracion").Include("Miembro_Proyecto.Usuario")
                         .Where(x => x.Id_elementoconfiguracion == id).ToList();
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
