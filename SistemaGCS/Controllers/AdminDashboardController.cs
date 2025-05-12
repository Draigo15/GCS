using SistemaGCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SistemaGCS.Controllers
{
    public class AdminDashboardController : Controller
    {
        private ModelGCS db = new ModelGCS();

        public ActionResult Index()
        {
            // Cuadros principales
            ViewBag.TotalUsuarios = db.Usuario.Count();
            ViewBag.TotalProyectos = db.Proyecto.Count();
            ViewBag.TotalCambios = db.Solicitud_Cambios.Count();
            ViewBag.TotalMiembros = db.Miembro_Proyecto.Count();

            // Usuarios por tipo (para gráfico)
            var usuariosPorTipo = db.Usuario
                .GroupBy(u => u.Tipo_Usuario.Nombre)
                .Select(g => new
                {
                    Tipo = g.Key,
                    Cantidad = g.Count()
                }).ToList();

            ViewBag.UsuariosLabels = usuariosPorTipo.Select(u => u.Tipo).ToList();
            ViewBag.UsuariosData = usuariosPorTipo.Select(u => u.Cantidad).ToList();

            // Proyectos por estado (para gráfico)
            var proyectosPorEstado = db.Proyecto
                .GroupBy(p => p.Estado)
                .Select(g => new
                {
                    Estado = g.Key,
                    Cantidad = g.Count()
                }).ToList();

            ViewBag.ProyectosLabels = proyectosPorEstado.Select(p => p.Estado).ToList();
            ViewBag.ProyectosData = proyectosPorEstado.Select(p => p.Cantidad).ToList();

            return View();
        }
    }
}
