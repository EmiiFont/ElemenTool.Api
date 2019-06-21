using ElemenTool.CacheLayer.Infrastructure;
using ElementTool.WebApi.DataObjects;
using ElementTool.WebApi.Infrastructure;
using ElementTool.WebApi.Infrastructure.DbStorage;
using ElementTool.WebApi.Infrastructure.Filters;
using ElementTool.WebApi.Infrastructure.Validator;
using System;
using System.Web.Http;

namespace ElementTool.WebApi.Controllers
{
    public class AuthController : ApiController
    {
        private ICache _context;
        private ElementService _elementService;

        public AuthController()
        {
            _context = new FirebaseStorage();
            _elementService = new ElementService(_context);

        }
        [AllowAnonymous]
        public IHttpActionResult Post([FromBody]ElemenToolItem item)
        {
            if (CheckUser(item.AccountName, item.UserName, item.Password))
            {
                return Ok(JwtManager.GenerateToken(item));
            }

            return Unauthorized();
        }

        [JwtAuthentication]
        public IHttpActionResult Get()
        {
            return Ok(true);
        }

        private bool CheckUser(string account, string username, string password)
        {
            var newItem = new ElemenToolItem();
            newItem.AccountName = account;
            newItem.UserName = username;
            newItem.Password = password;
            newItem.FullAccount = username + "@" + account;
            newItem.UpdatedAt = DateTime.Now;
            newItem.CreatedAt = DateTime.Now;

            var existing = _context.GetAccountItem(newItem.AccountName, newItem.UserName);

            if (existing == null)
            {
                _context.InsertElementToolEntity(newItem);
            }
            else
            {
                if (existing.Password != newItem.Password)
                {
                    existing.Password = newItem.Password;
                    _context.UpdateAccountItem(existing);
                }
            }

            return _elementService.CanLogin(newItem);
        }
    }
}