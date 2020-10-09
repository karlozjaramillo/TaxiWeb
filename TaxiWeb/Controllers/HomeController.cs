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
        public string Login(string usuario, string password)
        {
            var usuarioDb = db.Usuarios.Where(u =>
                u.Usuario.Equals(usuario) &&
                u.Password.Equals(password)).FirstOrDefault();

            if (usuarioDb != null)
            {
                FormsAuthentication.SetAuthCookie(usuarioDb.Usuario, false);
                HttpContext.Session.Add("usuario", usuarioDb.Usuario);

                return Newtonsoft.Json.JsonConvert.SerializeObject(usuarioDb);
            }
            else
            {
                return "{\"error\":\"El usuario no existe\"}";
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