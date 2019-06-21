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
    public class IssueDetailController : ApiController
    {
        private ElementService _elementService;
        private ICache _cacheLayer;
       
        public IssueDetailController()
        {
            _cacheLayer = new FirebaseStorage();
            _elementService = new ElementService(_cacheLayer);
        }

        public IHttpActionResult Get(int id)
        {
            _elementService._accountItem = GetItemFromClaims();

            var result = _elementService.GetIssueDetails(id);

            return Ok(result);
        }

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