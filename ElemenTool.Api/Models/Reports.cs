using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElemenTool.Api.com.elementool.www;

namespace ElemenTool.Api.Models
{
    public class Reports
    {
        private QuickReport v;

        public Reports()
        {
            
        }

        public Reports(QuickReport v)
        {
            this.v = v;
            ID = v.ID;
            Description = v.Description;
            Name = v.Name;
        }

        public int ID { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}