using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppFirst.Models;

namespace WebAppFirst.Controllers
{
    public class ShippersController : Controller
    {
        // GET: Shippers
        northwindEntities entities = new northwindEntities();
        public ActionResult Index()
        {
            //List<Shippers> model = entities.Shippers.ToList();
            //return View(model); TOIMII MOLEMMILLA? NÄÄ?

            //var shippers = entities.Shippers;
            //return View(

            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {

                var shippers = entities.Shippers.Include(s => s.Region);
                return View(shippers.ToList());
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); } //{periaatteessa turhat?}
            Shippers shippersss = entities.Shippers.Find(id);
            if (shippersss == null) { return HttpNotFound(); }
            ViewBag.RegionID = new SelectList(entities.Region, "RegionID", "RegionDescription", shippersss.RegionID);
            return View(shippersss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598
        public ActionResult Edit([Bind(Include = "ShipperID,CompanyName,Phone,RegionID")] Shippers shipper)
        {
            if (ModelState.IsValid)
            {
                entities.Entry(shipper).State = EntityState.Modified;
                entities.SaveChanges();
                ViewBag.RegionID = new SelectList(entities.Region, "RegionID", "RegionDescription", shipper.RegionID);
                return RedirectToAction("Index");
            }
            return View(shipper);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RegionID = new SelectList(entities.Region, "RegionID", "RegionDescription");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipperID,CompanyName,Phone,RegionID")] Shippers shipper)
        {
            if (ModelState.IsValid)
            {
                entities.Shippers.Add(shipper);
                entities.SaveChanges();
                ViewBag.RegionID = new SelectList(entities.Region, "RegionID", "RegionDescription", shipper.RegionID); //ilman viimeistäkin pärjää!
                return RedirectToAction("Index");
            }
            return View(shipper);
        }



        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Shippers shippers = entities.Shippers.Find(id);
            if (shippers == null) return HttpNotFound();
            return View(shippers);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shippers shippers = entities.Shippers.Find(id);
            entities.Shippers.Remove(shippers);
            entities.SaveChanges();
            ViewBag.RegionID = new SelectList(entities.Region, "RegionID", "RegionDescription", shippers.RegionID);
            return RedirectToAction("Index");
        }
    }
}