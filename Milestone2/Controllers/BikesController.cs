using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Milestone2.Models;

namespace Milestone2.Controllers
{
    public class BikesController : Controller
    {
        private BikeDBContext db = new BikeDBContext();

        // GET: Bikes
        public ActionResult Index()
        {
            return View(db.BikeListModels.ToList());
        }

        // GET: Bikes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeListModel bikeListModel = db.BikeListModels.Find(id);
            if (bikeListModel == null)
            {
                return HttpNotFound();
            }
            return View(bikeListModel);
        }

        // GET: Bikes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bikes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductModel,Description")] BikeListModel bikeListModel)
        {
            if (ModelState.IsValid)
            {
                db.BikeListModels.Add(bikeListModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bikeListModel);
        }

        // GET: Bikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeListModel bikeListModel = db.BikeListModels.Find(id);
            if (bikeListModel == null)
            {
                return HttpNotFound();
            }
            return View(bikeListModel);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductModel,Description")] BikeListModel bikeListModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bikeListModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bikeListModel);
        }

        // GET: Bikes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeListModel bikeListModel = db.BikeListModels.Find(id);
            if (bikeListModel == null)
            {
                return HttpNotFound();
            }
            return View(bikeListModel);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BikeListModel bikeListModel = db.BikeListModels.Find(id);
            db.BikeListModels.Remove(bikeListModel);
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
