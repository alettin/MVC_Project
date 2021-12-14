using MVC_WebApplication.Models;
using MVC_WebApplication.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CategoryList()
        {
            dbContext dbcontext = new dbContext { _mvcProjectEntities = new MvcProjectEntities() };
            var model = dbcontext._mvcProjectEntities.Categories.ToList().OrderBy(x => x.DisplayOrder);
            return View(model);
        }

        public ActionResult CreateOrUpdateCategory(int? id)
        {
            var model = new Category();
            if (Convert.ToInt32(id) > 0)
            {
                dbContext dbcontext = new dbContext { _mvcProjectEntities = new MvcProjectEntities() };
                model = dbcontext._mvcProjectEntities.Categories.Find(id);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOrUpdateCategory(Category category)
        {
            dbContext dbcontext = new dbContext { _mvcProjectEntities = new MvcProjectEntities() };
            var model = category;
            model.CreateDate = DateTime.Now;
            if ((model.Id) > 0)
            {
                dbcontext._mvcProjectEntities.Categories.Attach(model);
                dbcontext._mvcProjectEntities.Entry(model).State = EntityState.Modified;
            }
            else
                dbcontext._mvcProjectEntities.Categories.Add(model);
            dbcontext._mvcProjectEntities.SaveChanges();

            return RedirectToAction("CategoryList");
        }
    }
}