using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SistemaGCS.Models;

namespace SistemaGCS.Controllers
{
    public class Miembro_ProyectoController : Controller
    {
        private Proyecto objProyecto = new Proyecto();
        private Rol objRol = new Rol();
        private Usuario objUsuario = new Usuario();
        private Miembro_Proyecto objMiembro = new Miembro_Proyecto();

        // Lista general de miembros
        public ActionResult Index(string criterio)
        {
            if (string.IsNullOrWhiteSpace(criterio))
                return View(objMiembro.Listar());
            else
                return View(objMiembro.Buscar(criterio));
        }

        // Listar miembros por proyecto
        public ActionResult IndexListar(int id)
        {
            ViewBag.listar = objMiembro.Listarid(id);
            ViewBag.proyecto = objProyecto.Listarid(id);
            return View();
        }

        // Vista para agregar miembro a un proyecto específico
        public ActionResult AgregarUnico(int id = 0)
        {
            ViewBag.Tipousua = objUsuario.Listar();
            ViewBag.Tiporol = objRol.Listar();
            ViewBag.Tipopro = objProyecto.Listar();
            ViewBag.idproyecto = id;
            ViewBag.proyecto = objProyecto.Listarid(id);

            return View(id < 100 ? new Miembro_Proyecto() : objMiembro.Obtener(id));
        }

        // Guardar miembro único con proyecto preseleccionado
        public ActionResult GuardarUnico(Miembro_Proyecto model)
        {
            if (ModelState.IsValid)
            {
                // Validar si ya existe ese usuario en ese proyecto
                var existe = objMiembro.Listarid(model.Id_proyecto)
                                .Any(x => x.Id_usuario == model.Id_usuario && x.Id_miembro != model.Id_miembro);

                if (existe)
                {
                    ModelState.AddModelError("", "Este usuario ya está asignado al proyecto.");
                    ViewBag.Tipousua = objUsuario.Listar();
                    ViewBag.Tiporol = objRol.Listar();
                    ViewBag.Tipopro = objProyecto.Listar();
                    ViewBag.idproyecto = model.Id_proyecto;
                    return View("AgregarUnico", model);
                }

                model.Guardar();
                return RedirectToAction("IndexListar", new { id = model.Id_proyecto });
            }

            ViewBag.Tipousua = objUsuario.Listar();
            ViewBag.Tiporol = objRol.Listar();
            ViewBag.Tipopro = objProyecto.Listar();
            ViewBag.idproyecto = model.Id_proyecto;
            return View("AgregarUnico", model);
        }

        // Vista general para edición
        public ActionResult Agregar(int id = 0)
        {
            ViewBag.Tipousua = objUsuario.Listar();
            ViewBag.Tiporol = objRol.Listar();
            ViewBag.Tipopro = objProyecto.Listar();
            return View(id == 0 ? new Miembro_Proyecto() : objMiembro.Obtener(id));
        }

        // Guardar edición general
        public ActionResult Guardar(Miembro_Proyecto model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return RedirectToAction("Index");
            }

            ViewBag.Tipousua = objUsuario.Listar();
            ViewBag.Tiporol = objRol.Listar();
            ViewBag.Tipopro = objProyecto.Listar();
            return View("Agregar", model);
        }

        // (Opcional) Desactivar miembro
        public ActionResult Desactivar(int id)
        {
            var miembro = objMiembro.Obtener(id);
            if (miembro != null)
            {
                // Implementar lógica si agregas campo Estado
                // miembro.Estado = "I";
                miembro.Eliminar(); // Elimina de forma directa (o cambia por lógica de estado)
            }
            return RedirectToAction("IndexListar", new { id = miembro.Id_proyecto });
        }
        public ActionResult Eliminar(int id)
        {
            var miembro = objMiembro.Obtener(id);
            if (miembro != null)
            {
                objMiembro.Eliminar();
                return RedirectToAction("IndexListar", new { id = miembro.Id_proyecto });
            }
            return HttpNotFound();
        }

    }
}
