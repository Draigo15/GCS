using SistemaGCS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaGCS.Controllers
{
    public class DesarrolladorDashboardController : Controller
    {
        private ModelGCS db = new ModelGCS();

        public ActionResult Index()
        {
            if (Session["Id_usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int idUsuario = int.Parse(Session["Id_usuario"].ToString());

            // Obtener proyectos del desarrollador
            var proyectos = db.Miembro_Proyecto
                              .Where(mp => mp.Id_usuario == idUsuario)
                              .Select(mp => mp.Proyecto)
                              .Distinct()
                              .ToList();

            // Obtener elementos de configuración asignados al usuario (por su ID de miembro)
            var elementos = db.Miembro_Proyecto
                              .Where(mp => mp.Id_usuario == idUsuario)
                              .Join(db.Miembro_Elemento,
                                    mp => mp.Id_miembro,
                                    me => me.Id_miembro,
                                    (mp, me) => me.Elemento_Configuracion)
                              .Include(ec => ec.Fase) // Incluir la Fase del EC
                              .Distinct()
                              .ToList();

            ViewBag.MisProyectos = proyectos;
            ViewBag.MisElementos = elementos;

            return View();
        }
    }
}