using ElemenTool.CacheLayer.Entities;
using System;
using System.Linq;
using ElementTool.WebApi.com.elementool.www;

namespace ElementTool.WebApi.Infrastructure.helpers
{
    public static class BugTrackingIssueHelpers
    {
        public static BugTrackingIssue SetBugTrackingIssueFields(BugTrackingIssue issue, IssueDetails issuedetails)
        {
            issue.IssueNumber = issuedetails.IssueNumber;

            for (int i = 1; i < issuedetails.Fields.Count; i++)
            {
                issue.FieldsArray[i].Value = issuedetails.Fields[i].Value;
            }

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
    }
}