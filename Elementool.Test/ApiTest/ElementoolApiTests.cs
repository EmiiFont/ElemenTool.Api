using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElemenTool.CacheLayer.Infrastructure;
using System.Linq;
using ElementTool.WebApi.Controllers;
using ElementTool.WebApi.DataObjects;
using System.Configuration;

namespace Elementool.Test
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string accountName = ConfigurationManager.AppSettings["accountname"];
        private static readonly string userName = ConfigurationManager.AppSettings["username"];
        private static readonly string password = ConfigurationManager.AppSettings["password"];

        [TestMethod]
        public void GetIssuesShouldReturnAListOfIssues()
        {
            var api = new ElementoolApi("","","");
            var mylist = api.GetIssueList();
           // api.GetIssueDetails(mylist.First().IssueNumber);
        }

        [TestMethod]
        public void PostLoginShouldLoginUser()
        {
            var api = new IssueDetailController();
            var item = new ElemenToolItem();
            item.AccountName = accountName;
            item.Id = "";
            item.UserName = userName;
            item.Password= password;
            item.FullAccount = item.UserName + "@" + item.AccountName;

            var isLogged = true;
            Assert.IsTrue(isLogged);
        }

        [TestMethod]
        public void GetIssueListFromControllerShouldReturnAnIssueList()
        {
            var api = new IssueDetailController();
            var item = new ElemenToolItem();
            item.AccountName = accountName;
            item.UserName = userName;
            item.Id = item.UserName + "@" + item.AccountName;

           // var isLogged = api.Get();
            //Assert.IsTrue(isLogged.Count > 0);

        }
        [TestMethod]
        public void GetQuickReportsFromElementooApi()
        {
            var api = new ElementoolApi(accountName, userName, password);

            var isLogged = api.GetReports();
            //Assert.IsTrue(isLogged.Count > 0);
        }

        [TestMethod]
        public void GetIssuesByQuickReportIDShouldReturnAListOfIssues()
        {
            var api = new ElementoolApi(accountName, userName, password);

            var reports = api.GetReports();

            var listOfIssues = api.GetIssuesByReportId(reports[2].ID);

            Assert.IsTrue(listOfIssues.Count > 0);
        }

        [TestMethod]
        public void GetIssuesByQuickReportIDShouldReturnNull()
        {
            var api = new ElementoolApi(accountName, userName, password);

            var listOfIssues = api.GetIssuesByReportId(0);

            Assert.IsTrue(listOfIssues == null);
        }

        [TestMethod]
        public void GetIssuesDetailsShouldReturnAnIssue()
        {
            var api = new ElementoolApi(accountName, userName, password);

            var listOfIssues = api.GetIssueDetails(17760);

            Assert.IsTrue(listOfIssues != null);
        }

        [TestMethod]
        public void GetIssuesDetailsShouldReturnAListOfIssues()
        {
            var api = new ElementoolApi(accountName, userName, password);

            var listOfIssues = api.GetIssueList();

            Assert.IsTrue(listOfIssues != null);
        }

        [TestMethod]
        public void GetI()
        {
            var api = new ElementoolApi(accountName, userName, password);
             api.GetWelcomeReportList();

            Assert.IsTrue(true);
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