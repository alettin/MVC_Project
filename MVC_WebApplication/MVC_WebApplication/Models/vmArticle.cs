using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApplication.Models
{
    public class vmArticle
    {
        public vmArticle()
        {
            CommentCount = 0;
            HitCount = 0;
        }

        public string SeoUrl { get; set; }

        public string Title { get; set; }

        public string Short { get; set; }

        [AllowHtml]
        public string Full { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool AllowComments { get; set; }

        public int CommentCount { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        public int HitCount { get; set; }

    }
}