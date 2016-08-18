using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElemenTool.CacheLayer.Infrastructure;
using System.Linq;

namespace Elementool.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetIssuesShouldReturnAListOfIssues()
        {
            var api = new ElementoolApi("","","");
            var mylist = api.GetIssueList();
            api.GetIssueDetails(mylist.First().IssueNumber);
        }
    }
}
