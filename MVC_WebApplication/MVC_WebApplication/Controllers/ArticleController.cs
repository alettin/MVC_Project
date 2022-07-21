using MVC_WebApplication.Models;
using MVC_WebApplication.Models.DatabaseModel;
using MVC_WebApplication.MvcFilter;
//using MVC_WebApplication.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.Controllers
{
    [LogAuthorize("MENU-001")]
    public class ArticleController : Controller
    {
        public ActionResult Index()
        {
            
            var list = new List<vmArticle>();
            
            DataTable datatable = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcProjectEntities"].ConnectionString))
            {
                string sqlQuery = "SELECT a.[Id],[Title],[Short],[Long],[MetaKeywords],[MetaDescription],[MetaTitle],[HitCount], u.Url,c.Name FROM[MvcProject].[dbo].[Articles] a,[dbo].[SeoUrls] u,[dbo].[Categories] c where a.Id = u.ItemId and u.SystemTable = 1 and a.CategoryId = c.Id";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(datatable);
                sqlConnection.Close();
                sqlDataAdapter.Dispose();

            }

            list = (from DataRow dr in datatable.Rows
                    select new vmArticle()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Title = dr["Title"].ToString(),
                        Short = dr["Short"].ToString(),
                        Full = dr["Long"].ToString(),
                        MetaKeywords = dr["MetaKeywords"].ToString(),
                        MetaDescription = dr["MetaDescription"].ToString(),
                        MetaTitle = dr["MetaTitle"].ToString(),
                        HitCount = Convert.ToInt32(dr["HitCount"]),
                        SeoUrl = dr["Url"].ToString()
                    }).ToList();

            
          
            return View(list);
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
                string sqlQuery = "INSERT INTO [dbo].[Articles]([Title],[Short],[Long],[MetaKeywords],[MetaDescription],[MetaTitle],[HitCount],[CategoryId],[CreateDate]) VALUES(@Title,@Short,@Long,@MetaKeywords,@MetaDescription,@MetaTitle,@HitCount,@CategoryId,@CreateDate);SELECT CAST(scope_identity() AS int)";
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
                //sqlCommand.ExecuteNonQuery();

                var itemId = (int)sqlCommand.ExecuteScalar();

                sqlConnection.Close();

                string sqlQuery2 = "INSERT INTO [dbo].[SeoUrls]([SystemTable],[ItemId],[Url],[IsActive],[CreateDate])VALUES(@SystemTable, @ItemId, @Url,@IsActive,@CreateDate)";
                SqlCommand sqlCommand2 = new SqlCommand(sqlQuery2, sqlConnection);
                sqlCommand2.Parameters.AddWithValue("SystemTable", SystemTable.Article);
                sqlCommand2.Parameters.AddWithValue("ItemId", itemId);
                sqlCommand2.Parameters.AddWithValue("Url", vmArticle.SeoUrl);
                sqlCommand2.Parameters.AddWithValue("IsActive", 1);
                sqlCommand2.Parameters.AddWithValue("CreateDate", DateTime.Now);
                sqlConnection.Open();
                sqlCommand2.ExecuteNonQuery();
                sqlConnection.Close();
            }
            TempData["model"] = vmArticle;
            return RedirectToAction("Details");
        }

        [LogAction("Article details action executed.", LogType.Action)]
        public ActionResult Details(int id)
        {
            var model = new vmArticle();
            DataTable datatable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcProjectEntities"].ConnectionString))
            {
                string sqlQuery = "SELECT a.[Id],[Title],[Short],[Long],[MetaKeywords],[MetaDescription],[MetaTitle],[HitCount], u.Url,c.Name FROM[MvcProject].[dbo].[Articles] a,[dbo].[SeoUrls] u,[dbo].[Categories] c where a.Id = u.ItemId and u.SystemTable = 1 and a.CategoryId = c.Id and a.Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", id);
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(datatable);
                sqlConnection.Close();
                sqlDataAdapter.Dispose();
            }

            model = (from DataRow dr in datatable.Rows
                     select new vmArticle()
                     {
                         Id = Convert.ToInt32(dr["Id"]),
                         Title = dr["Title"].ToString(),
                         Short = dr["Short"].ToString(),
                         Full = dr["Long"].ToString(),
                         MetaKeywords = dr["MetaKeywords"].ToString(),
                         MetaDescription = dr["MetaDescription"].ToString(),
                         MetaTitle = dr["MetaTitle"].ToString(),
                         HitCount = Convert.ToInt32(dr["HitCount"]),
                         SeoUrl = dr["Url"].ToString()
                     }).FirstOrDefault();

            return View(model);
        }
    }
}