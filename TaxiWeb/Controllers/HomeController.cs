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
            var usuarios = db.Usuarios.Where(u =>
                u.Usuario.Equals(usuario) &&
                u.Password.Equals(password)).FirstOrDefault();

            if (usuarios != null)
            {
                FormsAuthentication.SetAuthCookie(usuarios.Usuario, false);
                HttpContext.Session.Add("usuario", usuarios.Usuario);
                ViewBag.Usuario = "Bienvenido " + usuarios.Usuario;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}