using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3;

namespace WebApplication3.Controllers
{
    public class BikesManagerController : Controller
    {
        private AdventureWorks2012Entities db = new AdventureWorks2012Entities();

        public ActionResult Index()
        {
            var Products = from prod in db.Products where prod.ProductCategoryID == 5 || prod.ProductCategoryID == 6 || prod.ProductCategoryID == 7 select prod;
            return View(Products);
        }

        // GET: BikesManager/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryID = (IEnumerable<SelectListItem>) from cat in db.ProductCategories where cat.ParentProductCategoryID == 1 select new SelectListItem { Value = cat.ProductCategoryID.ToString() , Text = cat.Name };
            ViewBag.ProductModelID = ( from mod in db.vProductAndDescription2 where mod.ProductCategoryID == 5 || mod.ProductCategoryID == 6 || mod.ProductCategoryID == 7 select new SelectListItem { Value = mod.ProductModelID.ToString(), Text = mod.ProductModel }).Distinct();
            ViewBag.MountainModels = (from mod in db.vProductAndDescription2 where mod.ProductCategoryID == 5 select new SelectListItem { Value = mod.ProductModelID.ToString(), Text = mod.ProductModel }).Distinct();
            ViewBag.RoadModels = (from mod in db.vProductAndDescription2 where mod.ProductCategoryID == 6 select new SelectListItem { Value = mod.ProductModelID.ToString(), Text = mod.ProductModel }).Distinct();
            ViewBag.TouringModels = (from mod in db.vProductAndDescription2 where mod.ProductCategoryID == 7 select new SelectListItem { Value = mod.ProductModelID.ToString(), Text = mod.ProductModel }).Distinct();
            ViewBag.now = DateTime.Now.Date.ToString("yyyy/MM/dd");
            return View();
        }

        // POST: BikesManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = (IEnumerable<SelectListItem>)from cat in db.ProductCategories where cat.ParentProductCategoryID == 1 select new SelectListItem { Value = cat.ProductCategoryID.ToString(), Text = cat.Name };
            ViewBag.ProductModelID = (from mod in db.vProductAndDescription2 where mod.ProductCategoryID == 5 || mod.ProductCategoryID == 6 || mod.ProductCategoryID == 7 select new SelectListItem { Value = mod.ProductModelID.ToString(), Text = mod.ProductModel }).Distinct();
            ViewBag.MountainModels = (from mod in db.vProductAndDescription2 where mod.ProductCategoryID == 5 select new SelectListItem { Value = mod.ProductModelID.ToString(), Text = mod.ProductModel }).Distinct();
            ViewBag.RoadModels = (from mod in db.vProductAndDescription2 where mod.ProductCategoryID == 6 select new SelectListItem { Value = mod.ProductModelID.ToString(), Text = mod.ProductModel }).Distinct();
            ViewBag.TouringModels = (from mod in db.vProductAndDescription2 where mod.ProductCategoryID == 7 select new SelectListItem { Value = mod.ProductModelID.ToString(), Text = mod.ProductModel }).Distinct();
            ViewBag.now = DateTime.Now.Date.ToString("yyyy/MM/dd");
            return View(product);
        }

        // GET: BikesManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            return View(product);
        }

        // POST: BikesManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryID,ProductModelID,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", product.ProductCategoryID);
            return View(product);
        }

        // GET: BikesManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: BikesManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public JsonResult CheckListPrice(decimal ListPrice, decimal StandardCost)
        {
            bool result;
            if (StandardCost != null)
            {
                if (ListPrice > StandardCost)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }else
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DuplicateName(string name)
        {
            bool result;

            if(! db.Products.Any(x => x.Name == name))
            {
                result = true;
            }else
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DuplicateNum(string productNumber)
        {
            bool result;

            if (!db.Products.Any(x => x.ProductNumber == productNumber))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
