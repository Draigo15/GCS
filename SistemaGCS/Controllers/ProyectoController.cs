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
            return View(objProyecto.Obtener(id));
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
            if (ModelState.IsValid)
            {
                model.Guardar();

                // Relación Proyecto - ECS (a través de la fase → Miembro_Elemento o tu tabla correspondiente)
                // Aquí puedes guardar los ECSSeleccionados como se necesite

                return Redirect("~/Proyecto/Index");
            }

            ViewBag.Ec = objMeto.Listar();
            ViewBag.sol = objSC.ListarRespuesta();
            return View("~/Proyecto/Agregar", model);
        }

        public ActionResult Estado()
        {

           
            return View();
        }
        public ActionResult ObtenerFasesConECS(int id_metodologia)
        {
            var fases = new Fase().ListarPorMetodologia(id_metodologia); // Asegúrate que este método existe
            var elementos = new Elemento_Configuracion().Listar(); // Ya existente

            ViewBag.Elementos = elementos;
            return PartialView("_FasesConECS", fases); // Vista parcial que muestra las fases y sus ECS
        }
    }
}