using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult EventDetails(int ItemId)
        {
            var model = "Get event detail from database with parameter";
            return View();
        }
    }
}