using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ElemenTool.CacheLayer.Entities
{
    public class Issue
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Indexed]
        public int IssueNumber { get; set; }
        public string Title { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedIn  { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Product { get; set; }
        public string AssignedTo { get; internal set; }
    }
}
