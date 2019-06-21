using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElemenTool.CacheLayer.Entities;
using System.Threading.Tasks;
using ElemenTool.Api.DataObjects;
using ElementTool.WebApi.DataObjects;

namespace ElementTool.WebApi.Infrastructure
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
        void AddIssueList(string cacheKey, List<Issue> item);

        IssueDetails GetIssueDetailsFromStore(int issueNumber);

        List<Issue> GetIssueListFromStore(string accountName);
        void AddIssueList(object p, List<Issue> issuelist);
        ElemenToolItem GetAccountItem(string accountName, string userName);
        void InsertElementToolEntity(ElemenToolItem item);
        void UpdateAccountItem(ElemenToolItem item);

        void AddToProcessTable(string userName, string accountName);
    }
}