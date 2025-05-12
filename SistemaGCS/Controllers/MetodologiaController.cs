using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGCS.Models;

namespace SistemaGCS.Controllers
{
    public class MetodologiaController : Controller
    {
        private Metodologia objMetodologia = new Metodologia();
        private Fase objFase = new Fase();

        // GET: /Metodologia/Index
        public ActionResult Index(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return View(objMetodologia.Listar());
            else
                return View(objMetodologia.Buscar(criterio));
        }

        public ActionResult Agregar(int id = 0)
        {
            return View(id == 0 ? new Metodologia() : objMetodologia.Obtener(id));
        }

        public ActionResult Guardar(Metodologia model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return RedirectToAction("Index");
            }
            return View("Agregar");
        }

        // NUEVA ACCIÓN: Visualizar metodología y sus fases
        public ActionResult Visualizar(int id)
        {
            var metodologia = objMetodologia.Obtener(id);
            if (metodologia == null)
                return HttpNotFound();

            metodologia.Fase = objFase.ListarPorMetodologia(id);
            return View(metodologia);
        }

        // NUEVA ACCIÓN: Agregar una fase desde modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarFase(int Id_metodologia, string NombreFase)
        {
            if (!string.IsNullOrWhiteSpace(NombreFase))
            {
                objFase.Nombre = NombreFase;
                objFase.Id_metodologia = Id_metodologia;
                objFase.Guardar();
            }
            return RedirectToAction("Visualizar", new { id = Id_metodologia });
        }
    }
}
