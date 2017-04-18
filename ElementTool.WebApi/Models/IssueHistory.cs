using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementTool.WebApi.Models
{
    public class IssueHistory
    {
        public string FieldName { get; set; }
        public string Change { get; set; }
        public string Usename { get; set; }
        public DateTime DateOfChange { get; set; }
    }
}