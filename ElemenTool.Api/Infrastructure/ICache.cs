using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElemenTool.CacheLayer.Entities;
using System.Threading.Tasks;
using ElemenTool.Api.DataObjects;

namespace ElemenTool.Api.Infrastructure
{
    public interface ICache
    {
        /// <summary>
        /// Add item to the cache store, it will update previously added item.
        /// </summary>
        /// <param name="cacheKey">key to lookup item</param>
        /// <param name="item">the item to store</param>
        Task<IssueDetails> AddIssueDetails(string cacheKey, IssueDetails item);

        /// <summary>
        /// Get item from the cache store, if item don't exist it will be added to
        /// cache
        /// </summary>
        /// <param name="cacheKey">key to lookup item</param>
        /// <param name="item">the item to store</param>
        /// <returns></returns>
        Task<List<Issue>> AddIssueList(string cacheKey, List<Issue> item);

        IssueDetails GetIssueDetailsFromStore(int issueNumber);

        List<Issue> GetIssueListFromStore();
        void AddIssueList(object p, List<Issue> issuelist);

        void InsertElementToolEntity(ElemenToolItem item);

    }
}