using SistemaGCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SistemaGCS.Controllers
{
    public class CronogramaController : Controller
    {
        // GET: Cronograma
        private Cronograma objCrono = new Cronograma();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {

                return View(objCrono.Listar());
            }
            else
            {
                return View(objCrono.Buscar(criterio));
            }
        }


        public ActionResult Visualizar(int id)
        {
            return View(objCrono.Obtener(id));
        }
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objCrono.Listar() : objCrono.Buscar(criterio));

        }

        public ActionResult Agregar(int id = 0)
        {

            //ViewBag.Ec = objCrono.Listar();//llenar el combox
            //ViewBag.sol = objSC.ListarRespuesta(); //listar solo solicitudes aprobados
            return View(id == 0 ? new Cronograma() : objCrono.Obtener(id));
        }

        public ActionResult Guardar(Cronograma model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Cronograma/Index");
            }
            else
            {
                return View("~/Cronograma/Agregar");
            }
        }

    }
}