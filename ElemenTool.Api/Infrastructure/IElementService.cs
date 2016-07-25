﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElemenTool.CacheLayer.Entities;

namespace ElemenTool.CacheLayer.Infrastructure
{
    public interface IElementService
    {
        IssueDetails AddIssueDetails(IssueDetails issueDetails);
        List<Issue> AddIssueList(List<Issue> issue);
        Task<IssueDetails> GetIssueDetails(int issueNumber);
        List<Issue> GetIssueList(bool refresh = false);

        Task<IssueDetails> GetRefreshedIssueDetails(IssueDetails cachedIssueDetails);
    }
}
