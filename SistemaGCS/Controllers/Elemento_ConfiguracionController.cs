using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SistemaGCS.Models;

namespace SistemaGCS.Controllers
{
    public class Elemento_ConfiguracionController : Controller
    {
        private Elemento_Configuracion objElementoConfiguracion = new Elemento_Configuracion();
        private Fase objFase = new Fase();

        // INDEX GENERAL
        public ActionResult Index(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return View(objElementoConfiguracion.Listar());
            else
                return View(objElementoConfiguracion.Buscar(criterio));
        }

        public ActionResult Buscar(string criterio)
        {
            return View(string.IsNullOrWhiteSpace(criterio)
                ? objElementoConfiguracion.Listar()
                : objElementoConfiguracion.Buscar(criterio));
        }

        // FORMULARIO AGREGAR DESDE PANEL GENERAL
        public ActionResult Agregar(int id = 0)
        {
            ViewBag.Fase = objFase.Listar();
            return View(id == 0 ? new Elemento_Configuracion() : objElementoConfiguracion.Obtener(id));
        }

        public ActionResult Guardar(Elemento_Configuracion model)
        {
            if (ExisteElementoDuplicado(model.Nombre, model.Id_elementoconfiguracion))
                ModelState.AddModelError("Nombre", "Ya existe un elemento con ese nombre.");

            if (ModelState.IsValid)
            {
                model.Guardar();
                return RedirectToAction("Index");
            }

            ViewBag.Fase = objFase.Listar();
            return View("Agregar", model);
        }

        // LISTADO DE ECS DE UNA FASE
        public ActionResult IndexListar(int id)
        {
            ViewBag.listar = objElementoConfiguracion.Listarid(id);
            ViewBag.proyecto = objFase.Listaridnuevo(id);
            return View();
        }

        // FORMULARIO AGREGAR ÚNICO
        public ActionResult AgregarUnico(int id = 0)
        {
            ViewBag.Fase = objFase.Listar();
            ViewBag.idproyecto = id;
            return View(id < 100 ? new Elemento_Configuracion() : objElementoConfiguracion.Obtener(id));
        }

        public ActionResult GuardarUnico(Elemento_Configuracion model)
        {
            if (ExisteElementoDuplicado(model.Nombre, model.Id_elementoconfiguracion))
                ModelState.AddModelError("Nombre", "Ya existe un elemento con ese nombre.");

            if (ModelState.IsValid)
            {
                model.Guardar();
                return RedirectToAction("IndexListar", new { id = model.Id_fase });
            }

            ViewBag.Fase = objFase.Listar();
            return View("AgregarUnico", model);
        }

        // MODAL: GUARDAR DIRECTO (+ECS)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GuardarDesdeModal(int Id_fase, string Nombre, string Codigo, string Nomenclatura)
        {
            if (ExisteElementoDuplicado(Nombre))
            {
                TempData["mensaje"] = "Ya existe un elemento con ese nombre.";
                return RedirectToAction("IndexListar", new { id = Id_fase });
            }

            var nuevo = new Elemento_Configuracion
            {
                Id_fase = Id_fase,
                Nombre = Nombre,
                Codigo = Codigo,
                Nomenclatura = Nomenclatura
            };

            if (ModelState.IsValid)
            {
                nuevo.Guardar();
                return RedirectToAction("IndexListar", new { id = Id_fase });
            }

            TempData["mensaje"] = "Error al registrar el elemento.";
            return RedirectToAction("IndexListar", new { id = Id_fase });
        }

        // EDITAR
        public ActionResult Editar(int id)
        {
            var modelo = objElementoConfiguracion.Obtener(id);
            ViewBag.Fase = objFase.Listar();
            return View("AgregarUnico", modelo);
        }

        // DESACTIVAR
        public ActionResult Desactivar(int id)
        {
            var elemento = objElementoConfiguracion.Obtener(id);
            if (elemento != null)
            {
                elemento.Estado = "I";
                elemento.Guardar();
            }
            return RedirectToAction("IndexListar", new { id = elemento.Id_fase });
        }

        // VALIDACIÓN DUPLICADOS
        private bool ExisteElementoDuplicado(string nombre, int? idExcluir = null)
        {
            using (var db = new ModelGCS())
            {
                return db.Elemento_Configuracion
                         .Any(x => x.Nombre == nombre && x.Estado == "A" && x.Id_elementoconfiguracion != idExcluir);
            }
        }
    }
}
