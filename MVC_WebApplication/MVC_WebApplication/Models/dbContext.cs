using MVC_WebApplication.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_WebApplication.Models
{
    public class dbContext
    {
        public MvcProjectEntities _mvcProjectEntities { get; set; }
    }
}