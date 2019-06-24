using System;
using System.Collections.Generic;
using System.Data;
using ElemenTool.CacheLayer.Entities;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using System.Linq;
using ElementTool.WebApi.com.elementool.www;
using ElementTool.WebApi.DataObjects;
using ElementTool.WebApi.Infrastructure.helpers;
using SharpRaven;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees;
using SharpRaven.Data;
using ElementTool.WebApi.Models;

namespace ElemenTool.CacheLayer.Infrastructure
{
    public class ElementoolApi
    {
        private readonly string _eToolAccountName;
        private readonly string _eToolUserName;
        private readonly string _eToolUserPasswd;
        private readonly BugTracking _btService;
        private RavenClient _sentryLog;
        private static readonly string sentryDSN = ConfigurationManager.AppSettings["sentryDSN"];

        public ElementoolApi(string eToolAccountName, string eToolUserName, string eToolUserPasswd)
        {
            _eToolAccountName = eToolAccountName;
            _eToolUserName = eToolUserName;
            _eToolUserPasswd = eToolUserPasswd;
            _btService = new BugTracking();

            if (!string.IsNullOrEmpty(sentryDSN))
                _sentryLog = new RavenClient(sentryDSN);
        }

        public ElementoolApi()
        {

        }

        public List<Issue> GetIssueList()
        {
            SetServiceCredentials();
            //this method does nothing but throws an exception if login info is incorrect

            var listOfIssues = new List<Issue>();

            IssueField[] repList = _btService.GetCustomReportQueryFields(false);

            var fields = GetSystemFieldsCaption(repList);

            string descr = "";

            var reportResult = _btService.ExecuteCustomReport(new IssueField[0], ref descr);

            foreach (DataTable table in reportResult.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var issue = new Issue();
                    issue.Id = Convert.ToInt32(row["Issue Number"]);
                    issue.Product = row[fields.Product] as string;
                    issue.Title = row[fields.Title] as string;
                    issue.Status = row[fields.Status] as string;
                    issue.AccountName = _eToolAccountName;
                    issue.AssignedTo = row[fields.AssignedTo] as string;
                    issue.SubmittedBy = row[fields.SubmittedBy] as string;
                    //issue.LastUpdateDate = GetLastUpdateDate(btService, issue.Id);
                    issue.SubmittedIn = string.IsNullOrEmpty(Convert.ToString(row[fields.SubmittedIn])) ? DateTime.MinValue : Convert.ToDateTime(row[fields.SubmittedIn]);
                    listOfIssues.Add(issue);
                }
            }

            return listOfIssues.OrderByDescending(b => b.Id).ToList();
        }

        public bool LoginCheck(ElemenToolItem accountItem)
        {
            //create an instance of the proxy class

            //(user name = "account\username")
            SetServiceCredentials();
            try
            {
                _btService.LoginCheck();
            }
            catch (Exception ex)
            {
                _sentryLog.Capture(new SentryEvent(ex));
                return false;
            }

            return true;
        }

        public QuickReport[] GetReports()
        {
            SetServiceCredentials();
            var userReports = _btService.GetQuickReportsList();

            return userReports;
        }

        public List<Issue> GetIssuesByReportId(int reportId)
        {

            try
            {
                SetServiceCredentials();

                var userReports = _btService.ExecuteQuickReport(reportId);

                IssueField[] repList = _btService.GetCustomReportQueryFields(false);

                var issueList = new List<Issue>();
                foreach (DataTable table in userReports.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        var issue = new Issue();

                        foreach (DataColumn column in table.Columns)
                        {
                            if (column.ColumnName.Equals("Issue Number", StringComparison.CurrentCultureIgnoreCase))
                            {
                                issue.Id = Convert.ToInt32(row[column].ToString());
                            }
                            else
                            {
                                var systemField = GetFieldSystemCaption(repList, column.ColumnName);
                                switch (systemField.SystemName)
                                {
                                    case "title":
                                        issue.Title = row[column].ToString();
                                        break;
                                    case "status":
                                        issue.Status = row[column].ToString();
                                        break;
                                    case "reporting_date":
                                        issue.SubmittedIn = string.IsNullOrEmpty(Convert.ToString(row[column])) ? DateTime.MinValue : Convert.ToDateTime(row[column]);
                                        break;
                                    case "priority":
                                        issue.Priority = row[column].ToString();
                                        break;
                                    case "assigned_to":
                                        issue.AssignedTo = row[column].ToString();
                                        break;
                                    case "reported_by":
                                        issue.SubmittedBy = row[column].ToString();
                                        break;
                                }
                            }
                        }

                        issueList.Insert(0, issue);
                    }
                }

                return issueList;
            }
            catch (Exception ex)
            {
                _sentryLog.Capture(new SentryEvent(ex));
            }

