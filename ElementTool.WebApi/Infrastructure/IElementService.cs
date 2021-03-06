﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElemenTool.CacheLayer.Entities;
using ElemenTool.Api.DataObjects;
using ElementTool.WebApi.DataObjects;
using ElementTool.WebApi.Models;

namespace ElemenTool.CacheLayer.Infrastructure
{
    public interface IElementService
    {
        IssueDetails AddIssueDetails(IssueDetails issueDetails);
        List<Issue> AddIssueList(List<Issue> issue);
        IssueDetails GetIssueDetails(int issueNumber);
        List<Issue> GetIssueList(bool refresh = false);
        IEnumerable<Report> GetReportList(bool refresh = false);
        IEnumerable<Report> GetWelcomeReportList();
        IEnumerable<Issue> GetIssuesByReportId(int id);
        IssueDetails SaveIssue(IssueDetails issueDetails);
        bool CanLogin(ElemenToolItem item);
        Task<IssueDetails> GetRefreshedIssueDetails(IssueDetails cachedIssueDetails);
    }
}
