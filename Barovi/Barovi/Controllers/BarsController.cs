using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Barovi.Data;
using Barovi.Models;

namespace Barovi.Controllers
{
    public class BarsController : Controller
    {

        //kreirame objekt od bazata, so koj podocna ja povrzuvame bazata so soodvetnite pogledi
        private BaroviContext db = new BaroviContext();


        //Gi izlistuva site barovi koi se smesteni vo bazata
        // GET: Bars
        public ActionResult Index()
        {
              return View(db.Bars.ToList());
        }


        //kreira pogled  meni koj gi sodrzi pijalocite koi se na promocija
        public ActionResult Meni()
        {
            return View();
        }



        //Proveruva dali go imame barot
        //dokolku go imame ni gi vrakja podatocite za nego, a dokolku go nema vrakja HttpNotFound
        //ViewBag go koristime kako referenca za imeto na soodvetniot bar da se prikazuva i vo Details pogledot

        // GET: Bars/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bars bars = db.Bars.Find(id);
            if (bars == null)
            {
                return HttpNotFound();
            }

            var i = db.Bars.Find(id);
            ViewBag.Ime = i.Name;
            ViewBag.Id = i.Id;

            return View(bars);
        }



        //Kreira pogled za dodavanje na barot
        // GET: Bars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        //Se kreira dadeniot bar, so soodvetnite parametri, se zacuvuva vo databazata i se redirektira do Index pogledot
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Grad,Image,Hours,Lat,Lon,Phone,Rating")] Bars bars)
        {
            if (ModelState.IsValid)
            {
                db.Bars.Add(bars);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bars);
        }


        //Proveruva dali postoi bar so dadenoto id
        // GET: Bars/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bars bars = db.Bars.Find(id);
            if (bars == null)
            {
                return HttpNotFound();
            }
            return View(bars);
        }

        // POST: Bars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        //Ovozmozuva promena na informaciite za barot, gi zacuvuva i ne redirektira do pogledot Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Grad,Image,Hours,Lat,Lon,Phone,Rating")] Bars bars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bars).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bars);
        }


        //Proveruva dali ima bar so dadenoto id 
        // GET: Bars/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bars bars = db.Bars.Find(id);
            if (bars == null)
            {
                return HttpNotFound();
            }
            return View(bars);
        }


        //Ovozmozuva brisenje na dadeniot bar, zacuvuvanje na promenite i redirektiranje do pogledot Index
        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bars bars = db.Bars.Find(id);
            db.Bars.Remove(bars);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Sluzi za osloboduvanje na nemenadzirani resursi i opcionalno osloboduvanje na menadziranite
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
