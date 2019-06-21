using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using ElemenTool.Api.DataObjects;
using ElemenTool.Api.Models;
using ElemenTool.CacheLayer.Infrastructure;
using ElemenTool.Api.Infrastructure;
using System.Collections.Generic;
using ElemenTool.CacheLayer.Entities;
using System;
using Microsoft.WindowsAzure.Storage;
using ElemenTool.Api.Infrastructure.AzureStorage;

namespace ElemenTool.Api.Controllers
{
    public class IssuesController : ApiController
    {
        private ElementService _elementService;
        private ICache _cacheLayer;
        private AzureContext _context;

        public IssuesController()
        {
            _context = new AzureContext();

            _cacheLayer = new SqliteCache();
            _elementService = new ElementService(_cacheLayer);
        }

        public bool PostLogin(ElemenToolItem item)
        {
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;

            item.PartitionKey = item.AccountName;
            item.RowKey = item.UserName;
            var existing = _context.GetAccountItem(item.AccountName, item.UserName);

            if (existing == null)
            {
                _cacheLayer.InsertElementToolEntity(item);
            }
            else
            {
                if (existing.Password != item.Password)
                {
                    //update item
                }
            }
           
            return _elementService.CanLogin(item);
        }

        // GET tables/TodoItem
        public List<Issue> GetIssues(string user)
        {
            var accountName = user.Split('@')[0];
            var username = user.Split('@')[1];

            var item = _context.GetAccountItem(accountName, username);
            _elementService._accountItem = item;

            var result = _elementService.GetIssueList();
            return result;
        }

        //// GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        //public async Task<IssueDetails> GetIssueDetails(int id, string user)
        //{
        //    var accountName = user.Split('@')[0];
        //    var username = user.Split('@')[1];

        //    var item = _context.GetAccountItem(accountName, username);

        //    _elementService._accountItem = item;
        //    var result = _elementService.GetIssueDetails(id).Result;

        //    return result;
        //}

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return new Task(() => { });
        }
    }
}