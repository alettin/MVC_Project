//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_WebApplication.Models.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class SeoUrl
    {
        public int Id { get; set; }
        public int SystemTable { get; set; }
        public int ItemId { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
