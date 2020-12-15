using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppFirst.Models;

namespace WebAppFirst.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.OmaTieto = "Tuotehaku";
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                northwindEntities entities = new northwindEntities();
                List<Products> model = entities.Products.ToList();
                entities.Dispose();
                return View(model);
            }
        }

        public ActionResult Index2()
        {
            ViewBag.OmaTieto = "Tuotehaku";

            northwindEntities entities = new northwindEntities();
            List<Products> model = entities.Products.ToList();
            entities.Dispose();
            return View(model);
        }
    }
}