using ElemenTool.CacheLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData.Query;

namespace ElementTool.WebApi.Models
{
    [Page(MaxTop = 100, PageSize = 15)]
    [OrderBy("Id", "SubmittedIn", "SubmittedBy", "LastUpdateDate")]
    [Filter("Id", "Status", "Title")]
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Decription { get; set; }
        public IEnumerable<Issue> Issues { get; set; }
    }
}