using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaGCS.Models;
using System.Web.Mvc;

namespace SistemaGCS.Controllers
{
    public class SolicitudController : Controller
    {
        // GET: Usuario
        
        private Solicitud objSolicitud = new Solicitud();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objSolicitud.Listar());
            }
            else
            {
                return View(objSolicitud.Buscar(criterio));
            }
        }

        public ActionResult Visualizar(int id)
        {
            return View(objSolicitud.Obtener(id));
        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objSolicitud.Listar() : objSolicitud.Buscar(criterio));

        }

        public ActionResult Agregar(int id = 0)
        {
            
            return View(id == 0 ? new Solicitud() : objSolicitud.Obtener(id));
        }

        public ActionResult Guardar(Solicitud model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Solicitud/Index");
            }
            else
            {
                return View("~/Solicitud/Agregar");
            }
        }
    }
}