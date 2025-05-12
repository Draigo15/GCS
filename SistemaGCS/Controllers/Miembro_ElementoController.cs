using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaGCS.Models;
using System.Web.Mvc;

namespace SistemaGCS.Controllers
{
    public class Miembro_ElementoController : Controller
    {
        // GET: Miembro_Proyecto
        private Miembro_Elemento objME = new Miembro_Elemento();

        private Elemento_Configuracion objEC = new Elemento_Configuracion();
        private Miembro_Proyecto objMiembros = new Miembro_Proyecto();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objME.Listar());
            }
            else
            {
                return View(objME.Buscar(criterio));
            }

        }
        public ActionResult Agregar(int id = 0)
        {

            ViewBag.TipoEC = objEC.Listar();
            ViewBag.Tipomiembros = objMiembros.Listar();
            
            return View(id == 0 ? new Miembro_Elemento() : objME.Obtener(id));

        }
        public ActionResult Guardar(Miembro_Elemento model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Miembro_Elemento/Index");
            }
            else
            {
                return View("~/Miembro_Elemento/Agregar");
            }
        }



        // NUEVOS METODOS

        ///NUEVOS METODOS
        public ActionResult IndexListar(int id)
        {
            ViewBag.listar = objME.Listarid(id);

            return View();

        }

        //agregar nuevo
        public ActionResult IndexListarregistrar(int id)
        {
            ViewBag.listar = objME.Listarid(id);

            ViewBag.proyecto = objME.Listarid(id);

            return View();

        }


        public ActionResult AgregarUnico(int id)
        {
            
            ViewBag.TipoEC = objME.Listarid(id);

            ViewBag.TipoMiembros = objMiembros.Listar();

            ViewBag.idproyecto = id;

            return View(id < 100 ? new Miembro_Elemento() : objME.Obtener(id));

        }
        public ActionResult GuardarUnico(Miembro_Elemento model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                int numero = model.Id_elementoconfiguracion;
                return Redirect("~/Miembro_Elemento/IndexListarregistrar/" + numero);
            }
            else
            {
                return View("~/Miembro_Elemento/AgregarUnico");
            }
        }



    }
}