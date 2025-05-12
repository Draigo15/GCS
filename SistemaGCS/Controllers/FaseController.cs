using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaGCS.Models;
using System.Web.Mvc;

namespace SistemaGCS.Controllers
{
    public class FaseController : Controller
    {
        private Fase objFase = new Fase();


        private Metodologia objMetodologia = new Metodologia();
        // GET: Semestre
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objFase.Listar());
            }
            else
            {
                return View(objFase.Buscar(criterio));
            }
        }

        public ActionResult Visualizar(int id)
        {
            return View(objFase.Obtener(id));
        }

        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.Metodologia = objMetodologia.Listar();

            return View(id == 0 ? new Fase() // Agregarmos un nuevo objeto
                : objFase.Obtener(id) //Devuelve el id del objeto
                );
        }

        public ActionResult Guardar(Fase model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Fase/index");
            }
            else
            {
                return View("~/Fase/AgregarEditar");
            }
        }


        ///NUEVOS METODOS
        public ActionResult IndexListar(int id)
        {
            ViewBag.listar = objFase.Listarid(id);

            return View();
                        
        }










        public ActionResult AgregarEditarListar(int id = 0)
        {
            ViewBag.Metodologia = objMetodologia.Listar();

            return View(id == 0 ? new Fase() // Agregarmos un nuevo objeto
                : objFase.Obtener(id) //Devuelve el id del objeto
                );
        }

        public ActionResult GuardarListar(Fase model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Fase/index");
            }
            else
            {
                return View("~/Fase/AgregarEditarListar");
            }
        }
    }
}