using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementTool.WebApi.Models
{
    public class IssueRemarks
    {
        public string Username { get; set; }
        public string RemarkText { get; set; }
        public DateTime DateOfRemark{ get; set; }
    }
}