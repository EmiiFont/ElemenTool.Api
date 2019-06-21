using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.OData.Query;

namespace ElemenTool.CacheLayer.Entities
{
    [Page(MaxTop = 100, PageSize = 15)]
    [OrderBy("Id","SubmittedIn","SubmittedBy", "LastUpdateDate")]
    [Filter("Id", "Status", "Title")]
    public class Issue
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedIn  { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Product { get; set; }
        public string AssignedTo { get; internal set; }
        public string AccountName { get; internal set; }
        public DateTime LastUpdateDate { get; internal set; }
    }
}
