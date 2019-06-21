using System.Web.Http;
using ElemenTool.CacheLayer.Infrastructure;
using ElementTool.WebApi.Infrastructure;
using ElementTool.WebApi.DataObjects;
using ElementTool.WebApi.Infrastructure.DbStorage;
using ElementTool.WebApi.Infrastructure.Filters;
using System.Security.Claims;
using System.Linq;
using System.Web.OData;

namespace ElementTool.WebApi.Controllers
{
    [JwtAuthentication]
    [EnableQuery]
    public class IssuesController : ODataController
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

            return Ok(result.AsQueryable());
        }

        public IHttpActionResult Get(int key)
        {
            _elementService._accountItem = GetItemFromClaims();

            var result = _elementService.GetIssuesByReportId(key);

            return Ok(result);
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