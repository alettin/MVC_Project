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
    public class ArticleController : Controller
    {
      

        public ActionResult Index()
        {
            dbContext dbContext = new dbContext {_mvcProjectEntities = new MvcProjectEntities()};
            var model = dbContext._mvcProjectEntities.Articles.OrderBy(x=> x.CreateDate).ThenByDescending(x => x.CreateDate);
            return View(model);
        }

        public ActionResult ArticleDetails(int ItemId)
        {
            dbContext dbContext = new dbContext { _mvcProjectEntities = new MvcProjectEntities() };
            var model = dbContext._mvcProjectEntities.Articles.Find(ItemId);
            //var model = "Get article detail from database with parameter";
            //return View("~/Views/Article/ArticleDetails.cshtml", model: model);
            if (model == null)
                return RedirectToAction("PageNotFound","Error");
            return View(model);
        }

        public ActionResult CreateOrUpdateArticle(int? id)
        {
            var model = new Article();

            if(Convert.ToInt32(id) > 0)
            {
                dbContext dbContext = new dbContext { _mvcProjectEntities = new MvcProjectEntities() };
                model = dbContext._mvcProjectEntities.Articles.Find(id);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOrUpdateArticle(Article article)
        {
            dbContext dbContext = new dbContext { _mvcProjectEntities = new MvcProjectEntities() };
            var model = article;
            model.CreateDate = DateTime.Now;
            if (article.Id > 0)
            {
                dbContext._mvcProjectEntities.Articles.Attach(model);
                dbContext._mvcProjectEntities.Entry(model).State = EntityState.Modified;
            }
            else
                dbContext._mvcProjectEntities.Articles.Add(model);

            dbContext._mvcProjectEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details()
        {
            vmArticle vmArticle = new vmArticle();
            vmArticle = TempData["model"] as vmArticle;
            return View(vmArticle);
        }
    }
}