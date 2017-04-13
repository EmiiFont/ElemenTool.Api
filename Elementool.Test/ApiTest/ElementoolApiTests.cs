using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElemenTool.CacheLayer.Infrastructure;
using System.Linq;
using ElemenTool.Api.Controllers;
using ElemenTool.Api.DataObjects;

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
           // api.GetIssueDetails(mylist.First().IssueNumber);
        }

        [TestMethod]
        public void PostLoginShouldLoginUser()
        {
            var api = new IssuesController();
            var item = new ElemenToolItem();
            item.AccountName = "";
            item.UserName = "";
            item.Password= "";
            item.Id = item.UserName + "@" + item.AccountName;

            var isLogged = api.PostLogin(item);
            Assert.IsTrue(isLogged);
        }

        [TestMethod]
        public void GetIssueListFromControllerShouldReturnAnIssueList()
        {
            var api = new IssuesController();
            var item = new ElemenToolItem();
            item.AccountName = "";
            item.UserName = "";
            item.Id = item.UserName + "@" + item.AccountName;

            var isLogged = api.GetIssues(item.UserName + "@" + item.AccountName);
            Assert.IsTrue(isLogged.Count > 0);
        }
    }
}
