using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaGCS.Models;
using System.Web.Mvc;

namespace SistemaGCS.Controllers
{
    public class Elemento_ConfiguracionController : Controller
    {
        // GET: Usuario
        private Elemento_Configuracion objElementoConfiguracion = new Elemento_Configuracion();

        private Fase objFase = new Fase();
       

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objElementoConfiguracion.Listar());
            }
            else
            {
                return View(objElementoConfiguracion.Buscar(criterio));
            }

        }

        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objElementoConfiguracion.Listar() : objElementoConfiguracion.Buscar(criterio));

        }
        //METODO AGREGAR ID
        public ActionResult Agregar(int id = 0)
        {
            ViewBag.Fase = objFase.Listar();//llenar el combox
            

            return View(id == 0 ? new Elemento_Configuracion() : objElementoConfiguracion.Obtener(id));
        }
        //METODO GUARDAR
        public ActionResult Guardar(Elemento_Configuracion model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Elemento_Configuracion/Index");
            }
            else
            {
                return View("~/Elemento_Configuracion/Agregar");
            }
        }


        public ActionResult IndexListar(int id)
        {
            ViewBag.listar = objElementoConfiguracion.Listarid(id);


            ViewBag.proyecto = objFase.Listaridnuevo(id);

            return View();

            

        }


        public ActionResult AgregarUnico(int id = 0)
        {

            ViewBag.Fase = objFase.Listar();
            

            ViewBag.Tipopro = objFase.Listar();

            ViewBag.idproyecto = id;

            return View(id < 100 ? new Elemento_Configuracion() : objElementoConfiguracion.Obtener(id));

        }
        public ActionResult GuardarUnico(Elemento_Configuracion model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                int numero = model.Id_fase;
                return Redirect("~/Elemento_Configuracion/IndexListar/" + numero);
            }
            else
            {
                return View("~/Miembro_Proyecto/Agregar");
            }
        }




    }
}