using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaGCS.Models;
using System.Web.Mvc;

namespace SistemaGCS.Controllers
{
    public class ProyectoController : Controller
    {
        // GET: Usuario
        private  Proyecto objProyecto = new Proyecto();

        private Metodologia objMeto = new Metodologia();
        private Solicitud_Cambios objSC = new Solicitud_Cambios();


        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objProyecto.Listar());
            }
            else
            {
                return View(objProyecto.Buscar(criterio));
            }

        }
        public ActionResult Visualizar(int id)
        {
            var proyecto = objProyecto.Obtener(id);

            using (var db = new ModelGCS())
            {
                var ecsActivos = db.Elemento_Proyecto
                    .Where(ep => ep.Id_proyecto == id && ep.Estado == "A")
                    .Select(ep => new
                    {
                        ep.Id_elementoconfiguracion,
                        ep.Elemento_Configuracion.Nombre,
                        ep.Elemento_Configuracion.Codigo,
                        ep.Elemento_Configuracion.Nomenclatura,
                        FaseNombre = ep.Elemento_Configuracion.Fase.Nombre
                    })
                    .ToList();

                ViewBag.ECSActivos = ecsActivos;
            }

            return View(proyecto);
        }

        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objProyecto.Listar() : objProyecto.Buscar(criterio));

        }

        public ActionResult Agregar(int id = 0)
        {

            ViewBag.Ec = objMeto.Listar();//llenar el combox
            ViewBag.sol = objSC.ListarRespuesta(); //listar solo solicitudes aprobados
            return View(id == 0 ? new Proyecto() : objProyecto.Obtener(id));
        }

        [HttpPost]
        public ActionResult Guardar(Proyecto model, int[] ECSSeleccionados)
        {
            // Validación básica de modelo
            if (!ModelState.IsValid)
            {
                ViewBag.Ec = new Metodologia().Listar();
                ViewBag.sol = new Solicitud_Cambios().ListarRespuesta();
                return View("Agregar", model);
            }

            // Validar que haya al menos un ECS activo
            bool tieneECSActivo = false;
            foreach (var id in ECSSeleccionados ?? new int[0])
            {
                var estado = Request.Form[$"EstadoECS_{id}"];
                if (estado == "A")
                {
                    tieneECSActivo = true;
                    break;
                }
            }

            if (!tieneECSActivo)
            {
                ModelState.AddModelError("", "Debe seleccionar al menos un ECS con estado ACTIVO.");
                ViewBag.Ec = new Metodologia().Listar();
                ViewBag.sol = new Solicitud_Cambios().ListarRespuesta();
                return View("Agregar", model);
            }

            // Guardar el proyecto
            model.Guardar();

            // Guardar relaciones ECS - Proyecto
            using (var db = new ModelGCS())
            {
                foreach (var id in ECSSeleccionados)
                {
                    var estado = Request.Form[$"EstadoECS_{id}"] ?? "A"; // default A
                    var relacion = new Elemento_Proyecto
                    {
                        Id_proyecto = model.Id_proyecto,
                        Id_elementoconfiguracion = id,
                        Estado = estado
                    };

                    db.Elemento_Proyecto.Add(relacion);
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Estado()
        {

           
            return View();
        }
        public JsonResult ObtenerFasesConECS(int id_metodologia)
        {
            using (var db = new ModelGCS())
            {
                var fases = db.Fase
                    .Where(f => f.Id_metodologia == id_metodologia)
                    .Select(f => new
                    {
                        f.Id_fase,
                        f.Nombre,
                        ECS = f.Elemento_Configuracion
                            .Where(e => e.Estado == "A")
                            .Select(e => new
                            {
                                e.Id_elementoconfiguracion,
                                e.Nombre,
                                e.Codigo,
                                e.Nomenclatura
                            }).ToList()
                    }).ToList();

                return Json(fases, JsonRequestBehavior.AllowGet);
            }
        }

    }
}