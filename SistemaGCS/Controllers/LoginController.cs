using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using SistemaGCS.Models;
using System.Data.Entity;

namespace SistemaGCS.Controllers
{
    public class LoginController : Controller
    {
        // Vista del formulario de login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Index(Usuario model, string ReturnUrl)
        {
            using (var db = new ModelGCS())
            {
                // Incluye navegación hacia Tipo_Usuario si usas EF
                var user = db.Usuario
                    .Include("Tipo_Usuario")
                    .FirstOrDefault(u =>
                        u.Correo == model.Correo &&
                        u.Password == model.Password &&
                        u.Estado == "A"
                    );

                if (user != null)
                {
                    // Guardar datos en sesión
                    Session["Id_usuario"] = user.Id_usuario;
                    Session["NombreCompleto"] = user.Nombre + " " + user.Apellido;
                    Session["RolNombre"] = user.Tipo_Usuario.Nombre;

                    // Autenticación clásica
                    FormsAuthentication.SetAuthCookie(user.Correo, false);

                    // Redirección según el tipo de usuario
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);

                    if (user.Id_tipousuario == 1)
                        return RedirectToAction("Index", "AdminDashboard");
                    else if (user.Id_tipousuario == 2)
                        return RedirectToAction("Index", "DesarrolladorDashboard");

                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["mensaje"] = "Correo o contraseña incorrectos.";
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
