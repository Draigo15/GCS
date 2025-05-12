using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaGCS.Models;
namespace SistemaGCS.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        private Rol objRol= new Rol();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objRol.Listar());
            }
            else
            {
                return View(objRol.Buscar(criterio));
            }
        }
        public ActionResult Visualizar(int id)
        {
            return View(objRol.Obtener(id));
        }
        public ActionResult Agregar(int id = 0)
        {
            return View(id == 0 ? new Rol() : objRol.Obtener(id));
        }

        public ActionResult Guardar(Rol model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Rol/index");
            }
            else
            {
                return View("~/Rol/Agregar");
            }
        }


        public ActionResult Eliminar(int id)
        {
            objRol.Id_rol = id;
            objRol.Eliminar();
            return Redirect("~/Rol");
        }

    }
}