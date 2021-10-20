using MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ArticleDetails(int ItemId)
        {
            var model = "Get article detail from database with parameter";
            return View("~/Views/Article/ArticleDetails.cshtml", model: model);
        }

        public ActionResult CreateArticle()
        {
            var model = new vmArticle();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateArticle(vmArticle vmArticle)
        {
            //save to database then redirect the page
            return View("Details",vmArticle);
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}