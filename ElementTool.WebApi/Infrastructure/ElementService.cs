﻿using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using ElemenTool.CacheLayer.Entities;
using ElementTool.WebApi.DataObjects;
using ElementTool.WebApi.Infrastructure;
using ElementTool.WebApi.Models;
using System.Linq;

namespace ElemenTool.CacheLayer.Infrastructure
{
    public class ElementService : IElementService
    {
        private readonly ICache _documentStorage;
        private ElementoolApi _elementoolApi;
        public ElemenToolItem _accountItem;

        public ElementService(ICache documentStorage)
        {
            _documentStorage = documentStorage;
        }

        public IssueDetails AddIssueDetails(IssueDetails issueDetails)
        {
            _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);
            _elementoolApi.SaveIssueDetails(issueDetails);

            return issueDetails;
        }

        public List<Issue> AddIssueList(List<Issue> issue)
        {
            throw new NotImplementedException();
        }

        public IssueDetails GetIssueDetails(int issueNumber)
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

            ////TODO: check for cache date if is more than 3 hours load from api.
            //if(_documentStorage.GetIssueListFromStore(_accountItem.AccountName).Count == 0)
            //{
            //    var issuelist = _elementoolApi.GetIssueList();
            //    _documentStorage.AddToProcessTable(_accountItem.UserName, _accountItem.AccountName);
            //    //await Task.Run(() => _documentStorage.AddIssueList(null, issuelist));

            //    return issuelist;
            //}
            //if (refresh)
            //{
            //    return _elementoolApi.GetIssueList();
            //}

            //return _documentStorage.GetIssueListFromStore(_accountItem.AccountName);

            return _elementoolApi.GetIssueList();
        }

        public async Task<IssueDetails> GetRefreshedIssueDetails(IssueDetails cachedIssueDetails)
        {
            _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);

            IssueDetails d = null;
            
            await Task.Run(() =>  d =_elementoolApi.GetIssueDetails(cachedIssueDetails.IssueNumber));

            if (!d.Serialize().Equals(cachedIssueDetails.Serialize()))
            {
                await _documentStorage.AddIssueDetails(null, d);
            }
            return d;
        }

        public IssueDetails SaveIssue(IssueDetails issueDetails)
        {
            _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);

            return _elementoolApi.SaveIssue(issueDetails);
        }

        public bool CanLogin(ElemenToolItem item)
        {
            _elementoolApi = new ElementoolApi(item.AccountName, item.UserName, item.Password);
            var canLogin = _elementoolApi.LoginCheck(item);

            return canLogin;
        }

        public IEnumerable<Report> GetReportList(bool refresh = false)
        {
            _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);

            var quick = _elementoolApi.GetReports();

            var list = quick.Select(c => new Report() { Decription = c.Description, Id = c.ID, Name = c.Name });

            return list;
        }

        public IEnumerable<Report> GetWelcomeReportList()
        {
             _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);

            var quick = _elementoolApi.GetWelcomeReportList();

            return quick;
        }

        public IEnumerable<Issue> GetIssuesByReportId(int id)
        {
            _elementoolApi = new ElementoolApi(_accountItem.AccountName, _accountItem.UserName, _accountItem.Password);

            var quick = _elementoolApi.GetIssuesByReportId(id);

            return quick;
        }
    }
}
