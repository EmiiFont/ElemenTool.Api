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

namespace ElemenTool.Api.Controllers
{
    public class ElemenToolController : TableController<TodoItem>
    {
        private ElementService _elementService;
        private ICache _cacheLayer;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<TodoItem>(context, Request, Services);
            _cacheLayer = new SqliteCache();
            _elementService = new ElementService(_cacheLayer);
        }

        // GET tables/TodoItem
        [Route("api/getIssues")]
        public List<Issue> GetIssues([FromUri]ElemenToolItem item)
        {
            _elementService._accountItem = item;
            var result = _elementService.GetIssueList();
            return result;
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [Route("api/getIssueDetails")]
        public async Task<IssueDetails> GetIssueDetails(int id, [FromUri]ElemenToolItem item)
        {
            _elementService._accountItem = item;
            var result = _elementService.GetIssueDetails(id).Result;

            return result;
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(TodoItem item)
        {
            TodoItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}