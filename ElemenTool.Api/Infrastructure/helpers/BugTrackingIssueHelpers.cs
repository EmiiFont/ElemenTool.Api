using ElemenTool.Api.com.elementool.www;
using ElemenTool.CacheLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElemenTool.Api.Infrastructure.helpers
{
    public static class BugTrackingIssueHelpers
    {
        public static BugTrackingIssue SetBugTrackingIssueFields(BugTrackingIssue issue, IssueDetails issuedetails)
        {
            issue.IssueNumber = issuedetails.IssueNumber;
            issue.FieldsArray[0].Value = issuedetails.Title;
            issue.FieldsArray[1].Value = issuedetails.Description;
            issue.FieldsArray[2].Value = issuedetails.StepstoReproduce;
            issue.FieldsArray[3].Value = issuedetails.Product;
            issue.FieldsArray[4].Value = issuedetails.Status;
            issue.FieldsArray[5].Value = issuedetails.Originalissue;
            issue.FieldsArray[6].Value = issuedetails.Versionfoundin;
            issue.FieldsArray[7].Value = issuedetails.Priority;
            issue.FieldsArray[8].Value = issuedetails.QAAssigned;
            issue.FieldsArray[9].Value = issuedetails.Submittedby;
            issue.FieldsArray[10].Value = issuedetails.DevAssigned;
            issue.FieldsArray[11].Value = issuedetails.Versionfixedin;
            issue.FieldsArray[12].Value = issuedetails.Remarks;
            issue.FieldsArray[13].Value = issuedetails.Customer;
            issue.FieldsArray[14].Value = issuedetails.CaseContact;
            issue.FieldsArray[15].Value = issuedetails.Productperson;
            issue.FieldsArray[16].Value = issuedetails.FixType;
            issue.FieldsArray[17].Value = issuedetails.CC;
            issue.FieldsArray[18].Value = issuedetails.TimeSpent;
            issue.FieldsArray[19].Value = issuedetails.SLADeliveryDate;
            issue.FieldsArray[20].Value = issuedetails.PriorityListPosition;
            issue.FieldsArray[21].Value = issuedetails.ServicePack;
            issue.FieldsArray[22].Value = issuedetails.BuildNumber;
            issue.FieldsArray[23].Value = issuedetails.AssignedPriority;
            issue.FieldsArray[24].Value = issuedetails.DevTimeRem;
            issue.FieldsArray[25].Value = issuedetails.DevCompDate;
            issue.FieldsArray[26].Value = issuedetails.DaysSinceIssueCreated;
            issue.FieldsArray[27].Value = issuedetails.QACompDate;

            return issue;
        }

        public static IssueDetails GetNewIssueDetails(BugTrackingIssue repList)
        {
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
    }
}