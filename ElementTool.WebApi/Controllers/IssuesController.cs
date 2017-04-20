using System.Threading.Tasks;
using System.Web.Http;
using ElemenTool.CacheLayer.Infrastructure;
using ElementTool.WebApi.Infrastructure;
using ElementTool.WebApi.DataObjects;
using ElementTool.WebApi.Infrastructure.DbStorage;
using ElementTool.WebApi.Infrastructure.Filters;
using System.Security.Claims;

namespace ElementTool.WebApi.Controllers
{
    [JwtAuthentication]
    public class IssuesController : ApiController
    {
        private ElementService _elementService;
        private ICache _cacheLayer;
       

        public IssuesController()
        {
            _cacheLayer = new FirebaseStorage();
            _elementService = new ElementService(_cacheLayer);
        }

        // GET tables/TodoItem
        public IHttpActionResult Get()
        {
            _elementService._accountItem = GetItemFromClaims();

            var result = _elementService.GetIssueList();

            return Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            _elementService._accountItem = GetItemFromClaims();

            var result = _elementService.GetIssueDetails(id);

            return Ok(result);
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

        private ElemenToolItem GetItemFromClaims()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var etItem = new ElemenToolItem();
            etItem.UserName = identity.FindFirst(ClaimTypes.Name).Value;
            etItem.Password = identity.FindFirst(ClaimTypes.UserData).Value;
            etItem.AccountName = identity.FindFirst(ClaimTypes.GroupSid).Value;

            return etItem;
        }
    }
}