using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaGCS.Models;
using System.Web.Mvc;

namespace SistemaGCS.Controllers
{
    public class Solicitud_CambiosController : Controller
    {
        // GET: Usuario
        private Solicitud_Cambios objSC = new Solicitud_Cambios();
        private Solicitud objSolicitud = new Solicitud();
       
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objSC.Listar());
            }
            else
            {
                return View(objSC.Buscar(criterio));
            }
        }

        public ActionResult Visualizar(int id)
        {
            return View(objSC.Obtener(id));
        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objSC.Listar() : objSC.Buscar(criterio));

        }

        public ActionResult Agregar(int id = 0)
        {
            ViewBag.TipoSolicitud = objSolicitud.Listar();//llenar combobox de proyecto
            

            return View(id == 0 ? new Solicitud_Cambios() : objSC.Obtener(id));
        }

        public ActionResult Guardar(Solicitud_Cambios model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Solicitud_Cambios/index");
            }
            else
            {
                return View("~/Solicitud_Cambios/Agregar");
            }
        }

     
    }
}