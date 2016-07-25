using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElemenTool.CacheLayer.Entities;
using ElemenTool.Api.Infrastructure;
using ElemenTool.Api.DataObjects;

namespace ElemenTool.CacheLayer.Infrastructure
{
    public class ElementService : IElementService
    {
        private readonly ICache _sqliteCache;
        private ElementoolApi _elementoolApi;
        public ElemenToolItem _accountItem;

        public ElementService(ICache sqliteCache)
        {
            _sqliteCache = sqliteCache;
        }

        public IssueDetails AddIssueDetails(IssueDetails issueDetails)
        {
            throw new NotImplementedException();
        }

        public List<Issue> AddIssueList(List<Issue> issue)
        {
            throw new NotImplementedException();
        }

        public async Task<IssueDetails> GetIssueDetails(int issueNumber)
        {
            _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);

            //var cachedIssue = _sqliteCache.GetIssueDetailsFromStore(issueNumber);
           
            //if (cachedIssue == null)
            //{
                var issue = _elementoolApi.GetIssueDetails(issueNumber);
                //this one can be an async call
                //await  _sqliteCache.AddIssueDetails(null, issue);
                return issue;
           // }

           // return cachedIssue;
        }

        public List<Issue> GetIssueList(bool refresh = false)
        {
            _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);

            //TODO: check for cache date if is more than 3 hours load from api.
            //if (_sqliteCache.GetIssueListFromStore().Count == 0)
            //{
                var issuelist = _elementoolApi.GetIssueList();
                Task.Run(() => _sqliteCache.AddIssueList(null, issuelist));
                
                return issuelist;
           // }
            //if (refresh)
            //{
             //  return _elementoolApi.GetIssueList();
            //}

           /// return _sqliteCache.GetIssueListFromStore();
        }

        public async Task<IssueDetails> GetRefreshedIssueDetails(IssueDetails cachedIssueDetails)
        {
            _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);

            IssueDetails d = null;
            
            await Task.Run(() =>  d =_elementoolApi.GetIssueDetails(cachedIssueDetails.IssueNumber));

            if (!d.Serialize().Equals(cachedIssueDetails.Serialize()))
            {
                await _sqliteCache.AddIssueDetails(null, d);
            }
            return d;
        }
    }
}
