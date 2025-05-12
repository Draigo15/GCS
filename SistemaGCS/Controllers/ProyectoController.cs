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

        public ActionResult Guardar(Proyecto model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Proyecto/Index");
            }
            else
            {
                return View("~/Proyecto/Agregar");
            }
        }

        public ActionResult Estado()
        {

           
            return View();
        }
    }
}