            return null;
        }

        public IssueDetails GetIssueDetails(int issueNumber)
        {
            SetServiceCredentials();
            var issue = new IssueDetails();

            BugTrackingIssue repList = _btService.GetIssueByNum(issueNumber);

            var history = _btService.GetIssueHistory(issueNumber);

            //var remarks = _btService.GetIssueRemarks(issueNumber);

            issue.History = BuildIssueHistory(history);

           // issue.Remarks = BuildRemarks(remarks);

            issue.IssueNumber = repList.IssueNumber;
            issue.Title = repList.FieldsArray[0].Value;
            issue.Description = repList.FieldsArray[1].Value;
            issue.StepstoReproduce = repList.FieldsArray[2].Value;
            issue.Product = repList.FieldsArray[3].Value;

            foreach (var item in repList.FieldsArray.Skip(4))
            {
                var value = item.Value;

                if (item.SystemName == "remarks")
                {
                    value = System.Web.HttpUtility.HtmlEncode(value);
                }
                var fields = new IssueDetailsFields()
                {
                    Caption = item.Caption,
                    Value = value,
                    SystemName = item.SystemName,
                    OptionList = item.ValueList
                };
                issue.Fields.Add(fields);
            }

            return issue;
        }

        private List<IssueHistory> BuildIssueHistory(DataSet history)
        {
            if (history == null) return null;

            var newHistory = new List<IssueHistory>();

            foreach (DataTable table in history.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var hist = new IssueHistory();
                    hist.FieldName = row[2].ToString();
                    hist.Change= row[3].ToString();
                    hist.Usename = row[4].ToString();
                    DateTime res;
                    DateTime.TryParse(row[5] + " " + row[6], out res);
                    hist.DateOfChange = res;
                    newHistory.Add(hist);
                }
            }

            return newHistory;
        }

        private List<IssueRemarks> BuildRemarks(DataSet history)
        {
            if (history == null) return null;

            var newRemarks = new List<IssueRemarks>();

            foreach (DataTable table in history.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var hist = new IssueRemarks();
                    hist.Username = row[2].ToString();
                    hist.RemarkText = row[3].ToString();
                    DateTime res;
                    DateTime.TryParse(row[4] + " " + row[5], out res);
                    hist.DateOfRemark = res;
                    newRemarks.Add(hist);
                }
            }

            return newRemarks;
        }


        public IssueDetails SaveIssue(IssueDetails issueDetails)
        {
            SetServiceCredentials();
            BugTrackingIssue Issue = _btService.GetIssueByNum(issueDetails.IssueNumber);

            var issue = BugTrackingIssueHelpers.SetBugTrackingIssueFields(Issue, issueDetails);

            BugTrackingIssue savedIssue = _btService.SaveIssue(issue);

            return BugTrackingIssueHelpers.GetNewIssueDetails(savedIssue);
        }

        public List<Report> GetWelcomeReportList()
        {
            SetServiceCredentials();

            var welcomeReports = _btService.GetWelcomeReports();
            var reportlist = new List<Report>();

            foreach (DataTable table in welcomeReports.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var hist = new Report();
                    hist.Count = row[0].ToString();
                    hist.Name = row[1].ToString();
                    hist.WelcomeReportId = row[2].ToString();
                    reportlist.Add(hist);
                }
            }

            return reportlist;
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

        private IssueField GetFieldSystemCaption(IssueField[] repList, string caption)
        {
            return repList.FirstOrDefault(v => v.Caption == caption);
        }

        private DateTime GetLastUpdateDate(int issueNumber)
        {
            DateTime res = new DateTime();

            var d = _btService.GetIssueHistory(issueNumber);

            foreach (DataTable table in d.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    var date = row[5];
                    var time = row[6];
                    var dateTim = DateTime.TryParse(date + " " + time, out res);

                    break;
                }
            }

            return res;
        }

        private void SetServiceCredentials()
        {
            //(user name = "account\username")
            UsernameToken token = new UsernameToken(_eToolAccountName + @"\" + _eToolUserName, _eToolUserPasswd, PasswordOption.SendHashed);
            _btService.RequestSoapContext.Security.Tokens.Add(token);
            _btService.RequestSoapContext.Security.Elements.Add(new MessageSignature(token));
        }
    }
}
