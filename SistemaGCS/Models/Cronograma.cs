
namespace SistemaGCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Cronograma")]
    public partial class Cronograma
    {
        [Key]
        public int Id_cronograma { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [StringLength(50)]
        public string FechaInicio { get; set; }

        [StringLength(50)]
        public string Fechafin { get; set; }




        // listar proyecto
        public List<Cronograma> Listar()
        {
            var sc = new List<Cronograma>();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Cronograma.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sc;

        }
        // obtener solicitud
        public Cronograma Obtener(int id)
        {
            var sc = new Cronograma();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Cronograma.Where(x => x.Id_cronograma == id)
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
        public List<Cronograma> Buscar(string criterio)
        {
            var sc = new List<Cronograma>();

            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Cronograma.Include("Metodologia").Include("Solicitud_Cambios").Where(x => x.Nombre.Contains(criterio) ||
                                x.Nombre.Contains(criterio) ||
                                x.FechaInicio.Contains(criterio) ||
                                x.Fechafin.Contains(criterio) ||
                                x.Descripcion.Contains(criterio))
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
                    if (this.Id_cronograma > 0)
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


        public Cronograma Listarid(int id)
        {
            var sc = new Cronograma();
            try
            {
                using (var db = new ModelGCS())
                {
                    sc = db.Cronograma.Where(x => x.Id_cronograma == id)
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
