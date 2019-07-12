using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAutos.Models;

namespace DataAutos.Controllers
{
    public class AutoesController : Controller
    {
        static List<Auto> ListaAutos =new List<Auto>();
        static Persona persona1 = new Persona();

        private DataAutosEntities db = new DataAutosEntities();

        // GET: Autoes
        public ActionResult Index()
        {
            var auto = db.Auto.Include(a => a.Persona);
            return View(auto.ToList());
        }

        // GET: Autoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.Auto.Find(id);
            if (auto == null)
            {
                return HttpNotFound();
            }
            return View(auto);
        }

        // GET: Autoes/Create
        public ActionResult Create()
        {

            //ViewBag.per_id = new SelectList(db.Persona, "per_id", "per_nombre");
            var per = new Persona();
            return View(per);
        }
        [HttpPost]
        public ActionResult Create(Persona persona)
        {
            db.Persona.Add(persona);
            db.SaveChanges();
            /*foreach (var carro in Auto)
            {
               carro.per_id = persona.per_id;
                db.Auto.Add(carro);
                db.SaveChanges();
            }*/
            //ViewBag.per_id = new SelectList(db.Persona, "per_id", "per_nombre", auto.per_id);
            return View(persona);
        }
        public ActionResult AgregarAuto(Auto auto, Persona per1)
        {
            ViewBag.Nombre = per1.per_nombre;
            ViewBag.Edad = per1.per_edad;
            ListaAutos.Add(new Auto
            {
                aut_id = auto.aut_id,
                per_id = per1.per_id,
                aut_color = auto.aut_color,
                aut_modelo = auto.aut_modelo,
                aut_placa = auto.aut_placa
            });
            return View();
            //return View(auto);
        }

        public ActionResult Salvar(Auto auto, Persona per1)
        {
            db.Persona.Add(per1);
            foreach (var carro in ListaAutos)
            {
                carro.per_id = per1.per_id;
                db.Auto.Add(carro);
                db.SaveChanges();
            }
            //ViewBag.per_id = new SelectList(db.Persona, "per_id", "per_nombre", auto.per_id);
            return View();
        }

        private ActionResult Cancel()
        {
            // process the cancellation request here.
            return (View("Cancelled"));
        }

        private ActionResult Send()
        {
            // perform the actual send operation here.
            return (View("SendConfirmed"));
        }


       
        // GET: Autoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.Auto.Find(id);
            if (auto == null)
            {
                return HttpNotFound();
            }
            ViewBag.per_id = new SelectList(db.Persona, "per_id", "per_nombre", auto.per_id);
            return View(auto);
        }

        // POST: Autoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aut_id,per_id,aut_placa,aut_modelo,aut_color")] Auto auto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.per_id = new SelectList(db.Persona, "per_id", "per_nombre", auto.per_id);
            return View(auto);
        }

        // GET: Autoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Auto auto = db.Auto.Find(id);
            if (auto == null)
            {
                return HttpNotFound();
            }
            return View(auto);
        }

        // POST: Autoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Auto auto = db.Auto.Find(id);
            db.Auto.Remove(auto);
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
