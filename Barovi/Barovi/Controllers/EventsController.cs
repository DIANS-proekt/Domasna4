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
    public class EventsController : Controller
    {

        //kreirame objekt od bazata, so koj podocna ja povrzuvame bazata so soodvetnite pogledi
        private BaroviContext db = new BaroviContext();



        //Gi izlistuva site nastani koi se smesteni vo bazata 
        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }


        // GET: Events/Details/5

        //Proveruva dali go imame nastanot
        //dokolku go imame ni gi vrakja podatocite za nego, a dokolku go nema vrakja HttpNotFound
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Events/Create

        //Kreira pogled za dodavanje na nastan
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //Se kreira dadeniot nastan, so soodvetnite parametri, se zacuvuva vo databazata i se redirektira do Index pogledot
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Image,Location,Price,Age")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(events);
        }

        // GET: Events/Edit/5

        //Proveruva dali postoi nastan so dadenoto id
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //Ovozmozuva promena na informaciite za nastanot, gi zacuvuva i ne redirektira do pogledot Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Image,Location,Price,Age")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Delete/5

        //Proveruva dali ima nastan so dadenoto id 
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Delete/5

        //Ovozmozuva brisenje na dadeniot nastan, zacuvuvanje na promenite i redirektiranje do pogledot Index
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Events events = db.Events.Find(id);
            db.Events.Remove(events);
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
