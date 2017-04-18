using System.Threading.Tasks;
using System.Web.Http;
using ElemenTool.CacheLayer.Infrastructure;
using System.Collections.Generic;
using ElemenTool.CacheLayer.Entities;
using System;
using ElementTool.WebApi.Infrastructure;
using ElementTool.WebApi.DataObjects;
using ElementTool.WebApi.Infrastructure.DbStorage;

namespace ElementTool.WebApi.Controllers
{
    public class IssuesController : ApiController
    {
        private ElementService _elementService;
        private ICache _cacheLayer;
        private ICache _context;

        public IssuesController()
        {
            _context = new FirebaseStorage();

            _cacheLayer = new FirebaseStorage();
            _elementService = new ElementService(_cacheLayer);
        }

        public bool PostLogin(ElemenToolItem item)
        {
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;

            var existing = _context.GetAccountItem(item.AccountName, item.UserName);

            if (existing == null)
            {
                _context.InsertElementToolEntity(item);
            }
            else
            {
                if (existing.Password != item.Password)
                {
                    existing.Password = item.Password;
                    _context.UpdateAccountItem(existing);
                }
            }
           
            return _elementService.CanLogin(item);
        }

        // GET tables/TodoItem
        public List<Issue> GetIssues(string id)
        {
            var username = id.Split('@')[0];
            var accountName = id.Split('@')[1];

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