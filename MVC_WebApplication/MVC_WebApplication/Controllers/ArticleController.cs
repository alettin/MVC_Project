using MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.Controllers
{
    public class ArticleController : Controller
    {
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
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcProjectEntities"].ConnectionString))
            {
                string sqlQuery = "INSERT INTO [dbo].[Articles]([Title],[Short],[Long],[MetaKeywords],[MetaDescription],[MetaTitle],[HitCount],[CategoryId],[CreateDate]) VALUES(@Title,@Short,@Long,@MetaKeywords,@MetaDescription,@MetaTitle,@HitCount,@CategoryId,@CreateDate)";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("Title", vmArticle.Title);
                sqlCommand.Parameters.AddWithValue("Short", vmArticle.Short);
                sqlCommand.Parameters.AddWithValue("Long", vmArticle.Full);
                sqlCommand.Parameters.AddWithValue("MetaKeywords", vmArticle.MetaKeywords);
                sqlCommand.Parameters.AddWithValue("MetaDescription", vmArticle.MetaDescription);
                sqlCommand.Parameters.AddWithValue("MetaTitle", vmArticle.MetaTitle);
                sqlCommand.Parameters.AddWithValue("HitCount", 0);
                sqlCommand.Parameters.AddWithValue("CategoryId", vmArticle.CategoryId);
                sqlCommand.Parameters.AddWithValue("CreateDate", DateTime.Now);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            TempData["model"] = vmArticle;
            return RedirectToAction("Details");
        }

        public ActionResult Details()
        {
            vmArticle vmArticle = new vmArticle();
            vmArticle = TempData["model"] as vmArticle;
            return View(vmArticle);
        }
    }
}