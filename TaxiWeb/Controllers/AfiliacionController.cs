using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiWeb.Models;

namespace TaxiWeb.Controllers
{
    public class AfiliacionController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();

        // GET: Afiliacion
        public ActionResult Index()
        {
            return View(db.Afiliacion.ToList());
        }

        // GET: Afiliacion/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliacion afiliacion = db.Afiliacion.Find(id);
            if (afiliacion == null)
            {
                return HttpNotFound();
            }
            return View(afiliacion);
        }

        // GET: Afiliacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Afiliacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaRadicado,Cedula,NombreCompleto,Edad,Valor")] Afiliacion afiliacion)
        {
            if (ModelState.IsValid)
            {
                db.Afiliacion.Add(afiliacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(afiliacion);
        }

        // GET: Afiliacion/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliacion afiliacion = db.Afiliacion.Find(id);
            if (afiliacion == null)
            {
                return HttpNotFound();
            }
            return View(afiliacion);
        }

        // POST: Afiliacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaRadicado,Cedula,NombreCompleto,Edad,Valor")] Afiliacion afiliacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(afiliacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(afiliacion);
        }

        // GET: Afiliacion/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliacion afiliacion = db.Afiliacion.Find(id);
            if (afiliacion == null)
            {
                return HttpNotFound();
            }
            return View(afiliacion);
        }

        // POST: Afiliacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Afiliacion afiliacion = db.Afiliacion.Find(id);
            db.Afiliacion.Remove(afiliacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
