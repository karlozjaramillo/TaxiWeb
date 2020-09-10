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
    public class VehiculoController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();

        // GET: Vehiculo
        public ActionResult Index()
        {
            var vehiculo = db.Vehiculo.Include(v => v.Conductor);
            return View(vehiculo.ToList());
        }

        // GET: Vehiculo/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: Vehiculo/Create
        public ActionResult Create()
        {
            var conductores = (from conductor in db.Conductor
                               select new
                               {
                                   Id = conductor.Id,
                                   nombreCompleto = conductor.Nombre + " " + conductor.Apellido
                               });
            ViewBag.IdConductor = new SelectList(conductores, "Id", "nombreCompleto");
            return View();
        }

        // POST: Vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Placa,Tipo,Marca,Color,IdConductor")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                vehiculo.Placa = vehiculo.Placa.ToUpper();
                db.Vehiculo.Add(vehiculo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // GET: Vehiculo/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // POST: Vehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Placa,Tipo,Marca,Color,IdConductor")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConductor = new SelectList(db.Conductor, "Id", "Nombre", vehiculo.IdConductor);
            return View(vehiculo);
        }

        // GET: Vehiculo/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            db.Vehiculo.Remove(vehiculo);
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
