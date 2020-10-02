using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaxiWeb.Models;

namespace TaxiWeb.Controllers
{
    public class HomeController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string password)
        {
            var comparacion = db.Usuarios.Where(u =>
                u.Usuario.Equals(usuario) &&
                u.Password.Equals(password)).FirstOrDefault();

            if (comparacion != null)
            {
                FormsAuthentication.SetAuthCookie(comparacion.Usuario, false);
                ViewBag.Usuario = "Bienvenido " + comparacion.Usuario;
                return View("Index");
            }
            else
            {
                return View("Error");
            }
        }
    }
}