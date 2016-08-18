﻿using System;
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
using ElemenTool.Api.DataObjects;
using System.Linq;

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

        public ElementoolApi()
        {

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
            var fields = GetSystemFieldsCaption(repList);

            string descr = "";
           
            var reportResult = btService.ExecuteCustomReport(repList, ref descr);

            foreach (DataTable table in reportResult.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var issue = new Issue();
                    issue.IssueNumber = Convert.ToInt32(row["Issue Number"]);
                    issue.Product = row[fields.Product] as string;
                    issue.Title = row[fields.Title] as string;
                    issue.Status = row[fields.Status] as string;
                    issue.AssignedTo = row[fields.AssignedTo] as string;
                    issue.SubmittedBy = row[fields.SubmittedBy] as string;
                    issue.SubmittedIn = string.IsNullOrEmpty(Convert.ToString(row[fields.SubmittedIn])) ? DateTime.MinValue : Convert.ToDateTime(row[fields.SubmittedIn]);
                    listOfIssues.Add(issue);
                }
            }

            return listOfIssues.OrderByDescending(b => b.SubmittedIn).ToList();
        }

        public bool LoginCheck(ElemenToolItem accountItem)
        {
            //create an instance of the proxy class
            var btService = new BugTracking();
            //(user name = "account\username")
            UsernameToken token = new UsernameToken(accountItem.AccountName + @"\" + accountItem.UserName, accountItem.Password, PasswordOption.SendHashed);
            btService.RequestSoapContext.Security.Tokens.Add(token);
            btService.RequestSoapContext.Security.Elements.Add(new MessageSignature(token));
            try
            {
                btService.LoginCheck();
            }
            catch (Exception ex)
            {
                //TODO: log exception
                return false;
            }

            return true;
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

            foreach (var item in repList.FieldsArray.Skip(4))
            {
                var fields = new IssueDetailsFields()
                {
                    Caption = item.Caption,
                    Value = item.Value,
                    SystemName = item.SystemName,
                    OptionList = item.ValueList
                };
                issue.Fields.Add(fields);
            }
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

        private IssueSystemFields GetSystemFieldsCaption(IssueField[] repList)
        {
            var issueSystemFields = new IssueSystemFields();
            issueSystemFields.Title = repList.First(v => v.SystemName == "title").Caption;
            issueSystemFields.Product = repList.First(v => v.SystemName == "component").Caption;
            issueSystemFields.Status = repList.First(v => v.SystemName == "status").Caption;
            issueSystemFields.SubmittedBy = repList.First(v => v.SystemName == "reported_by").Caption;
            issueSystemFields.SubmittedIn = repList.First(v => v.SystemName == "reporting_date").Caption;
            issueSystemFields.Version = repList.First(v => v.SystemName == "version_no").Caption;
            issueSystemFields.Severity = repList.First(v => v.SystemName == "severity").Caption;
            issueSystemFields.Priority = repList.First(v => v.SystemName == "priority").Caption;
            issueSystemFields.Description = repList.First(v => v.SystemName == "description").Caption;
            issueSystemFields.LastUpdated = repList.First(v => v.SystemName == "last_updated").Caption;
            issueSystemFields.AssignedTo = repList.First(v => v.SystemName == "assigned_to").Caption;

            return issueSystemFields;
        }
    }
}
