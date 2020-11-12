﻿using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TaxiWeb.Models;

namespace TaxiWeb.Controllers
{
    [Authorize]
    public class ConductorController : Controller
    {
        private TaxiWebEntities db = new TaxiWebEntities();

        // GET: Conductor
        public ActionResult Index()
        {
            // EJEMPLO LINQ
            //var lista = db.Conductor.Where(q => q.Nombre.StartsWith("E"));
            //var fechaMin = db.Conductor.Min(q => q.FechaNacimiento);
            //var lista = (from conductor in db.Conductor
            //             where fechaMin == conductor.FechaNacimiento
            //             select conductor);
            //var lista = (from conductor in db.Conductor
            //             join vehiculo in db.Vehiculo on conductor.Id equals vehiculo.IdConductor
            //             select conductor);

            // -----------------------------------------------------
            // EJERCICIOS LINQ:

            // Conductores cuya licencia de conducción ya venció.
            //var licenciaVencida = db.Conductor.Where(v => v.ExpiracionLicencia < DateTime.Now);
            //return View(licenciaVencida.ToList());

            // Conductores con licencia de conducción próxima a vencer (rango de X días).
            //var fechaActual = DateTime.Now;
            //var fechaMaxima = fechaActual.AddDays(10);
            //var proximaVencer = db.Conductor.Where(p => p.ExpiracionLicencia > fechaActual
            //&& p.ExpiracionLicencia < fechaMaxima);
            //return View(proximaVencer.ToList());
            // -----------------------------------------------------

            // Listar los conductores que no hayan pasado previamente por la Afiliación.
            //var afiliado = from conductor in db.Conductor
            //               join afilia in db.Afiliacion
            //               on conductor.Cedula equals afilia.Cedula
            //               select conductor.Cedula;

            //var lista = from cond in db.Conductor
            //            where !(afiliado).Contains(cond.Cedula)
            //            select cond;

            //return View(lista.ToList());
            // -----------------------------------------------------

            // Listar conductores que aparecen mínimo 2 veces inscritos en clases.
            //var clasesAgrupadas = (from clase in db.Clase
            //                       group clase by clase.IdConductor into grupoClases
            //                       select grupoClases);

            //clasesAgrupadas = clasesAgrupadas.Where(c => c.Count() >= 2);

            //var idConductores = clasesAgrupadas.Select(cA => cA.Key);

            //var lista = (from conductor in db.Conductor
            //             where idConductores.Contains(conductor.Id)
            //             select conductor);

            //return View(lista.ToList());
            // -----------------------------------------------------

            var lista = (from conductor in db.Conductor
                         join afiliacion in db.Afiliacion
                         on conductor.Cedula equals afiliacion.Cedula
                         select new ConductorAfiliacion
                         {
                             Conductor = conductor,
                             Radicado = afiliacion.Id
                         }).Union(from conductor in db.Conductor
                                  join afiliacion in db.Afiliacion
                                  on conductor.Cedula equals afiliacion.Cedula into afilia
                                  from joinAfiliacion in afilia.DefaultIfEmpty()
                                  where joinAfiliacion == null
                                  select new ConductorAfiliacion
                                  {
                                      Conductor = conductor,
                                      Radicado = 0
                                  });

            return View(lista.ToList());

            //return View(db.Conductor.ToList());
        }

        // GET: Conductor/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conductor conductor = db.Conductor.Find(id);
            if (conductor == null)
            {
                return HttpNotFound();
            }
            return View(conductor);
        }

        public string VerConductor(long id)
        {
            Conductor conductor = db.Conductor.Find(id);
            if (conductor != null)
            {
                return JsonConvert.SerializeObject(conductor, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            else
                return "{\"error\": \"No existe el conductor\"}";
        }

        // GET: Conductor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Conductor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,Cedula,FechaNacimiento,LicenciaConduccion,ExpiracionLicencia")] Conductor conductor)
        {
            if (ModelState.IsValid)
            {
                db.Conductor.Add(conductor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conductor);
        }

        // GET: Conductor/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conductor conductor = db.Conductor.Find(id);
            if (conductor == null)
            {
                return HttpNotFound();
            }
            return View(conductor);
        }

        // POST: Conductor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,Cedula,FechaNacimiento,LicenciaConduccion,ExpiracionLicencia")] Conductor conductor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conductor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conductor);
        }

        // GET: Conductor/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conductor conductor = db.Conductor.Find(id);
            if (conductor == null)
            {
                return HttpNotFound();
            }
            return View(conductor);
        }

        // POST: Conductor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Conductor conductor = db.Conductor.Find(id);
            db.Conductor.Remove(conductor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public string EliminarConductor(long id)
        {
            Conductor conductor = db.Conductor.Find(id);
            db.Conductor.Remove(conductor);
            db.SaveChanges();

            return JsonConvert.SerializeObject(conductor, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public string Listar()
        {
            return JsonConvert.SerializeObject(db.Conductor.ToList(), new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public string Crear(string cedula, string nombre, string apellido, DateTime fechaNacimiento, string licenciaConduccion, DateTime expiracionLicencia)
        {
            var maxId = db.Conductor.Max(x => x.Id);
            var id = maxId + 1;
            Conductor conductor = new Conductor
            {
                Id = id,
                Cedula = cedula,
                Nombre = nombre,
                Apellido = apellido,
                FechaNacimiento = fechaNacimiento,
                LicenciaConduccion = licenciaConduccion,
                ExpiracionLicencia = expiracionLicencia
            };
            if (ModelState.IsValid)
            {
                db.Conductor.Add(conductor);
                db.SaveChanges();
            }

            if (conductor != null)

                return JsonConvert.SerializeObject(conductor, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            else
                return "{\"error\":\"No se pueden guardar los datos\"}";
        }
    }    
}
