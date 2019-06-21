using ElemenTool.CacheLayer.Infrastructure;
using ElementTool.WebApi.DataObjects;
using ElementTool.WebApi.Infrastructure;
using ElementTool.WebApi.Infrastructure.DbStorage;
using System.Security.Claims;
using System.Linq;
using ElementTool.WebApi.Infrastructure.Filters;
using System.Web.Http;

namespace ElementTool.WebApi.Controllers
{
    [JwtAuthentication]
    public class ReportsController : ApiController
    {
        private ElementService _elementService;
        private ICache _cacheLayer;

        public ReportsController()
        {
            _cacheLayer = new FirebaseStorage();
            _elementService = new ElementService(_cacheLayer);
        }

        // GET tables/TodoItem
        public IHttpActionResult Get()
        {
            _elementService._accountItem = GetItemFromClaims();

            var result = _elementService.GetReportList();

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
