using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using ElemenTool.CacheLayer.Entities;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using ElemenTool.Api.com.elementool.www;
using ElemenTool.Api.Infrastructure.helpers;

namespace ElemenTool.CacheLayer.Infrastructure
{
    public class ElementoolApi
    {
        private readonly string _eToolAccountName;
        private readonly string _eToolUserName;
        private readonly string _eToolUserPasswd;
        
        public ElementoolApi(string eToolAccountName, string eToolUserName, string eToolUserPasswd)
        {
            _eToolAccountName = eToolAccountName;
            _eToolUserName = eToolUserName;
            _eToolUserPasswd = eToolUserPasswd;
        }

        public List<Issue> GetIssueList()
        {
            //create an instance of the proxy class
            var btService = new BugTracking();
            //(user name = "account\username")
            UsernameToken token = new UsernameToken(_eToolAccountName + @"\" + _eToolUserName, _eToolUserPasswd, PasswordOption.SendHashed);
            btService.RequestSoapContext.Security.Tokens.Add(token);
            btService.RequestSoapContext.Security.Elements.Add(new MessageSignature(token));
            //this method does nothing but throws an exception if login info is incorrect

            var listOfIssues = new List<Issue>();

            IssueField[] repList = btService.GetCustomReportQueryFields(false);
            
            string descr = null;
           
            var reportResult = btService.ExecuteCustomReport(new[] { repList[1] }, ref descr);
          
            foreach (DataTable table in reportResult.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var issue = new Issue();
                    issue.IssueNumber = Convert.ToInt32(row["Issue Number"]);
                    issue.Product = row["Product"] as string;
                    issue.Title = row["Title"] as string;
                    issue.Status = row["Status"] as string;
                    issue.SubmittedBy = row["submitted by"] as string;
                    issue.SubmittedIn = string.IsNullOrEmpty(Convert.ToString(row["Submitted in"])) ? DateTime.MinValue : Convert.ToDateTime(row["Submitted in"]);
                    listOfIssues.Add(issue);
                }
            }

            return listOfIssues;
        }


        public IssueDetails GetIssueDetails(int issueNumber)
        {
            //create an instance of the proxy class
            var btService = new BugTracking();
          
            //(user name = "account\username")
            UsernameToken token = new UsernameToken(_eToolAccountName + @"\" + _eToolUserName, _eToolUserPasswd, PasswordOption.SendHashed);
            btService.RequestSoapContext.Security.Tokens.Add(token);
            btService.RequestSoapContext.Security.Elements.Add(new MessageSignature(token));
           
            //this method does nothing but throws an exception if login info is incorrect
            
            BugTrackingIssue repList = btService.GetIssueByNum(issueNumber);
            
            var issue = new IssueDetails();
            issue.IssueNumber = repList.IssueNumber;
            issue.Title = repList.FieldsArray[0].Value;
            issue.Description = repList.FieldsArray[1].Value;
            issue.StepstoReproduce = repList.FieldsArray[2].Value;
            issue.Product = repList.FieldsArray[3].Value;
            issue.Status = repList.FieldsArray[4].Value;
            issue.Originalissue = repList.FieldsArray[5].Value;
            issue.Versionfoundin = repList.FieldsArray[6].Value;
            issue.Priority = repList.FieldsArray[7].Value;
            issue.QAAssigned = repList.FieldsArray[8].Value;
            issue.Submittedby = repList.FieldsArray[9].Value;
            issue.DevAssigned = repList.FieldsArray[10].Value;
            issue.Versionfixedin = repList.FieldsArray[11].Value;
            issue.Remarks = repList.FieldsArray[12].Value;
            issue.Customer = repList.FieldsArray[13].Value;
            issue.CaseContact = repList.FieldsArray[14].Value;
            issue.Productperson = repList.FieldsArray[15].Value;
            issue.FixType = repList.FieldsArray[16].Value;
            issue.CC = repList.FieldsArray[17].Value;
            issue.TimeSpent = repList.FieldsArray[18].Value;
            issue.SLADeliveryDate = repList.FieldsArray[19].Value;
            issue.PriorityListPosition = repList.FieldsArray[20].Value;
            issue.ServicePack = repList.FieldsArray[21].Value;
            issue.BuildNumber = repList.FieldsArray[22].Value;
            issue.AssignedPriority = repList.FieldsArray[23].Value;
            issue.DevTimeRem = repList.FieldsArray[24].Value;
            issue.DevCompDate = repList.FieldsArray[25].Value;
            issue.DaysSinceIssueCreated = repList.FieldsArray[26].Value;
            issue.QACompDate = repList.FieldsArray[27].Value;

            return issue;
        }

        public IssueDetails SaveIssue(IssueDetails issueDetails)
        {
            //create an instance of the proxy class
            var btService = new BugTracking();

            //(user name = "account\username")
            UsernameToken token = new UsernameToken(_eToolAccountName + @"\" + _eToolUserName, _eToolUserPasswd, PasswordOption.SendHashed);
            btService.RequestSoapContext.Security.Tokens.Add(token);
            btService.RequestSoapContext.Security.Elements.Add(new MessageSignature(token));


            BugTrackingIssue Issue = btService.GetIssueByNum(issueDetails.IssueNumber);

            var issue = BugTrackingIssueHelpers.SetBugTrackingIssueFields(Issue, issueDetails);

            BugTrackingIssue savedIssue = btService.SaveIssue(issue);

            return BugTrackingIssueHelpers.GetNewIssueDetails(savedIssue);
        }

    }
}
