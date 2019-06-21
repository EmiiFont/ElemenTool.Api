using System;
using System.Diagnostics;
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

        [TestMethod]
        public void GetQuickReport()
        {
            var api = new ElementoolApi("", "EFont", "");
            var mylist = api.GetQuickReports();
        }

        [TestMethod]
        public void GetIssueDetailsTest()
        {
            var api = new ElementoolApi("","","");
            var st = new Stopwatch();
           
            var mylist = api.GetIssueDetails(19071);



            st.Start();
            var mylist2 = api.GetIssueDetails(19071);
            st.Stop();

            var d = st.Elapsed;
            //api.GetIssueDetails(mylist.First().IssueNumber);
        }
    }
}
