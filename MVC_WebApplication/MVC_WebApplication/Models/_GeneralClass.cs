using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_WebApplication.Models
{
    public class UrlRecord
    {
        public SystemTable SystemTable { get; set; }

        public string SeoUrl { get; set; }

        public int ItemId { get; set; }

    }

    public enum SystemTable
    {
        Article = 1,
        Event = 2,
        Other = 99
    }

    public enum ExceptionCode
    {
        ApplicationError = 600,
        ViewError = 601,
        SaveLogError = 602,
        Default = 699,
    }
    public enum LogType
    {
        Login = 1,
        Action= 2,
        Error = 3
    }
}